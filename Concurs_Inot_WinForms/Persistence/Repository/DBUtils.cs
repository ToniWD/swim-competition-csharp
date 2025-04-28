using System;
using System.Data.SQLite;
using log4net;

namespace Persistence.Repository
{
    public class DBUtils
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DBUtils));

        private string ConnectionString;
        private static SQLiteConnection _connection;
        private static readonly object _lock = new object();
        
        public DBUtils(string url)
        {
            ConnectionString = url;
        }

        private void CreateConnection()
        {
            Log.Info("trying to connect to database ... ");
            _connection = new SQLiteConnection(ConnectionString);

            try
            {
                _connection.Open();
            }
            catch (SQLiteException e)
            {
                Log.Error(e.Message);
                throw new Exception("Could not connect to database");
            }
        }

        public SQLiteConnection GetConnection()
        {
            lock (_lock)
            {
                if (_connection == null || _connection.State != System.Data.ConnectionState.Open)
                {
                    CreateConnection();
                }
                return _connection;
            }
        }
    }
}