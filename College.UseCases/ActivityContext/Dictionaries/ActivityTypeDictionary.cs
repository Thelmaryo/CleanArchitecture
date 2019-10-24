using College.Entities.ActivityContext.Entities;
using College.Entities.ActivityContext.Interfaces;
using College.UseCases.ActivityContext.Inputs;
using College.UseCases.Shared.Enumerators;
using System.Collections.Generic;
using System.Linq;

namespace College.UseCases.ActivityContext.Dictionaries
{
    public static class ActivityTypeDictionary
    {
        public static IActivity GetActivity(ActivityInputRegister input)
        {
            var discipline = new Discipline(input.DisciplineId, input.DisciplineName);
            IDictionary<EActivityType, IActivity> dictionary = new Dictionary<EActivityType, IActivity> {
                { EActivityType.Activity, new Activity(discipline, input.Description, input.Date, input.Value, input.DistributedPoints, null) },
                { EActivityType.Exam1, new Exam1(discipline, null) },
                { EActivityType.Exam2, new Exam2(discipline, null) },
                { EActivityType.Exam3, new Exam3(discipline, null) },
                { EActivityType.FinalExam, new FinalExam(discipline, null) }
            };
            return dictionary.SingleOrDefault(x => x.Key == input.ActivityType).Value;
        }

        public static IActivity GetActivity(ActivityInputUpdate input)
        {
            var discipline = new Discipline(input.DisciplineId, input.DisciplineName);
            IDictionary<EActivityType, IActivity> dictionary = new Dictionary<EActivityType, IActivity> {
                { EActivityType.Activity, new Activity(discipline, input.Description, input.Date, input.Value, input.DistributedPoints, input.Id) },
                { EActivityType.Exam1, new Exam1(discipline, input.Id) },
                { EActivityType.Exam2, new Exam2(discipline, input.Id) },
                { EActivityType.Exam3, new Exam3(discipline, input.Id) },
                { EActivityType.FinalExam, new FinalExam(discipline, input.Id) }
            };
            return dictionary.SingleOrDefault(x => x.Key == input.ActivityType).Value;
        }
    }
}
