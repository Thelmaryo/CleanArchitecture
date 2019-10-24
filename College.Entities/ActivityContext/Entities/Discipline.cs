using College.Entities.Shared;
using System;

namespace College.Entities.ActivityContext.Entities
{
    public class Discipline : Entity
    {
        public Discipline(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public Discipline()
        {

        }

        public string Name { get; private set; }
    }
}
