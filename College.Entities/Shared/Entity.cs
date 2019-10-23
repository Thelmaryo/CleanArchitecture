using System;
using System.Collections.Generic;

namespace College.Entities.Shared
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public Dictionary<string, string> Notifications { get; protected set; }
        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
