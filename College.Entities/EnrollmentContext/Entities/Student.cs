using College.Entities.Shared;
using System;

namespace College.Entities.EnrollmentContext.Entities
{
    public class Student : Entity
    {
        public Student(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public Student()
        {

        }

        public string Name { get; private set; }
    }
}
