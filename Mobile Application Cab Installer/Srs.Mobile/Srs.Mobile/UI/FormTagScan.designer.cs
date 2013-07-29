namespace Srs.Mobile.UI
{
    partial class FormTagScan
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.lstRFIDTags = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnComplete = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblAisleNo = new System.Windows.Forms.Label();
            this.btnFill = new System.Windows.Forms.Button();
            this.lblTitleStatus = new System.Windows.Forms.Label();
            this.lblTStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(97, 25);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(94, 20);
            // 
            // lstRFIDTags
            // 
            this.lstRFIDTags.Columns.Add(this.columnHeader1);
            this.lstRFIDTags.Location = new System.Drawing.Point(0, 48);
            this.lstRFIDTags.Name = "lstRFIDTags";
            this.lstRFIDTags.Size = new System.Drawing.Size(240, 145);
            this.lstRFIDTags.TabIndex = 1;
            this.lstRFIDTags.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tag ID";
            this.columnHeader1.Width = 235;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(165, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(72, 20);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnComplete
            // 
            this.btnComplete.Location = new System.Drawing.Point(123, 230);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(114, 35);
            this.btnComplete.TabIndex = 3;
            this.btnComplete.Text = "Complete";
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(3, 230);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(114, 35);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblAisleNo
            // 
            this.lblAisleNo.Location = new System.Drawing.Point(3, 203);
            this.lblAisleNo.Name = "lblAisleNo";
            this.lblAisleNo.Size = new System.Drawing.Size(234, 20);
            this.lblAisleNo.Text = "Aisle No: ";
            // 
            // btnFill
            // 
            this.btnFill.Location = new System.Drawing.Point(3, 3);
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size(72, 20);
            this.btnFill.TabIndex = 5;
            this.btnFill.Text = "Temp Fill";
            this.btnFill.Visible = false;
            this.btnFill.Click += new System.EventHandler(this.btnFill_Click);
            // 
            // lblTitleStatus
            // 
            this.lblTitleStatus.Location = new System.Drawing.Point(3, 26);
            this.lblTitleStatus.Name = "lblTitleStatus";
            this.lblTitleStatus.Size = new System.Drawing.Size(88, 20);
            this.lblTitleStatus.Text = "Reader Status: ";
            // 
            // lblTStatus
            // 
            this.lblTStatus.Location = new System.Drawing.Point(84, 3);
            this.lblTStatus.Name = "lblTStatus";
            this.lblTStatus.Size = new System.Drawing.Size(75, 20);
            this.lblTStatus.Visible = false;
            // 
            // FormTagScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.btnFill);
            this.Controls.Add(this.lblAisleNo);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnComplete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lstRFIDTags);
            this.Controls.Add(this.lblTitleStatus);
            this.Controls.Add(this.lblTStatus);
            this.Controls.Add(this.lblStatus);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "FormTagScan";
            this.Text = "RFID Stock Take - Aisle Scan";
            this.Load += new System.EventHandler(this.frmTagScan_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ListView lstRFIDTags;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblAisleNo;
        private System.Windows.Forms.Button btnFill;
        private System.Windows.Forms.Label lblTitleStatus;
        private System.Windows.Forms.Label lblTStatus;
        
    }
}