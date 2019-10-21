using College.Entities.Shared;

namespace College.Entities.Activity.Entities
{
    public class Discipline : Entity
    {
        public Discipline(string name)
        {
            Name = name; 
        }
        public Discipline()
        {

        }

        public string Name { get; private set; }
    }
}
