using College.Entities.ActivityContext.Entities;
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
            var activity = new Activity(new Discipline(command.Activity.DisciplineId), command.Activity.Description, command.Activity.Date, command.Activity.Value, command.DistributedPoints, null);
            var result = new StandardResult();
            result.AddRange(activity.Notifications);
            if (result.Notifications.Count == 0)
            {
                _AREP.Create(activity);
                result.Notifications.Add("Success", "A atividade foi criada.");
            }
            return result;
        }

        public ICommandResult Handle(ActivityInputUpdate command)
        {
            var activity = new Activity(new Discipline(command.Activity.DisciplineId), command.Activity.Description, command.Activity.Date, command.Activity.Value, command.DistributedPoints, command.Id);
            var result = new StandardResult();
            result.AddRange(activity.Notifications);
            if (result.Notifications.Count == 0)
            {
                _AREP.Edit(activity);
                result.Notifications.Add("Success", "A atividade foi atualizada.");
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
