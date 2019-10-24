using College.UseCases.Shared.Commands;
using System.Collections.Generic;

namespace College.UseCases.Shared.Result
{
    public class StandardResult : ICommandResult
    {
        public IDictionary<string, string> Notifications { get; set; }
        public StandardResult()
        {
            Notifications = new Dictionary<string, string>();
        }
    }
}
