using System;
using System.Data;
using System.Data.SQLite;
using log4net;
using log4net.Config;

namespace Concurs_Inot_WinForms.Repository;

public class DBUtils
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(DBUtils));
    
    private String ConnectionString;
    private static SQLiteConnection _connection;
    private static readonly object _lock = new object();

    public DBUtils(String url)
    {
        this.ConnectionString = url;
    }

    private void CreateConnection()
    {
        Log.Info("trying to connect to database ... ");
        _connection = new SQLiteConnection(this.ConnectionString);

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