using College.Entities.Shared;

namespace College.Entities.EnrollmentContext.Entities
{
    public class Discipline : Entity
    {
        public Discipline(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
