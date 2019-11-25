using College.Infra.DataSource;

namespace College.UseCases.Tests.DataSource
{
    public class DBConfiguration : IDBConfiguration
    {
        public string StringConnection => "Server=DESKTOP-N1T1LL1; database=College; User Id=sa; Password=123";
    }
}
