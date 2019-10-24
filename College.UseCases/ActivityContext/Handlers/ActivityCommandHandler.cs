using College.UseCases.ActivityContext.Dictionaries;
using College.UseCases.ActivityContext.Inputs;
using College.UseCases.ActivityContext.Repositories;
using College.UseCases.Shared.Commands;
using College.UseCases.Shared.Result;

namespace College.UseCases.ActivityContext.Handlers
{
    public class ActivityCommandHandler : ICommandHandler<ActivityInputRegister>, ICommandHandler<ActivityInputUpdate>,
        ICommandHandler<ActivityInputDelete>
    {
        private readonly IActivityRepository _AREP;

        public ActivityCommandHandler(IActivityRepository AREP)
        {
            _AREP = AREP;
        }

        public ICommandResult Handle(ActivityInputRegister command)
        {
            var activity = ActivityTypeDictionary.GetActivity(command);
            var result = new StandardResult();
            if (activity.Notifications.Count == 0)
            {
                _AREP.Create(activity);
                result.Notifications.Add("Success", "A atividade foi criada.");
            }
            else
            {
                foreach (var notification in activity.Notifications)
                    result.Notifications.Add(notification);
            }
            return result;
        }

        public ICommandResult Handle(ActivityInputUpdate command)
        {
            var activity = ActivityTypeDictionary.GetActivity(command);
            var result = new StandardResult();
            if (activity.Notifications.Count == 0)
            {
                _AREP.Update(activity);
                result.Notifications.Add("Success", "A atividade foi atualizada.");
            }
            else
            {
                foreach (var notification in activity.Notifications)
                    result.Notifications.Add(notification);
            }
            return result;
        }

        public ICommandResult Handle(ActivityInputDelete command)
        {
            _AREP.Delete(command.Id);
            var result = new StandardResult();
            result.Notifications.Add("Success", "A atividade foi excluida.");
            return result;
        }
    }
}
