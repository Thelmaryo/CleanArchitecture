using System;

namespace College.Entities.EvaluationContext.ValueObjects
{
    public class Semester
    {
        public DateTime Begin { get; private set; }
        public DateTime End { get; private set; }
        public Semester()
        {
            if (DateTime.Now.Month <= 6)
            {
                Begin = new DateTime(DateTime.Now.Year, 1, 1);
                End = new DateTime(DateTime.Now.Year, 6, 30);
            }
            else
            {
                Begin = new DateTime(DateTime.Now.Year, 7, 1);
                End = new DateTime(DateTime.Now.Year, 12, 31);
            }
        }

        public Semester(DateTime begin, DateTime end)
        {
            Begin = begin;
            End = end;
        }
    }
}
