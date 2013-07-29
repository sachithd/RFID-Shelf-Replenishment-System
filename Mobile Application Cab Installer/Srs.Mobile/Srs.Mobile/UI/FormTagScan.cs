using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Srs.Mobile.Common;
using Srs.Mobile.Data;
using Srs.Mobile.Properties;
using Symbol.RFID2;
using Symbol.ResourceCoordination;
using Logger = Srs.Mobile.Common.Logger;
using TriggerEventArgs = Symbol.ResourceCoordination.TriggerEventArgs;
using TriggerState = Symbol.ResourceCoordination.TriggerState;


namespace Srs.Mobile.UI
{
    /// <summary>
    /// Some parts of this class were referred and taken from Motorola MC9090 RFID Demo application source code
    /// </summary>
    public partial class FormTagScan : Form
    {
        #region Delegates
            public delegate void DisplayAutoOutputHandler(TagEventArgs args);
            public delegate void DisplayOutputHandler(string text, ReaderEventArgs args);
        #endregion

        #region RFID_Variables
        
            private readonly IRFIDReader _deviceReader = Reader.DeviceReader;
            private Trigger _myTrigger;
            private Trigger.TriggerEventHandler _myTriggerHandler;
            private DisplayAutoOutputHandler _displayAutoHandler;
            private ReaderModel _mReaderModel = Reader.ReaderModel;
        
        #endregion

        private int _aisleId;
        private string _aisleNo;
        private LocalStore _localStore;
        private Logger _logger;
        private long _stocktakeId;
        

        /// <summary>
        /// Initialize the form including the RFID reader
        /// </summary>
        internal FormTagScan()
        {
            InitializeComponent();
            try
            {
                CreateRFIDReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Srs_Title);
            }
            KeyPreview = true;
            KeyUp += OnKeyUp;
        }

        public string ReturnAisleNumber { get; set; }

        public string SetAisleNo
        {
            set
            {
                _aisleNo = value;
                lblAisleNo.Text = "";
                lblAisleNo.Text = "Aisle No :" + value;
            }
        }

        public string SetAisleId
        {
            set
            {
                //string aid = value;
                //Debug.Write(value)
                _aisleId = Convert.ToInt32(value);
            }
        }

        public string SetAisleDescription
        {
            set { lblAisleNo.Text = lblAisleNo.Text + " " + value; }
        }

        public long SetStocktakeId
        {
            set { _stocktakeId = value; }
        }

