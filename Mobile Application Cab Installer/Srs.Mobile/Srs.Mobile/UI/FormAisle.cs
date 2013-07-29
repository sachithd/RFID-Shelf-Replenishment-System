using System;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using Srs.Mobile.Common;
using Srs.Mobile.Data;
using Srs.Mobile.Properties;
using Srs.Mobile.WebReference;

namespace Srs.Mobile.UI
{
    public partial class FormAisle : Form
    {
        private readonly FormTagScan _scanForm = new FormTagScan();
        private int _stocktakeId;
        private LocalStore _localStore;
        private Logger _logger;
        private WebServices _webService;

        internal FormAisle(int stocktakeId)
        {
            InitializeComponent();
            this._stocktakeId = stocktakeId;
        }


        private void LoadAisleNumbers()
        {
            try
            {
                //server ws = _webService.GetWebReference();
                //WebReference.server ws = new WebReference.server();
                //ws.Url = ConfigurationSettings.AppSettings["webServiceUrl"];
                //ws.Timeout = int.Parse(ConfigurationSettings.AppSettings["webServiceTimeOut"]); //Time out after 5 secs

                Aisles[] aislelist = _webService.GetAisleList();

                if (aislelist!=null)
                {
                    int i = 0;
                    foreach (Aisles item in aislelist)
                    {
                        var lid = new ListViewItem(aislelist[i].aisle_number); //Aisle Number
                        lid.SubItems.Add(aislelist[i].aisle_description); //Description
                        lid.SubItems.Add("Not Scanned"); //Status
                        lid.SubItems.Add(aislelist[i].aisle_id.ToString()); //Aisle ID is hidden
                        lstAisleList.Items.Add(lid);

                        i++;
                    }
                }
                else
                {
                    MessageBox.Show(Resources.Download_Aisle_Error,
                                Resources.Srs_Title);
                    Close();
                }
            }
            catch (WebException)
            {
                MessageBox.Show(Resources.Download_Aisle_Error,
                                Resources.Srs_Title);
                Close();

            }
        }


        /// <summary>
        /// Prepare the selected aisle to perform the stock take
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            //frmTagScan scanForm = new frmTagScan();
            bool flag = true;


