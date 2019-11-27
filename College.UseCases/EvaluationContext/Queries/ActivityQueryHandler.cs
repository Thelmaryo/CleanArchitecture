using College.UseCases.EvaluationContext.Inputs;
using College.UseCases.EvaluationContext.Repositories;
using College.UseCases.EvaluationContext.Results;
using College.UseCases.Shared.Commands;

namespace College.UseCases.EvaluationContext.Queries
{
    public class ActivityQueryHandler : IQueryHandler<ActivityInputGetByStudent, ActivityResultQueryGet>, IQueryHandler<ActivityInputGetByDiscipline, ActivityResultQueryList>
    {
        private readonly IActivityRepository _AREP;

        public ActivityQueryHandler(IActivityRepository AREP)
        {
            _AREP = AREP;
        }

        public ActivityResultQueryGet Handle(ActivityInputGetByStudent command)
        {
            var result = new ActivityResultQueryGet();
            result.Activity = _AREP.GetByStudent(command.StudentId, command.ActivityId);
            return result;
        }

        public ActivityResultQueryList Handle(ActivityInputGetByDiscipline command)
        {
            var result = new ActivityResultQueryList();
            result.Activities = _AREP.GetByDiscipline(command.StudentId, command.DisciplineId, new Shared.Semester(command.SemesterBegin, command.SemesterEnd));
            return result;
        }
    }
}
