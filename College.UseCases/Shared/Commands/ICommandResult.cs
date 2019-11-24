using System.Collections.Generic;

namespace College.UseCases.Shared.Commands
{
    public interface ICommandResult
    {
        IDictionary<string, string> Notifications { get; }
        bool IsValid { get; }
        void AddRange(IDictionary<string, string> notifications);
    }
}
