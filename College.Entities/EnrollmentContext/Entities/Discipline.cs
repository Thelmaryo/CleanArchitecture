using College.Entities.Shared;
using System;

namespace College.Entities.EnrollmentContext.Entities
{
    public class Discipline : Entity
    {
        public Discipline()
        {
                
        }
        public Discipline(string name)
        {
            Name = name;
        }

        public Discipline(Guid id)
        {
            Id = id;
        }

        public string Name { get; private set; }
    }
}