        /// <summary>
        /// Create the RFID reader
        /// </summary>
        private void CreateRFIDReader()
        {
            try
            {
                _displayAutoHandler = DisplayAutoTags; 
                SetupTriggerResource();
                SetupEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Srs_Title);
                //Application.Exit();
            }
        }

        /// <summary>
        /// Setup the trigger resources
        /// </summary>
        private void SetupTriggerResource()
        {
            try
            {
                //Creates a trigger object
                _myTrigger = Reader.GetTrigger;

                //Creates an event handler and attach a handler method for trigger
                _myTriggerHandler = MyTriggerH;

                _myTrigger.Stage2Notify += _myTriggerHandler;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.Failed_Trigger + ex.Message, Resources.Srs_Title);

            }
        }

        /// <summary>
        /// Setup the reader events
        /// </summary>
        private void SetupEvents()
        {
            _deviceReader.TagEvent += DeviceReaderTagEvent;
        }

        /// <summary>
        /// deviceReader_TagEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void DeviceReaderTagEvent(object sender, ReaderEventArgs args)
        {
            try
            {
                if (args != null)
                {
                    lstRFIDTags.Invoke(_displayAutoHandler, (TagEventArgs) args);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
               // MessageBox.Show(ex.Message, Resources.Srs_Title);
            }
        }

     
        private void MyTriggerH(object sender, TriggerEventArgs evt) //####
        {
            if (_deviceReader.ReaderStatus == ReaderStatus.ONLINE)
            {

              
                try
                {
                    if (evt.NewState == TriggerState.STAGE2)
                    {
                        _deviceReader.ReadMode = ReadMode.AUTONOMOUS;

                        Debug.Write("Triggering");
                        lblTStatus.Text = "Triggering";
                    }
                    else if (evt.NewState == TriggerState.RELEASED)
                    {
                        _deviceReader.ReadMode = ReadMode.ONDEMAND;
                        lblTStatus.Text = "OnDemand";

                    }
                }
                catch (Exception)
                {
                    //executionStatus = "Failed";
                }
            } 
        }

        /// <summary>
        /// DisplayAutoTags processes and displays tag data received from deviceReaderTagEvent handler
        /// 
        /// Read tags when the trigger is pressed
        /// </summary>
        private void DisplayAutoTags(TagEventArgs args)
        {
            TagEventArgs tagArgs = null;
            IEnumerable<IRFIDTag> tags = null;

            int count = 0;

            tagArgs = args;

            tags = tagArgs.Reader.Tags;

            try
            {
                foreach (IRFIDTag tag in tags)
                {
                    if (++count%50 == 0)
                    {
                        Application.DoEvents(); // give gui thread a chance to response user operation
                    }

                    byte[] tagId = tag.TagID;

                    try
                    {
                        if (tagId == null || tagId.Length == 0)
                        {
                            return;
                        }

                        string strTagId = null;

                        int ln = tagId.Length;

                        int j;
                        for (j = 0; j < ln; j++)
                        {
                            strTagId += tagId[j].ToString("X2");
                        }


                        var lvItem = new ListViewItem(new[] {strTagId});


                        if (!IsInCollection(lvItem))
                        {
                            lstRFIDTags.Items.Insert(0, lvItem).Selected = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Error:{0}", ex.Message), Resources.Srs_Title);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("DisplayText:{0}", ex.Message), Resources.Srs_Title);
            }
        }

        /// <summary>
        /// Only read and add tag id once to the listview. Checks if tag is already in the listview
        /// </summary>
        /// <param name="lvi"></param>
        /// <returns></returns>
        private bool IsInCollection(ListViewItem lvi)
        {
            foreach (ListViewItem item in lstRFIDTags.Items)
            {
                bool subItemEqualFlag = true;
                for (int i = 0; i < item.SubItems.Count; i++)
                {
                    string sub1 = item.SubItems[i].Text;
                    string sub2 = lvi.SubItems[i].Text;
                    if (sub1 != sub2)
                    {
                        subItemEqualFlag = false;
                    }
                }
                if (subItemEqualFlag)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// RFID reader is connected succesully 
        /// </summary>
        private void ReaderConnected()
        {
            lblStatus.Text = "Connected";
            lblTStatus.ForeColor = Color.Green;
            _deviceReader.ReadMode = ReadMode.ONDEMAND;
        }

        /// <summary>
        /// RFID reader is not connected
        /// </summary>
        private void ReaderDisconnected()
        {
            lblStatus.Text = "Disconnected";
            lblTStatus.ForeColor = Color.Red;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(Resources.Tag_Scan_Back, Resources.Srs_Title, MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstRFIDTags.Items.Clear();
        }

        private void frmTagScan_Load(object sender, EventArgs e)
        {
            _logger = new Logger();
            _localStore = new LocalStore();
            LoadRFIDReader();
            lstRFIDTags.Items.Clear(); //Clears ths list view
        }

        /// <summary>
        /// Load the RFID reader
        /// </summary>
        private void LoadRFIDReader()
        {
            try
            {
                if (_deviceReader.ReaderStatus != ReaderStatus.ONLINE)
                    _deviceReader.Connect();

                if (_deviceReader.ReaderStatus == ReaderStatus.ONLINE)
                    ReaderConnected();

                else
                    ReaderDisconnected();
            }
            catch
            {
                MessageBox.Show(Resources.Reader_Not_Initialized, Resources.Srs_Title);
                ReaderDisconnected();
                //Application.Exit();
            }
        }

        /// <summary>
        /// Complete the stock take of the current aisle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnComplete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(Resources.Tag_Scan_Complete, Resources.Srs_Title, MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                //Save the record to the database
                SaveDBAisleCount();

                //Go back to the previous screen
                ReturnAisleNumber = _aisleNo;
                //this.Close();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        /// <summary>
        /// Save the data to the local database
        /// </summary>
        private void SaveDBAisleCount()
        {
            SqlCeConnection conn = null;

            try
            {
                conn = _localStore.GetConnection();
                conn.Open();
                string strSQL =
                    "INSERT INTO stocktake_itemdetails(stocktake_id,tag_id,aisle_number,aisle_id) VALUES(@stocktake_id,@tag_id,@aisle_number,@aisle_id)";

                foreach (ListViewItem item in lstRFIDTags.Items)
                {
                    string rfidTag = item.Text;
                    using (var cmd = new SqlCeCommand(strSQL, conn))
                    {
                        cmd.Parameters.Add("@stocktake_id", _stocktakeId);
                        cmd.Parameters.Add("@tag_id", rfidTag);
                        cmd.Parameters.Add("@aisle_number", _aisleNo);
                        cmd.Parameters.Add("@aisle_id", _aisleId);
                        cmd.ExecuteNonQuery();
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


        /**
         * Temp fill the list box until reader is fixed. 
         */

        private void btnFill_Click(object sender, EventArgs e)
        {
            string strTagId = GenerateRandomNumber(10);

            var lvItem = new ListViewItem(new[] {strTagId});

            if (!IsInCollection(lvItem))
            {
                lstRFIDTags.Items.Insert(0, lvItem).Selected = true;
            }
        }


        private void frmTagScan_KeyDown(object sender, KeyEventArgs e)
        {
            //ListBox1.Items.Add(e.KeyCode);
            if (e.KeyCode == Keys.S)
            {
                e.Handled = false;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch ((int) e.KeyCode)
            {
                case 83:
                    string strTagId = GenerateRandomNumber(10);

                    var lvItem = new ListViewItem(new[] {strTagId});

                    if (!IsInCollection(lvItem))
                    {
                        lstRFIDTags.Items.Insert(0, lvItem).Selected = true;
                    }
                    break;
            }
        }


        private static string GenerateRandomNumber(int size)
        {
            Random random = new Random((int) DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            string s;
            for (int i = 0; i < size; i++)
            {
                s = Convert.ToString(Convert.ToInt32(Math.Floor(26*random.NextDouble() + 65)));
                builder.Append(s);
            }

            return builder.ToString();
        }
    }
}