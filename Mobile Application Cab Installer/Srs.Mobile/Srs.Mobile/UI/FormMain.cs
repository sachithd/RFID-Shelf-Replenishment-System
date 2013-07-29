using System;
using System.ComponentModel;
using System.Windows.Forms;
using Srs.Mobile.Common;

namespace Srs.Mobile.UI
{
    public partial class FormMain : Form
    {
        internal FormMain()
        {
            InitializeComponent();
            Closing += FormMain_FormClosing;
        }

        private void FormMain_FormClosing(object sender, CancelEventArgs e)
        {
            Reader.Shutdown();
            Application.Exit();
        }

        /// <summary>
        /// Show the login form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            var loginForm = new FormLogin();
            loginForm.ShowDialog();
        }

        /// <summary>
        /// Call the method shutdown to close the RFID reader
        /// Exit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Reader.Shutdown();
            Application.Exit();
        }


    }
}