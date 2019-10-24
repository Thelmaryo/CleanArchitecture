using College.UseCases.EvaluationContext.Dictionaries;
using College.UseCases.EvaluationContext.Inputs;
using College.UseCases.EvaluationContext.Repositories;
using College.UseCases.Shared.Commands;
using College.UseCases.Shared.Result;

namespace College.UseCases.EvaluationContext.Handlers
{
    public class ActivityCommandHandler : ICommandHandler<ActivityInputGiveGrade>, ICommandHandler<ActivityInputUpdateGrade>
    {
        private readonly IActivityRepository _AREP;

        public ActivityCommandHandler(IActivityRepository AREP)
        {
            _AREP = AREP;
        }

        public ICommandResult Handle(ActivityInputGiveGrade command)
        {
            var activity = ActivityTypeDictionary.GetActivity(command);
            var result = new StandardResult();
            if (activity.Notifications.Count == 0)
            {
                _AREP.Create(activity);
                result.Notifications.Add("Success", "A nota foi lançada.");
            }
            else
            {
                foreach (var notification in activity.Notifications)
                    result.Notifications.Add(notification);
            }
            return result;
        }

        public ICommandResult Handle(ActivityInputUpdateGrade command)
        {
            var activity = ActivityTypeDictionary.GetActivity(command);
            var result = new StandardResult();
            if (activity.Notifications.Count == 0)
            {
                _AREP.Edit(activity);
                result.Notifications.Add("Success", "A nota foi atualizada.");
            }
            else
            {
                foreach (var notification in activity.Notifications)
                    result.Notifications.Add(notification);
            }
            return result;
        }
    }
}
