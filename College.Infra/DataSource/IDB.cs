using System;
using System.Data;

namespace College.Infra.DataSource
{
    public interface IDB : IDisposable
    {
        IDbConnection GetCon();
    }
}
