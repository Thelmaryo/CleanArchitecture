using College.UseCases.ActivityContext.Inputs;
using College.UseCases.ActivityContext.Models;
using College.UseCases.ActivityContext.Repositories;
using College.UseCases.ActivityContext.Results;
using College.UseCases.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace College.UseCases.ActivityContext.Queries
{
    public class ActivityQueryHandler : IQueryHandler<ActivityInputGetByDiscipline, ActivityResultQueryList>, IQueryHandler<ExamInputGetByDiscipline, ActivityResultQueryList>, 
        IQueryHandler<ActivityInputGetById, ActivityResultQueryGetById>
    {
        private readonly IActivityRepository _AREP;

        public ActivityQueryHandler(IActivityRepository AREP)
        {
            _AREP = AREP;
        }

        public ActivityResultQueryList Handle(ActivityInputGetByDiscipline command)
        {
            var result = new ActivityResultQueryList();
            result.Activities = _AREP.GetByDiscipline(command.DisciplineId, command.Semester).Where(x => x.Value <= 15);
            return result;
        }

        public ActivityResultQueryGetById Handle(ActivityInputGetById command)
        {
            var result = new ActivityResultQueryGetById();
            result.Activity = _AREP.Get(command.ActivityId);
            return result;
        }

        public ActivityResultQueryList Handle(ExamInputGetByDiscipline command)
        {
            var result = new ActivityResultQueryList();
            result.Activities = _AREP.GetByDiscipline(command.DisciplineId, command.Semester).Where(x => x.Value > 15);
            return result;
        }
    }
}
