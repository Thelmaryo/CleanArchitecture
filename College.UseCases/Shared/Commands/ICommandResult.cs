using System.Collections.Generic;

namespace College.UseCases.Shared.Commands
{
    public interface ICommandResult
    {
        IDictionary<string, string> Notifications { get; set; }
    }
}
