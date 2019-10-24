using System.Data;
using System.Data.SqlClient;

namespace College.Infra.DataSource
{
    public class MSSQLDB : IDB
    {
        SqlConnection DB;
        readonly string strcon;
        public MSSQLDB(IDBConfiguration config)
        {
            strcon = config.StringConnection;
        }
        public void Dispose()
        {
            if (DB.State == ConnectionState.Open)
                DB.Close();
            DB.Dispose();
        }

        public IDbConnection GetCon()
        {
            DB = new SqlConnection(strcon);
            return DB;
        }
    }
}
