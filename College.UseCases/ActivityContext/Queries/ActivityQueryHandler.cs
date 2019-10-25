using College.UseCases.ActivityContext.Inputs;
using College.UseCases.ActivityContext.Repositories;
using College.UseCases.ActivityContext.Results;
using College.UseCases.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace College.UseCases.ActivityContext.Queries
{
    public class ActivityQueryHandler : IQueryHandler<ActivityInputGetByDiscipline, ActivityResultQueryList>
    {
        private readonly IActivityRepository _AREP;

        public ActivityQueryHandler(IActivityRepository AREP)
        {
            _AREP = AREP;
        }

        public ActivityResultQueryList Handle(ActivityInputGetByDiscipline command)
        {
            var result = new ActivityResultQueryList();
            result.Activities = _AREP.GetByDiscipline(command.DisciplineId, command.Semester);
            return result;
        }
    }
}
