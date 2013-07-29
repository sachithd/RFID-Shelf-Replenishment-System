using System;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using Srs.Mobile.Common;
using Srs.Mobile.Data;
using Srs.Mobile.Properties;
using Srs.Mobile.WebReference;

namespace Srs.Mobile.UI
{
    public partial class FormMenu : Form
    {
        private LocalStore _localStore;
        private Logger _logger;
        private Users _user;

        internal FormMenu(Users paramUser)
        {
            InitializeComponent();

            _user = paramUser;
            lblLoggedIn.Text = _user.Name;
        }


        private void btnNewStockTake_Click(object sender, EventArgs e)
        {
            //Opens a new stock take session
            //Inserts a record to the stock take table and returns the record id 
            int newStocktakeId = NewStockTakeDB();

            if (newStocktakeId > -1)
            {
                _logger.WriteLog("New stocktake started", _user.Name);
                FormAisle aisleForm = new FormAisle(newStocktakeId); //Pass the last inserted stocktake id to the next form
                aisleForm.ShowDialog();
            }
            else
            {
                _logger.WriteLog("Database Error Inserting Stock Take record", _user.Name);
                //Debug.WriteLine("Database Error. Please check the database");
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _user = null;
            Close();
        }

        /// <summary>
        /// Adds a new stock take record to the database
        /// </summary>
        /// <returns>Primary Key (Stocktake ID) of the insertered record</returns>
        private int NewStockTakeDB()
        {
            SqlCeConnection conn = null;

            int ustatus = 0;
            DateTime dt = DateTime.Now;
            int id = -1;


            try
            {
                conn = _localStore.GetConnection();

                string strSQL =
                    "INSERT INTO stocktake(user_name,time_start,upload_status) VALUES(@user_name,@time_start,@upload_status)";
                string query2 = "Select @@Identity";

                using (SqlCeCommand cmd = new SqlCeCommand(strSQL, conn))
                {
                    cmd.Parameters.AddWithValue("@user_name", _user.Name);
                    cmd.Parameters.AddWithValue("@time_start", dt);
                    cmd.Parameters.AddWithValue("@upload_status", ustatus);
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = query2;
                    object tmp = cmd.ExecuteScalar();
                    id = (tmp == null) ? 0 : Convert.ToInt32(tmp);
                    Debug.WriteLine(id);
                }
            }
            catch (SqlCeException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return id;
        }

        /// <summary>
        /// Manually process the failed to upload stock take records. 
        /// This method can be automated to run periodically or when the internet connection / server available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUploadFailed_Click(object sender, EventArgs e)
        {
            //Prepare the complex type to send to the webservice 
            SqlCeConnection conn = null;

            int stocktake_id = 0;

            try
            {
                conn = _localStore.GetConnection();
                conn.Open();

                //This will only upload completed but failed to upload stock data
                string strSQL = "SELECT * from stocktake WHERE upload_status= 0 and time_finished IS NOT NULL";

                SqlCeCommand cmd = new SqlCeCommand(strSQL, conn);
                SqlCeDataReader rdr = cmd.ExecuteReader();
                
                var stk = new Stock();
                if (null != rdr)
                {
                    while (rdr.Read())
                    {
                        stocktake_id = int.Parse(rdr["stock_takeid"].ToString());
                        stk.stocktake_id = stocktake_id;
                        stk.time_start = rdr["time_start"].ToString();
                        stk.time_finished = rdr["time_finished"].ToString();
                        stk.upload_status = int.Parse(rdr["upload_status"].ToString());
                        stk.user_name = rdr["user_name"].ToString();


                        //Count the number of records
                        string strSQL2 = "SELECT count(*) from stocktake_itemdetails WHERE stocktake_id= " +
                                         stocktake_id;
                        var cmd2 = new SqlCeCommand(strSQL2, conn);
                        var rowCount = (Int32) cmd2.ExecuteScalar();


                        string strSQL3 = "SELECT * from stocktake_itemdetails WHERE stocktake_id= " + stocktake_id;
                        SqlCeCommand cmd3 = new SqlCeCommand(strSQL3, conn);

                        SqlCeDataReader rdr2 = cmd3.ExecuteReader();

                        StockItems[] si = new StockItems[rowCount];
                        int j = 0;
                        if (null != rdr2)
                        {
                            while (rdr2.Read())
                            {
                                si[j] = new StockItems
                                            {
                                                stocktake_id = int.Parse(rdr2["stocktake_id"].ToString()),
                                                tag_id = rdr2["tag_id"].ToString(),
                                                aisle_number = rdr2["aisle_number"].ToString(),
                                                aisle_id = int.Parse(rdr2["aisle_id"].ToString())
                                            };
                                j++;
                            }
                            rdr2.Close();
                            rdr2.Dispose();
                        }

                        try
                        {
                            var ws = new server();
                            int ustatus = ws.InsertStockCount(si, stk);

                            if (ustatus == 1 || ustatus == 3)
                            {
                                // Upload sucessfull to both stocktake, stockitemdetails tables, now update the local database status
                                UpdateUploadStatus(stocktake_id);

                                // If the status is 3 there are no records to insert into stockitemdetails.
                            }


                        }
                        catch (WebException ex)
                        {
                            Debug.WriteLine("Web Error" + ex);
                        }
                        catch (XmlException ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }
                    }

                    rdr.Close();
                    rdr.Dispose();
                }
            }
            catch (SqlCeException ex)
            {
                Debug.WriteLine("Database Error" + ex);
            }
            finally
            {
                if (conn != null) conn.Close();
                MessageBox.Show(Resources.Upload_Retried, Resources.Srs_Title);
            }
        }

        /// <summary>
        /// Update the upload status in the local database
        /// </summary>
        /// <param name="stocktakeId"></param>
        private void UpdateUploadStatus(int stocktakeId)
        {
            SqlCeConnection conn = null;

            try
            {
                conn = _localStore.GetConnection();
                conn.Open();
                string strSQL = "UPDATE stocktake SET upload_status=1 WHERE stock_takeid = @stocktake_id";


                using (var cmd = new SqlCeCommand(strSQL, conn))
                {
                    cmd.Parameters.Add("@stocktake_id", stocktakeId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlCeException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        /// <summary>
        /// Show history window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHistory_Click(object sender, EventArgs e)
        {
            var historyForm = new FormHistory();
            historyForm.ShowDialog();
        }

        /// <summary>
        /// Show incomplete stock takes
        /// The idea was to implement a method to resume an incomplete stock take
        /// But as we need real time data this functionality is not required
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResumeStockTake_Click(object sender, EventArgs e)
        {
            var resumeStockForm = new FormUncompleted();
            resumeStockForm.ShowDialog();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            _logger = new Logger();
            _localStore = new LocalStore();

            //Only authorized users are allowed to perform these operations
            //This should check the user type instead of the username but this is just to demonstrate
            //how it should work 
            //5 and 3 users have admin privilages for the device
            if (!(_user.Type == 5 || _user.Type == 3))
            {
                btnHistory.Enabled = false;
                btnResumeStockTake.Enabled = false;
                btnUploadFailed.Enabled = false;
            }
        }
    }
}