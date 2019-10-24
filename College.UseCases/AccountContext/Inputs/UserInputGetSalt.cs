using College.UseCases.Shared.Commands;

namespace College.UseCases.AccountContext.Inputs
{
    public class UserInputGetSalt : ICommand
    {
        public string UserName { get; set; }
    }
}
