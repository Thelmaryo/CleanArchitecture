using College.Entities.ActivityContext.Entities;
using College.UseCases.ActivityContext.Inputs;
using College.UseCases.ActivityContext.Repositories;
using College.UseCases.Shared;
using College.UseCases.Shared.Commands;
using College.UseCases.Shared.Result;
using System.Linq;

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
            decimal points = 0;
            if (command.ValidateTotalGrade)
                points = _AREP.GetByDiscipline(command.Activity.DisciplineId, new Semester()).Sum(x => x.Value);
            var activity = new Activity(new Discipline(command.Activity.DisciplineId), command.Activity.Description, command.Activity.Date, command.Activity.Value, points, command.Activity.MaxValue, null);
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
            decimal points = _AREP.GetByDiscipline(command.Activity.DisciplineId, new Semester()).Sum(x => x.Value);
            var activity = new Activity(new Discipline(command.Activity.DisciplineId), command.Activity.Description, command.Activity.Date, command.Activity.Value, points, command.Activity.MaxValue, command.Id);
            var result = new StandardResult();
            result.AddRange(activity.Notifications);
            if (result.Notifications.Count == 0)
            {
                _AREP.Update(activity);
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
