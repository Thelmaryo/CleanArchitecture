using System;
using System.Collections.Generic;

namespace College.Entities.Shared
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public IDictionary<string, string> Notifications { get; protected set; }
        public Entity()
        {
            Id = Guid.NewGuid();
            Notifications = new Dictionary<string, string>();
        }

        public bool IsValid()
        {
            bool valid = false;
            valid = (Notifications.Count == 0);
            foreach (var value in Notifications.Values)
            {
                if (value == null)
                    valid = true;
                if (value != null)
                    return false;
            }
            return valid;
        }
    }
}
