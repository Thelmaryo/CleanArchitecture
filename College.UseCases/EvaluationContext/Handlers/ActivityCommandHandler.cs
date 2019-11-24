using College.Entities.EvaluationContext.Entities;
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
            var activity = new Activity(command.ActivityId, new Student(command.StudentId), command.Grade, command.Value);
            var result = new StandardResult();
            result.AddRange(activity.Notifications);
            if (result.Notifications.Count == 0)
            {
                _AREP.Create(activity);
                result.Notifications.Add("Success", "A nota foi lançada.");
            }
            return result;
        }

        public ICommandResult Handle(ActivityInputUpdateGrade command)
        {
            var activity = new Activity(command.ActivityId, new Student(command.StudentId), command.Grade, command.Value);
            var result = new StandardResult();
            result.AddRange(activity.Notifications);
            if (result.Notifications.Count == 0)
            {
                _AREP.Update(activity);
                result.Notifications.Add("Success", "A nota foi atualizada.");
            }
            return result;
        }
    }
}
