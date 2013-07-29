namespace Srs.Mobile.UI
{
    partial class FormReaderSettings
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
            this.pnlGen2TagReadParam = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numUpDownStartingQ = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblUnit = new System.Windows.Forms.Label();
            this.txtTxPower = new System.Windows.Forms.TextBox();
            this.lblTx = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pnlGen2TagReadParam.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGen2TagReadParam
            // 
            this.pnlGen2TagReadParam.BackColor = System.Drawing.SystemColors.GrayText;
            this.pnlGen2TagReadParam.Controls.Add(this.btnCancel);
            this.pnlGen2TagReadParam.Controls.Add(this.textBox1);
            this.pnlGen2TagReadParam.Controls.Add(this.btnOK);
            this.pnlGen2TagReadParam.Controls.Add(this.trackBar1);
            this.pnlGen2TagReadParam.Controls.Add(this.lblUnit);
            this.pnlGen2TagReadParam.Controls.Add(this.txtTxPower);
            this.pnlGen2TagReadParam.Controls.Add(this.lblTx);
            this.pnlGen2TagReadParam.Controls.Add(this.label5);
            this.pnlGen2TagReadParam.Controls.Add(this.label4);
            this.pnlGen2TagReadParam.Controls.Add(this.numUpDownStartingQ);
            this.pnlGen2TagReadParam.Controls.Add(this.label3);
            this.pnlGen2TagReadParam.Location = new System.Drawing.Point(3, 3);
            this.pnlGen2TagReadParam.Name = "pnlGen2TagReadParam";
            this.pnlGen2TagReadParam.Size = new System.Drawing.Size(234, 262);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(10, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(186, 30);
            this.label5.Text = "Choose higher value  if large number  of tags are expected in the field.";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 20);
            this.label4.Text = "Gen2 Tag Read Parameter :";
            // 
            // numUpDownStartingQ
            // 
            this.numUpDownStartingQ.Location = new System.Drawing.Point(95, 31);
            this.numUpDownStartingQ.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numUpDownStartingQ.Name = "numUpDownStartingQ";
            this.numUpDownStartingQ.ReadOnly = true;
            this.numUpDownStartingQ.Size = new System.Drawing.Size(88, 22);
            this.numUpDownStartingQ.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 20);
            this.label3.Tag = "Sets the number of slots in the first Inventory Round.The starting Q should be ch" +
                "osen higher for a larger expected number of tags in the field.";
            this.label3.Text = "Starting Q";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(138, 239);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 20);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(32, 239);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 20);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            // 
            // lblUnit
            // 
            this.lblUnit.Location = new System.Drawing.Point(189, 141);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(21, 18);
            this.lblUnit.Text = "DB";
            // 
            // txtTxPower
            // 
            this.txtTxPower.Location = new System.Drawing.Point(80, 138);
            this.txtTxPower.MaxLength = 3;
            this.txtTxPower.Name = "txtTxPower";
            this.txtTxPower.Size = new System.Drawing.Size(103, 21);
            this.txtTxPower.TabIndex = 13;
            this.txtTxPower.Text = "30";
            // 
            // lblTx
            // 
            this.lblTx.Location = new System.Drawing.Point(7, 139);
            this.lblTx.Name = "lblTx";
            this.lblTx.Size = new System.Drawing.Size(82, 20);
            this.lblTx.Text = "Tx Power:";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(5, 165);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(226, 45);
            this.trackBar1.TabIndex = 15;
            this.trackBar1.Value = 3;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 1F, System.Drawing.FontStyle.Regular);
            this.textBox1.Location = new System.Drawing.Point(5, 108);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(223, 1);
            this.textBox1.TabIndex = 19;
            this.textBox1.Text = "textBox1";
            // 
            // frmReaderSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.pnlGen2TagReadParam);
            this.Menu = this.mainMenu1;
            this.Name = "frmReaderSettings";
            this.Text = "RFID Reader Settings";
            this.pnlGen2TagReadParam.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGen2TagReadParam;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numUpDownStartingQ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.TextBox txtTxPower;
        private System.Windows.Forms.Label lblTx;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox textBox1;
    }
}