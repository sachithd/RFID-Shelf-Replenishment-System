namespace Srs.Mobile.UI
{
    partial class FormAisle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.lstAisleList = new System.Windows.Forms.ListView();
            this.chAisleNumber = new System.Windows.Forms.ColumnHeader();
            this.chDescription = new System.Windows.Forms.ColumnHeader();
            this.chStatus = new System.Windows.Forms.ColumnHeader();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(9, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(218, 20);
            this.lblTitle.Text = "Select the Aisle Number and Click Next";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(131, 184);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(106, 38);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(3, 184);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(106, 38);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.BackColor = System.Drawing.Color.LightGreen;
            this.btnUpload.Location = new System.Drawing.Point(7, 228);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(227, 37);
            this.btnUpload.TabIndex = 4;
            this.btnUpload.Text = "Complete Stocktake && Upload";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // lstAisleList
            // 
            this.lstAisleList.Columns.Add(this.chAisleNumber);
            this.lstAisleList.Columns.Add(this.chDescription);
            this.lstAisleList.Columns.Add(this.chStatus);
            this.lstAisleList.FullRowSelect = true;
            this.lstAisleList.Location = new System.Drawing.Point(7, 25);
            this.lstAisleList.Name = "lstAisleList";
            this.lstAisleList.Size = new System.Drawing.Size(223, 144);
            this.lstAisleList.TabIndex = 5;
            this.lstAisleList.View = System.Windows.Forms.View.Details;
            // 
            // chAisleNumber
            // 
            this.chAisleNumber.Text = "Aisle No";
            this.chAisleNumber.Width = 65;
            // 
            // chDescription
            // 
            this.chDescription.Text = "Description";
            this.chDescription.Width = 129;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            this.chStatus.Width = 106;
            // 
            // FormAisle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.lstAisleList);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lblTitle);
            this.Menu = this.mainMenu1;
            this.Name = "FormAisle";
            this.Text = "Shop Floor Aisle List ";
            this.Load += new System.EventHandler(this.FormAisle_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.ListView lstAisleList;
        private System.Windows.Forms.ColumnHeader chAisleNumber;
        private System.Windows.Forms.ColumnHeader chDescription;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.MainMenu mainMenu1;
    }
}