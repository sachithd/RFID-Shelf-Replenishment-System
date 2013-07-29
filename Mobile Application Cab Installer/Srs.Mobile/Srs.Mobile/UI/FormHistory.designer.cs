namespace Srs.Mobile.UI
{
    partial class FormHistory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.lstStockTakeList = new System.Windows.Forms.ListView();
            this.chTimeStarted = new System.Windows.Forms.ColumnHeader();
            this.chTimeFinished = new System.Windows.Forms.ColumnHeader();
            this.chStatus = new System.Windows.Forms.ColumnHeader();
            this.chUserName = new System.Windows.Forms.ColumnHeader();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnFilter = new System.Windows.Forms.Button();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblNotUploaded = new System.Windows.Forms.Label();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstStockTakeList
            // 
            this.lstStockTakeList.Columns.Add(this.chTimeStarted);
            this.lstStockTakeList.Columns.Add(this.chTimeFinished);
            this.lstStockTakeList.Columns.Add(this.chStatus);
            this.lstStockTakeList.Columns.Add(this.chUserName);
            this.lstStockTakeList.FullRowSelect = true;
            this.lstStockTakeList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstStockTakeList.Location = new System.Drawing.Point(3, 59);
            this.lstStockTakeList.Name = "lstStockTakeList";
            this.lstStockTakeList.Size = new System.Drawing.Size(234, 151);
            this.lstStockTakeList.TabIndex = 7;
            this.lstStockTakeList.View = System.Windows.Forms.View.Details;
            // 
            // chTimeStarted
            // 
            this.chTimeStarted.Text = "Date/Time Started";
            this.chTimeStarted.Width = 130;
            // 
            // chTimeFinished
            // 
            this.chTimeFinished.Text = "Date/Time Completed";
            this.chTimeFinished.Width = 130;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Upload Status";
            this.chStatus.Width = 106;
            // 
            // chUserName
            // 
            this.chUserName.Text = "Username";
            this.chUserName.Width = 60;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(51, 9);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(92, 22);
            this.dtpFrom.TabIndex = 9;
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(51, 31);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(92, 22);
            this.dtpTo.TabIndex = 9;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(155, 9);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(73, 20);
            this.btnFilter.TabIndex = 10;
            this.btnFilter.Text = "Filter";
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // lblFrom
            // 
            this.lblFrom.Location = new System.Drawing.Point(3, 11);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(45, 20);
            this.lblFrom.Text = "From";
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(3, 31);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(42, 20);
            this.lblTo.Text = "To";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(169, 234);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(68, 31);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblNotUploaded
            // 
            this.lblNotUploaded.BackColor = System.Drawing.Color.Yellow;
            this.lblNotUploaded.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblNotUploaded.Location = new System.Drawing.Point(3, 213);
            this.lblNotUploaded.Name = "lblNotUploaded";
            this.lblNotUploaded.Size = new System.Drawing.Size(234, 18);
            this.lblNotUploaded.Text = "Not Uploaded";
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(155, 33);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(73, 20);
            this.btnAll.TabIndex = 18;
            this.btnAll.Text = "All";
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(3, 234);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(68, 31);
            this.btnRemove.TabIndex = 23;
            this.btnRemove.Text = "Remove";
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Location = new System.Drawing.Point(77, 234);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(86, 31);
            this.btnRemoveAll.TabIndex = 24;
            this.btnRemoveAll.Text = "Remove All";
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // FormHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.lblNotUploaded);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lstStockTakeList);
            this.Menu = this.mainMenu1;
            this.Name = "FormHistory";
            this.Text = "View Stock Take History";
            this.Load += new System.EventHandler(this.frmHistory_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstStockTakeList;
        private System.Windows.Forms.ColumnHeader chTimeStarted;
        private System.Windows.Forms.ColumnHeader chTimeFinished;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chUserName;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblNotUploaded;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnRemoveAll;
    }
}