using College.UseCases.Shared.Commands;

namespace College.UseCases.AccountContext.Inputs
{
    public class UserInputResult : ICommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public string Salt { get; set; }
    }
}
