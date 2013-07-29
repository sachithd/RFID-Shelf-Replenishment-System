using System;
using System.Data.SqlServerCe;
using Srs.Mobile.Data;

namespace Srs.Mobile.Common
{
    internal class Logger
    {
        private readonly LocalStore _localStore;

        internal Logger()
        {
            _localStore = new LocalStore();
        }

        /// <summary>
        /// Insert a record to the log table
        /// </summary>
        internal void WriteLog(string message, string uname)
        {
            SqlCeConnection conn = null;

            DateTime dt = DateTime.Now;

            try
            {
                conn = _localStore.GetConnection();
                //conn.Open();
                string strSQL = "INSERT INTO log(username, action, date) VALUES(@user_name,@user_action,@datetime)";
                using (var cmd = new SqlCeCommand(strSQL, conn))
                {
                    cmd.Parameters.Add("@user_name", uname);
                    cmd.Parameters.Add("@user_action", message);
                    cmd.Parameters.Add("@datetime", dt);
                    conn.Open();
                    cmd.ExecuteNonQuery();
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
    }
}