namespace Srs.Mobile.UI
{
    partial class FormMenu
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
            this.btnNewStockTake = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnUploadFailed = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblLoggedIn = new System.Windows.Forms.Label();
            this.lblUserStatus = new System.Windows.Forms.Label();
            this.btnResumeStockTake = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNewStockTake
            // 
            this.btnNewStockTake.Location = new System.Drawing.Point(18, 15);
            this.btnNewStockTake.Name = "btnNewStockTake";
            this.btnNewStockTake.Size = new System.Drawing.Size(207, 38);
            this.btnNewStockTake.TabIndex = 0;
            this.btnNewStockTake.Text = "New Stock-take";
            this.btnNewStockTake.Click += new System.EventHandler(this.btnNewStockTake_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(18, 107);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(207, 38);
            this.btnHistory.TabIndex = 0;
            this.btnHistory.Text = "View History";
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnUploadFailed
            // 
            this.btnUploadFailed.Location = new System.Drawing.Point(18, 152);
            this.btnUploadFailed.Name = "btnUploadFailed";
            this.btnUploadFailed.Size = new System.Drawing.Size(207, 38);
            this.btnUploadFailed.TabIndex = 0;
            this.btnUploadFailed.Text = "Retry Upload";
            this.btnUploadFailed.Click += new System.EventHandler(this.btnUploadFailed_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(18, 197);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(207, 38);
            this.btnLogout.TabIndex = 0;
            this.btnLogout.Text = "Log Out";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblLoggedIn
            // 
            this.lblLoggedIn.ForeColor = System.Drawing.Color.Blue;
            this.lblLoggedIn.Location = new System.Drawing.Point(107, 248);
            this.lblLoggedIn.Name = "lblLoggedIn";
            this.lblLoggedIn.Size = new System.Drawing.Size(123, 20);
            this.lblLoggedIn.Text = " ";
            // 
            // lblUserStatus
            // 
            this.lblUserStatus.Location = new System.Drawing.Point(0, 248);
            this.lblUserStatus.Name = "lblUserStatus";
            this.lblUserStatus.Size = new System.Drawing.Size(104, 20);
            this.lblUserStatus.Text = "Logged In User: ";
            // 
            // btnResumeStockTake
            // 
            this.btnResumeStockTake.Location = new System.Drawing.Point(18, 61);
            this.btnResumeStockTake.Name = "btnResumeStockTake";
            this.btnResumeStockTake.Size = new System.Drawing.Size(207, 38);
            this.btnResumeStockTake.TabIndex = 2;
            this.btnResumeStockTake.Text = "Incomplete Stock-take";
            this.btnResumeStockTake.Click += new System.EventHandler(this.btnResumeStockTake_Click);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.btnResumeStockTake);
            this.Controls.Add(this.lblUserStatus);
            this.Controls.Add(this.lblLoggedIn);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnUploadFailed);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.btnNewStockTake);
            this.Menu = this.mainMenu1;
            this.Name = "frmMenu";
            this.Text = "Main Menu";
            this.Load += new System.EventHandler(this.frmMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNewStockTake;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnUploadFailed;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblLoggedIn;
        private System.Windows.Forms.Label lblUserStatus;
        private System.Windows.Forms.Button btnResumeStockTake;
    }
}