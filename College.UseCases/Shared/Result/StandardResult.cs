using College.UseCases.Shared.Commands;
using System.Collections.Generic;
using System.Linq;

namespace College.UseCases.Shared.Result
{
    public class StandardResult : ICommandResult
    {
        public IDictionary<string, string> Notifications { get; private set; }
        public bool IsValid => Notifications.Keys.ToList().All(x=>x == "Success");
        public StandardResult()
        {
            Notifications = new Dictionary<string, string>();
        }
        public void AddRange(IDictionary<string, string> notifications)
        {
            foreach(var notification in notifications)
                Notifications.Add(notification);
        }
    }
}