            if (lstAisleList.SelectedIndices.Count > 0)
            {
                foreach (int i in lstAisleList.SelectedIndices)
                {
                    
                    string tmpAisleNo = lstAisleList.Items[i].Text;
                    _scanForm.SetAisleNo = tmpAisleNo;
                    _scanForm.SetAisleDescription = lstAisleList.Items[i].SubItems[1].Text;
                    _scanForm.SetAisleId = lstAisleList.Items[i].SubItems[3].Text;

                    /**
                     * If the aisle is already scanned ask user for a confirmation and perform a new scan
                     * Delete the previous database record
                     */
                    if (lstAisleList.Items[i].SubItems[2].Text == "Scanned")
                    {
                        DialogResult scanResult =
                            MessageBox.Show(Resources.Aisle_Already_Scanned, Resources.Srs_Title,
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                            MessageBoxDefaultButton.Button1);
                        if (scanResult == DialogResult.Yes)
                        {
                            DeleteCurrentAisleData(_stocktakeId, tmpAisleNo);
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                }

                //Only open the scan form if flag is true.
                if (flag)
                {
                    _scanForm.SetStocktakeId = _stocktakeId;
                    DialogResult result = _scanForm.ShowDialog();
                    UpdateAisleListStatus();
                   /* if (result == DialogResult.OK || result == DialogResult.Yes)
                    {
                        string completedAisleNo = _scanForm.ReturnAisleNumber; //Aisle Number scan is completed

                        //Update the listview to scanned status.
                        UpdateAisleListStatus();
                    }*/
                }
            }
            else
            {
                MessageBox.Show(Resources.Select_Aisle,
                                Resources.Srs_Title);
            }
        }

        /// <summary>
        /// User decides to scan an aisle again. So delete the previous records.
        /// </summary>
        /// <param name="stocktakeId"></param>
        /// <param name="tmpAisleNo"></param>
        private void DeleteCurrentAisleData(int stocktakeId, string tmpAisleNo)
        {
            SqlCeConnection conn = null;


            try
            {
                conn = _localStore.GetConnection();
                conn.Open();
                string strSQL =
                    "DELETE FROM stocktake_itemdetails WHERE stocktake_id = @stocktake_id AND aisle_number=@aisle_no";
                
                using (var cmd = new SqlCeCommand(strSQL, conn))
                {
                    cmd.Parameters.Add("@stocktake_id", stocktakeId);
                    cmd.Parameters.Add("@aisle_no", tmpAisleNo);
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
        /// Update the status of the scanned aisles
        /// </summary>
        private void UpdateAisleListStatus()
        {
            SqlCeConnection conn = null;

            try
            {
                conn = _localStore.GetConnection();
                conn.Open();

                string strSQL = "SELECT distinct(aisle_number) from stocktake_itemdetails WHERE stocktake_id= " +
                                _stocktakeId + " ORDER BY aisle_number";

                var cmd = new SqlCeCommand(strSQL, conn);
                SqlCeDataReader rdr = cmd.ExecuteReader();

                //Reset the status
                foreach (ListViewItem item in lstAisleList.Items)
                {
                    item.SubItems[2].Text = "Not Scanned"; // Set the status
                    item.BackColor = Color.White;
                }

                if (null != rdr)
                {
                    while (rdr.Read())
                    {
                        foreach (ListViewItem item in lstAisleList.Items)
                        {
                            if (item.Text == rdr.GetString(0))
                            {
                                item.SubItems[2].Text = "Scanned"; // Set the status
                                item.BackColor = Color.Crimson;
                            }
                            /*else
                            {
                                item.SubItems[2].Text = "Not Scanned"; // Set the status
                                item.BackColor = Color.White;
                            }*/

                        }

                    }
                    rdr.Close();
                    rdr.Dispose();
                }
                else
                {
                    //No records are found
                    foreach (ListViewItem item in lstAisleList.Items)
                    {
                        item.SubItems[2].Text = "Not Scanned"; // Set the status
                        item.BackColor = Color.White;
                    }
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(Resources.Exit_Stock_Take,
                                                  Resources.Srs_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                  MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                Close();
               // _scanForm.Close(); //Destroy the form
            }
        }


        /// <summary>
        /// Uploads the stock take data onto the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            btnUpload.Enabled = false;
            btnBack.Enabled = false;
            btnNext.Enabled = false;

            //Ask user to confirm msg box
            DialogResult result = MessageBox.Show(Resources.Complete_Stock_Take,
                                                  Resources.Srs_Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                  MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                //If confirmed update the stocktake finishtime
                UpdateFinishDateTime(_stocktakeId);

                //Prepare the complex type to send to the webservice 
                SqlCeConnection conn = null;
                try
                {
                    conn = _localStore.GetConnection();
                    conn.Open();

                    string strSQL = "SELECT * from stocktake WHERE stock_takeid= " + _stocktakeId;

                    SqlCeCommand cmd = new SqlCeCommand(strSQL, conn);
                    SqlCeDataReader rdr = cmd.ExecuteReader();

                    Stock stk = new Stock();
                    if (null != rdr)
                    {
                        while (rdr.Read())
                        {
                            stk.stocktake_id = int.Parse(rdr["stock_takeid"].ToString());
                            stk.time_start = rdr["time_start"].ToString();
                            stk.time_finished = rdr["time_finished"].ToString();
                            stk.upload_status = int.Parse(rdr["upload_status"].ToString());
                            stk.user_name = rdr["user_name"].ToString();
                        }
                        rdr.Close();
                        rdr.Dispose();
                    }

                    //Count the number of records
                    strSQL = "SELECT count(*) from stocktake_itemdetails WHERE stocktake_id= " + _stocktakeId;
                    cmd = new SqlCeCommand(strSQL, conn);
                    var rowCount = (Int32) cmd.ExecuteScalar();


                    strSQL = "SELECT * from stocktake_itemdetails WHERE stocktake_id= " + _stocktakeId;
                    cmd = new SqlCeCommand(strSQL, conn);

                    rdr = cmd.ExecuteReader();

                    var si = new StockItems[rowCount];
                    int j = 0;
                    if (null != rdr)
                    {
                        while (rdr.Read())
                        {
                            //Debug.Write(rdr["stocktake_id"].ToString());
                            si[j] = new StockItems
                                        {
                                            stocktake_id = int.Parse(rdr["stocktake_id"].ToString()),
                                            tag_id = rdr["tag_id"].ToString(),
                                            aisle_number = rdr["aisle_number"].ToString(),
                                            aisle_id = int.Parse(rdr["aisle_id"].ToString())
                                        };
                            j++;
                        }
                        rdr.Close();
                        rdr.Dispose();
                    }


                    try
                    {
                        int ustatus = _webService.UploadStockData(si, stk);

                        //The records have been inserted sucessfully or no records have been inserted to stockitemdetails table. (0 products count)
                        if (ustatus == 1 || ustatus == 3)
                        {
                            //Upload succesfull, update the database status
                            UpdateUploadStatus(_stocktakeId, conn);
                            MessageBox.Show(Resources.Stock_Take_Sucessfull, Resources.Srs_Title);
                        }
                        else if (ustatus == 2) //Error inserting to stock item details 
                        {
                            MessageBox.Show(Resources.Stock_Take_Completed_Db_Error, Resources.Srs_Title,
                                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation,
                                            MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            MessageBox.Show(Resources.Stock_Take_Completed_Upload_Error, Resources.Srs_Title, MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }

                        
                    }
                    catch (WebException ex)
                    {
                        Debug.WriteLine("Web Error" + ex);
                    }
                    catch (XmlException ex)
                    {
                        //this report had an error loading!
                        Debug.WriteLine(ex.Message);
                    }
                }
                catch (SqlCeException ex)
                {
                    Debug.WriteLine("Database Error" + ex);
                }
                finally
                {
                    if (conn != null) conn.Close();
                    Close();
                }
            } // End of message box if
            btnUpload.Enabled = true;
            btnBack.Enabled = true;
            btnNext.Enabled = true;
        }


        /// <summary>
        /// Update the finished time of the completed stock take 
        /// </summary>
        /// <param name="stocktakeId"></param>
        private void UpdateFinishDateTime(int stocktakeId)
        {
            SqlCeConnection conn = null;
            DateTime dt = DateTime.Now;


            try
            {
                conn = _localStore.GetConnection();
                conn.Open();
                string strSQL = "UPDATE stocktake SET time_finished=@ftime WHERE stock_takeid = @stocktake_id";

                Debug.WriteLine(stocktakeId);
                Debug.WriteLine(dt);
                using (var cmd = new SqlCeCommand(strSQL, conn))
                {
                    cmd.Parameters.Add("@stocktake_id", stocktakeId);
                    cmd.Parameters.Add("@ftime", dt);
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
        /// Update the upload status (local database)
        /// This information is used to retry the upload
        /// </summary>
        /// <param name="stocktakeId"></param>
        private void UpdateUploadStatus(int stocktakeId, SqlCeConnection conn)
        {
            //SqlCeConnection conn = null;

            try
            {
                //conn = _localStore.GetConnection();
                //conn.Open();
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
        }

        private void FormAisle_Load(object sender, EventArgs e)
        {
            _logger = new Logger();
            _localStore = new LocalStore();
            _webService = new WebServices();

            //calling the method to load the aisle number from the web service
            LoadAisleNumbers();


        }
    }
}