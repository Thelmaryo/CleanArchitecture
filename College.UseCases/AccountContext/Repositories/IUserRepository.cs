using College.Entities.AccountContext.Entities;
using System;

namespace College.UseCases.AccountContext.Repositories
{
    public interface IUserRepository
    {
        Guid Login(User user);
        bool IsInRole(Guid userId, string role);
        string GetSalt(string UserName);
    }
}
