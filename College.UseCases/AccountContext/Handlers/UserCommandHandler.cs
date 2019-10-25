using College.Entities.AccountContext.Entities;
using College.UseCases.AccountContext.Inputs;
using College.UseCases.AccountContext.Repositories;
using College.UseCases.Shared.Commands;
using College.UseCases.Shared.Result;

namespace College.UseCases.AccountContext.Handlers
{
    public class UserCommandHandler : ICommandHandler<UserInputLogin>
    {
        private readonly IUserRepository _UREP;

        public UserCommandHandler(IUserRepository UREP)
        {
            _UREP = UREP;
        }

        public ICommandResult Handle(UserInputLogin command)
        {
            var user = new User(command.UserName, command.Password);
            _UREP.Login(user);
            //TO DO Authentication

            var result = new StandardResult();
            result.AddRange(user.Notifications);
            if (result.Notifications.Count == 0)
                result.Notifications.Add("Success", "O Acadêmico foi salvo");
            return result;
        }

        public ICommandResult Handle(UserInputIsInRole command)
        {
            var userRole = _UREP.IsInRole(command.UserId, command.Role);

            var result = new StandardResult();
            if (userRole != null)
                result.Notifications.Add("Success", "O Usuario pertence a role");
            else
                result.Notifications.Add("Error", "O Usuario não pertence a role");
            throw new System.NotImplementedException();
        }

        public ICommandResult Handle(UserInputGetSalt command)
        {
            var salt = _UREP.GetSalt(command.UserName);

            var result = new StandardResult();
            if (!string.IsNullOrEmpty(salt))
                result.Notifications.Add("Success", "O Sal obtido com sucesso");
            else
                result.Notifications.Add("Error", "Erro ao buscar Sal");
            throw new System.NotImplementedException();
        }
    }
}