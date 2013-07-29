namespace Srs.Mobile.UI
{
    partial class FormUncompleted
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
            this.chID = new System.Windows.Forms.ColumnHeader();
            this.chTimeStarted = new System.Windows.Forms.ColumnHeader();
            this.chUserName = new System.Windows.Forms.ColumnHeader();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstStockTakeList
            // 
            this.lstStockTakeList.Columns.Add(this.chID);
            this.lstStockTakeList.Columns.Add(this.chTimeStarted);
            this.lstStockTakeList.Columns.Add(this.chUserName);
            this.lstStockTakeList.FullRowSelect = true;
            this.lstStockTakeList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstStockTakeList.Location = new System.Drawing.Point(0, 0);
            this.lstStockTakeList.Name = "lstStockTakeList";
            this.lstStockTakeList.Size = new System.Drawing.Size(240, 191);
            this.lstStockTakeList.TabIndex = 8;
            this.lstStockTakeList.View = System.Windows.Forms.View.Details;
            // 
            // chID
            // 
            this.chID.Text = "StockTake ID";
            this.chID.Width = 60;
            // 
            // chTimeStarted
            // 
            this.chTimeStarted.Text = "Date/Time Started";
            this.chTimeStarted.Width = 130;
            // 
            // chUserName
            // 
            this.chUserName.Text = "Username";
            this.chUserName.Width = 60;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(70, 234);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(104, 31);
            this.btnBack.TabIndex = 9;
            this.btnBack.Text = "Close";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.Location = new System.Drawing.Point(3, 197);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(111, 31);
            this.btnRemoveSelected.TabIndex = 9;
            this.btnRemoveSelected.Text = "Remove Record";
            this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Location = new System.Drawing.Point(132, 197);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(105, 31);
            this.btnRemoveAll.TabIndex = 10;
            this.btnRemoveAll.Text = "Remove All";
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // FormUncompleted
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.btnRemoveAll);
            this.Controls.Add(this.btnRemoveSelected);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lstStockTakeList);
            this.Menu = this.mainMenu1;
            this.Name = "FormUncompleted";
            this.Text = "Incomplete Stock-takes";
            this.Load += new System.EventHandler(this.frmResumeStock_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstStockTakeList;
        private System.Windows.Forms.ColumnHeader chTimeStarted;
        private System.Windows.Forms.ColumnHeader chUserName;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.Button btnRemoveAll;
    }
}