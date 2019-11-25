using College.Entities.Shared;
using System;

namespace College.Entities.CourseContext.Entities
{
    public class Course : Entity
    {
        public Course()
        {

        }
        public Course(string name)
        {
            Name = name;
        }
        public Course(Guid id)
        {
            Id = id;
        }

        public string Name { get; private set; }
    }
}
