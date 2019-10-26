using College.Infra.DataSource;

namespace College.Infra.Tests.DataSource
{
    public class DBConfiguration : IDBConfiguration
    {
        public string StringConnection => "Server=DESKTOP-23IN36H; database=College; User Id=sa; Password=123";
    }
}
