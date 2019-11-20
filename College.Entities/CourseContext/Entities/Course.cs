using College.Entities.Shared;

namespace College.Entities.CourseContext.Entities
{
    public class Course : Entity
    {
        public Course(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        // Editar Course
        public void UpdateEntity(string name)
        {
            Name = name;
        }
    }
}
