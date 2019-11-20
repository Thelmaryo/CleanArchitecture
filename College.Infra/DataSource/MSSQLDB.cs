using System;
using System.Data;
using System.Data.SqlClient;

namespace College.Infra.DataSource
{
    public class MSSQLDB : IDB, IDisposable
    {
        SqlConnection DB;
        readonly string strcon;
        public MSSQLDB(IDBConfiguration config)
        {
            strcon = config.StringConnection;
        }
        public IDbConnection GetCon()
        {
            DB = new SqlConnection(strcon);
            return DB;
        }

        public void Dispose()
        {
            DB.Close();
            DB.Dispose();
        }
    }
}
