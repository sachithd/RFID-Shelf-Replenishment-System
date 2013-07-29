namespace Srs.Mobile.Common
{

    internal class Users
    {
        private string _password = "";
        private string _username = "";
        private int _userType = -1;

        public Users(int userType, string username, string password)
        {
            _userType = userType;
            _username = username;
            _password = password;
        }


        public int Type
        {
            get { return _userType; }
            set { _userType = value; }
        }


        public string Name
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}