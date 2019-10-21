using College.Entities.Enrollment.Enumerators;
using College.Entities.Shared;
using System;

namespace College.Entities.Enrollment.Entities
{
    public class Enrollment : Entity
    {
        public Enrollment(Student student, DateTime begin, DateTime end, EStatusEnrollment status)
        {
            Student = student;
            Begin = begin;
            End = end;
            Status = status;
        }
        public Enrollment()
        {

        }

        public Student Student { get; private set; }
        public DateTime Begin { get; private set; }
        public DateTime End { get; private set; }
        public EStatusEnrollment Status { get; private set; }
    }
}
