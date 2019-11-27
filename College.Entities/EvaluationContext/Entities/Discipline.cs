using College.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace College.Entities.EvaluationContext.Entities
{
    public class Discipline : Entity
    {
        public Discipline()
        {
            Activities = new List<Activity>();
        }
        public void AddActivities(IEnumerable<Activity> activities, decimal finalExamGrade, DateTime semesterBegin, DateTime semesterEnd)
        {
            FinalExamGrade = finalExamGrade;
            Activities = activities;
            if (Activities.All(x => semesterBegin <= x.Date && semesterEnd >= x.Date))
                StudentStatus = EStatusDiscipline.Enrolled;
            else
                StudentStatus = new RequiredGrade().GetStatus(Activities.Sum(x=>x.Grade), FinalExamGrade);
        }

        public string Name { get; private set; }
        public decimal FinalExamGrade { get; set; }
        public EStatusDiscipline StudentStatus { get; set; }
        public IEnumerable<Activity> Activities { get; private set; }
    }
}
