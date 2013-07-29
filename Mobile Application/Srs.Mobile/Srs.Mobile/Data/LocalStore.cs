using System;
using System.Data.SqlServerCe;
using System.IO;
using System.Reflection;

namespace Srs.Mobile.Data
{
    internal class LocalStore
    {
        private readonly string _dbConnectionString;
        private SqlCeConnection _connection;

        internal LocalStore()
        {
            _dbConnectionString = String.Concat("Data Source=",
                                               (Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase)),
                                               @"\Data\srdb.sdf; Persist Security Info=False");
        }

        internal SqlCeConnection GetConnection()
        {
            return _connection ?? (_connection = new SqlCeConnection(_dbConnectionString));
        }
    }
}