using College.Entities.CourseContext.Enumerators;
using College.Entities.Shared;
using System;

namespace College.Entities.CourseContext.Entities
{
    public class Professor : Entity
    {
        public Professor()
        {

        }
        public Professor(Guid id)
        {
            Id = id;
        }

        public Professor(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; private set; }
        public EDegree Degree { get; private set; }
    }
}
