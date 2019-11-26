using College.Infra.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace College.Helpers
{
    public class MSSQLConfiguration : IDBConfiguration
    {
        public string StringConnection => "Server=DESKTOP-23IN36H; database=College; User Id=sa; Password=123";
    }
}