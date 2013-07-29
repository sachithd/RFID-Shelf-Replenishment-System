using System;
using System.Windows.Forms;
using Srs.Mobile.Common;

namespace Srs.Mobile.UI
{
    public partial class FormLogin : Form
    {
        private Logger _logger;
        private Users _user;
        private WebServices _webService;

        internal FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() != String.Empty)
            {
                lblLoginError.Text = "Authenticating User. Please wait!";
                Application.DoEvents();
                btnLogin.Enabled = false;
                //Check for valid user name and password and create new instance of user class
                int loginSucess = _webService.LoginSucess(txtUsername.Text, txtPassword.Text);
                //MessageBox.Show(loginsucess.ToString(),"sss");

                //Predefined Mobile user roles 2,3,5
                if (loginSucess > 0 && loginSucess < 99)
                {
                    _user = new Users(loginSucess, txtUsername.Text, txtPassword.Text );

                    _logger.WriteLog("Loging", _user.Name); //Write a login record to the log table    

                    
                    if (txtUsername.Text.Trim().Equals("deviceadmin"))
                    {
                        var settingsForm = new FormReaderSettings();
                        settingsForm.ShowDialog();
                        lblLoginError.Text = String.Empty;
                    }
                    else
                    {
                        var aisleForm = new FormMenu(_user);
                        aisleForm.ShowDialog();
                        lblLoginError.Text = "";
                    }
                }
                else if (loginSucess == 99)
                {
                    lblLoginError.Text =
                        "Unable to connect to the server. Please check the internet connection and try again";
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    txtUsername.Focus();
                   
                }
                else
                {
                    lblLoginError.Text = "Invalid Login. Please try again";
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    txtUsername.Focus();
                }
                btnLogin.Enabled = true;
            }
            else
            {
                lblLoginError.Text = "Please enter username and password";
            }
        }

        /// <summary>
        /// Close and go back to the previous screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            _logger = new Logger();
            _webService = new WebServices();
        }
    }
}