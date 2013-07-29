using System;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Windows.Forms;
using Srs.Mobile.Common;
using Srs.Mobile.Data;

namespace Srs.Mobile.UI
{
    public partial class FormHistory : Form
    {
        private LocalStore _localStore;
        private Logger _logger;

        internal FormHistory()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmHistory_Load(object sender, EventArgs e)
        {
            _logger = new Logger();
            _localStore = new LocalStore();
            FillData();
            dtpFrom.Value = DateTime.Today.Subtract(TimeSpan.FromDays(1));
        }


        /// <summary>
        /// Read the database and fill the listview
        /// </summary>
        private void FillData()
        {
            SqlCeConnection conn = null;


            try
            {
                conn = _localStore.GetConnection();
                conn.Open();

                string strSQL =
                    "SELECT time_start, time_finished, upload_status, user_name from stocktake WHERE time_finished is NOT NULL ORDER BY time_finished DESC";

                var cmd = new SqlCeCommand(strSQL, conn);
                SqlCeDataReader rdr = cmd.ExecuteReader();

                if (null != rdr)
                {
                    while (rdr.Read())
                    {
                        var lid = new ListViewItem(rdr["time_start"].ToString()); //time_start
                        lid.SubItems.Add(rdr["time_finished"].ToString()); //time_finished


                        if (int.Parse(rdr["upload_status"].ToString()) == 1)
                        {
                            lid.SubItems.Add("Uploaded"); // upload_status
                            lid.BackColor = Color.White;
                        }
                        else
                        {
                            lid.SubItems.Add("Failed");
                            lid.BackColor = Color.Red;
                        }

                        /*if (rdr["time_finished"].ToString() == "")
                        {
                            lid.BackColor = Color.Khaki;
                        }*/

                        lid.SubItems.Add(rdr["user_name"].ToString()); //user_name
                        lstStockTakeList.Items.Add(lid);
                    }
                    rdr.Close();
                    rdr.Dispose();
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
        /// Reads the database and fills the data according to the given dates
        /// </summary>
        private void FilterData()
        {
            SqlCeConnection conn = null;

            DateTime dateFrom = dtpFrom.Value;
            DateTime dateTo = dtpTo.Value;
            try
            {
                conn = _localStore.GetConnection();
                conn.Open();

                string strSQL =
                    "SELECT time_start, time_finished, upload_status, user_name from stocktake WHERE time_finished IS NOT NULL AND time_finished BETWEEN @dateFrom AND @dateTo ORDER BY time_finished DESC";

                


                using (var cmd = new SqlCeCommand(strSQL, conn))
                {
                    cmd.Parameters.Add("@dateFrom", dateFrom);
                    cmd.Parameters.Add("@dateTo", dateTo);
                    SqlCeDataReader rdr = cmd.ExecuteReader();

                    if (null != rdr)
                    {
                        while (rdr.Read())
                        {
                            var lid = new ListViewItem(rdr["time_start"].ToString()); //time_start
                            lid.SubItems.Add(rdr["time_finished"].ToString()); //time_finished


                            if (int.Parse(rdr["upload_status"].ToString()) == 1)
                            {
                                lid.SubItems.Add("Yes"); // upload_status
                                lid.BackColor = Color.White;
                            }
                            else
                            {
                                lid.SubItems.Add("No");
                                lid.BackColor = Color.Yellow;
                            }


                            lid.SubItems.Add(rdr["user_name"].ToString()); //user_name
                            lstStockTakeList.Items.Add(lid);
                        }
                        rdr.Close();
                        rdr.Dispose();
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


        private void btnFilter_Click(object sender, EventArgs e)
        {
            lstStockTakeList.Items.Clear();
            FilterData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            lstStockTakeList.Items.Clear();
            FillData();
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
        }
    }
}