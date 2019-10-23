using College.UseCases.Commands;
using System.Collections.Generic;

namespace College.UseCases.Shared.Result
{
    public class StandardResult : ICommandResult
    {
        IDictionary<string, string> Notifications { get; set; }
    }
}
