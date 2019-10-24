using College.Entities.Shared;
using System;

namespace College.Entities.CourseContext.Entities
{
    public class Discipline : Entity
    {
        public Discipline() { }
        // Create
        public Discipline(string name, Guid courseId, Guid professorId, int weeklyWorkload, int period)
        {
            Name = name;
            CourseId = courseId;
            ProfessorId = professorId;
            WeeklyWorkload = weeklyWorkload;
            Period = period;
        }

        public string Name { get; private set; }
        public Guid CourseId { get; private set; }
        public Guid ProfessorId { get; private set; }
        public int WeeklyWorkload { get; private set; }
        public int Period { get; private set; }

        // Edit
        public Discipline(string name, Guid courseId, Guid professorId, int weeklyWorkload, int period, Guid? id)
        {
            if (id != null)
                Id = (Guid)id;
            Name = name;
            CourseId = courseId;
            ProfessorId = professorId;
            WeeklyWorkload = weeklyWorkload;
            Period = period;
        }
    }
}
