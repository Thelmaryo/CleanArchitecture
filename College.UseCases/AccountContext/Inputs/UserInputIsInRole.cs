using College.UseCases.Shared.Commands;
using System;

namespace College.UseCases.AccountContext.Inputs
{
    public class UserInputIsInRole : ICommand
    {
        public Guid UserId { get; set; }
        public string Role { get; set; }
    }
}
