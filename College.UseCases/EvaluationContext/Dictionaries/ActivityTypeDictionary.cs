using College.Entities.EvaluationContext.Entities;
using College.UseCases.EvaluationContext.Inputs;
using College.UseCases.Shared.Enumerators;
using System.Collections.Generic;
using System.Linq;

namespace College.UseCases.EvaluationContext.Dictionaries
{
    public static class ActivityTypeDictionary
    {
        public static ActivityBase GetActivity(ActivityInputGiveGrade input)
        {
            var student = new Student(input.StudentId, input.StudentName);
            IDictionary<EActivityType, ActivityBase> dictionary = new Dictionary<EActivityType, ActivityBase> {
                { EActivityType.Activity, new Activity(student, input.ActivityName, input.Value, input.Grade, null) },
                { EActivityType.Exam1, new Exam1(student, input.Grade, null) },
                { EActivityType.Exam2, new Exam2(student, input.Grade, null) },
                { EActivityType.Exam3, new Exam3(student, input.Grade, null) },
                { EActivityType.FinalExam, new FinalExam(student, input.Grade, null) }
            };
            return dictionary.SingleOrDefault(x => x.Key == input.ActivityType).Value;
        }

        public static ActivityBase GetActivity(ActivityInputUpdateGrade input)
        {
            var student = new Student(input.StudentId, input.StudentName);
            IDictionary<EActivityType, ActivityBase> dictionary = new Dictionary<EActivityType, ActivityBase> {
                { EActivityType.Activity, new Activity(student, input.ActivityName, input.Value, input.Grade, input.Id) },
                { EActivityType.Exam1, new Exam1(student, input.Grade, input.Id) },
                { EActivityType.Exam2, new Exam2(student, input.Grade, input.Id) },
                { EActivityType.Exam3, new Exam3(student, input.Grade, input.Id) },
                { EActivityType.FinalExam, new FinalExam(student, input.Grade, input.Id) }
            };
            return dictionary.SingleOrDefault(x => x.Key == input.ActivityType).Value;
        }

    }
}
