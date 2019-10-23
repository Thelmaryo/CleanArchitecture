using College.Entities.AccountContext.Entities;
using System;

namespace College.UseCases.AccountContext.Repositories
{
    public interface IUserRepository
    {
        public Guid Login(User user);
        public bool IsInRole(Guid userId, string role);
        public string GetSalt(string UserName);
    }
}
