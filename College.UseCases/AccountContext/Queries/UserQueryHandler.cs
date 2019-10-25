using College.Entities.AccountContext.Entities;
using College.UseCases.AccountContext.Inputs;
using College.UseCases.AccountContext.Repositories;
using College.UseCases.AccountContext.Results;
using College.UseCases.Services;
using College.UseCases.Shared.Commands;

namespace College.UseCases.AccountContext.Queries
{
    public class UserQueryHandler : IQueryHandler<UserInputLogin, UserResultQueryLogin>
    {
        readonly IUserRepository _UREP;
        readonly IEncryptor _encryptor;

        public UserQueryHandler(IUserRepository UREP, IEncryptor encryptor)
        {
            _UREP = UREP;
            _encryptor = encryptor;
        }
        public UserResultQueryLogin Handle(UserInputLogin command)
        {
            var result = new UserResultQueryLogin();
            var password = _encryptor.Encrypt(command.UserName, command.Password);
            var user = new User(command.UserName, password);

            result.UserId = _UREP.Login(user);

            return result;
        }
        public UserResultQueryIsInRole Handle(UserInputIsInRole command)
        {
            var result = new UserResultQueryIsInRole();
            result.IsInRole = _UREP.IsInRole(command.UserId, command.Role);

            return result;
        }
        public UserResultQueryGetSalt Handle(UserInputGetSalt command)
        {
            var result = new UserResultQueryGetSalt();
            result.Salt = _UREP.GetSalt(command.UserName);

            return result;
        }
    }
}
