using System;
using System.Data.SqlServerCe;
using System.Windows.Forms;
using Srs.Mobile.Common;
using Srs.Mobile.Data;
using Srs.Mobile.Properties;

namespace Srs.Mobile.UI
{
    public partial class FormUncompleted : Form
    {
        private LocalStore _localStore;
        private Logger _logger;

        internal FormUncompleted()
        {
            InitializeComponent();
        }

        private void frmResumeStock_Load(object sender, EventArgs e)
        {
            _logger = new Logger();
            _localStore = new LocalStore();
            FillData();
        }

        private void FillData()
        {
            lstStockTakeList.Items.Clear(); //Clears ths list view

            SqlCeConnection conn = null;


            try
            {
                conn = _localStore.GetConnection();
                conn.Open();

                string strSQL =
                    "SELECT stock_takeid, time_start, upload_status, user_name from stocktake where time_finished IS NULL and upload_status=0 ORDER BY time_start DESC";

                SqlCeCommand cmd = new SqlCeCommand(strSQL, conn);
                SqlCeDataReader rdr = cmd.ExecuteReader();

                if (null != rdr)
                {
                    while (rdr.Read())
                    {
                        var lid = new ListViewItem(rdr["stock_takeid"].ToString()); // Stock ID

                        lid.SubItems.Add(rdr["time_start"].ToString()); //time_start

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

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Call the method to remove the selected stock data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (lstStockTakeList.SelectedIndices.Count > 0)
            {
                foreach (int i in lstStockTakeList.SelectedIndices)
                {
                    int stocktakeId = int.Parse(lstStockTakeList.Items[i].Text);

                    DialogResult scanResult = MessageBox.Show(Resources.Incomplete_Remove_Selected,
                                                               Resources.Srs_Title, MessageBoxButtons.YesNo,
                                                               MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (scanResult == DialogResult.Yes)
                    {
                        DeleteCurrentStockData(stocktakeId);
                        FillData();
                    }
                }
            }
        }


        /// <summary>
        /// Remove the selected stock take data
        /// </summary>
        /// <param name="stocktake_id"></param>
        private void DeleteCurrentStockData(int stocktake_id)
        {
            SqlCeConnection conn = null;

            try
            {
                conn = _localStore.GetConnection();
                conn.Open();

                //Delete from the stocktake item details
                string strSQL = "DELETE FROM stocktake_itemdetails WHERE stocktake_id = @stocktake_id";
                //Debug.Write(strSQL);

                using (SqlCeCommand cmd = new SqlCeCommand(strSQL, conn))
                {
                    cmd.Parameters.Add("@stocktake_id", stocktake_id);
                    cmd.ExecuteNonQuery();
                }

                //Now delete from the stocktake 
                strSQL = "DELETE FROM stocktake WHERE stock_takeid = @stocktake_id";

                using (var cmd = new SqlCeCommand(strSQL, conn))
                {
                    cmd.Parameters.Add("@stocktake_id", stocktake_id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlCeException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Remove all the uncompleted stock data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            DialogResult scanResult = MessageBox.Show(Resources.Uncompleted_RemoveAll, Resources.Srs_Title,
                                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                       MessageBoxDefaultButton.Button1);
            if (scanResult == DialogResult.Yes)
            {
                foreach (ListViewItem item in lstStockTakeList.Items)
                {
                    int stocktakeId = int.Parse(item.Text);
                    DeleteCurrentStockData(stocktakeId);
                }
                FillData();
            }
        }
    }
}