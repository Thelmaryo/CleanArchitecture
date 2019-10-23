using System;

namespace College.Entities.StudentContext.Entities
{
    public class Course
    {
        public Course(Guid courseId)
        {
            CourseId = courseId;
        }

        public Course(Guid courseId, string name)
        {
            CourseId = courseId;
            Name = name;
        }

        public Guid CourseId { get; private set; }
        public string Name { get; private set; }
    }
}
