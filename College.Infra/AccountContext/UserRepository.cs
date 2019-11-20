using College.Entities.AccountContext.Entities;
using College.Infra.DataSource;
using College.UseCases.AccountContext.Repositories;
using Dapper;
using System;

namespace College.Infra.AccountContext
{
    public class UserRepository : IUserRepository
    {
        IDB _db;
        string sql;
        public UserRepository(IDB db)
        {
            _db = db;
        }
        public Guid Login(User user)
        {
            using (var db = _db.GetCon())
            {
                sql = "SELECT Id FROM [User] WHERE UserName = @UserName AND Password = @Password AND Active = 1";
                var userId = db.QuerySingleOrDefault<Guid>(sql, param: new { user.UserName, user.Password });
                return userId;
            }
        }

        public bool IsInRole(Guid userId, string role)
        {
            using (var db = _db.GetCon())
            {
                sql = "SELECT COUNT(*) AS Result FROM [User] WHERE Id = @Id AND Role = @Role";
                var isInRole = db.QuerySingleOrDefault<bool>(sql, param: new { Id = userId, Role = role });
                return isInRole;
            }
        }

        public string GetSalt(string UserName)
        {
            using (var db = _db.GetCon())
            {
                sql = "SELECT Salt FROM [User] WHERE UserName = @UserName";
                var salt = db.QuerySingleOrDefault<string>(sql, param: new { UserName });
                return salt;
            }
        }
    }
}
