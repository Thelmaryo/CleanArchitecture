using College.Entities.EvaluationContext.Entities;
using College.UseCases.EvaluationContext.Inputs;
using College.UseCases.EvaluationContext.Repositories;
using College.UseCases.EvaluationContext.Results;
using College.UseCases.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace College.UseCases.EvaluationContext.Queries
{
    public class DisciplineQueryHandler : IQueryHandler<DisciplineInputListByEnrollment, DisciplineResultQueryList>
    {
        private readonly IDisciplineRepository _DREP;
        private readonly IActivityRepository _AREP;

        public DisciplineQueryHandler(IDisciplineRepository DREP, IActivityRepository AREP)
        {
            _DREP = DREP;
            _AREP = AREP;
        }
        public DisciplineResultQueryList Handle(DisciplineInputListByEnrollment command)
        {
            var result = new DisciplineResultQueryList();
            var disciplines = _DREP.GetByEnrollment(command.EnrollmentId);
            foreach (var discipline in disciplines)
            {
                var activities = _AREP.GetByDiscipline(command.StudentId, discipline.Id, new Shared.Semester(command.SemesterBegin, command.SemesterEnd)).ToList();
                var finalExam = new Activity();
                if (activities.Count() > 0)
                {
                    finalExam = activities.SingleOrDefault(x => x.Value == 100);
                    activities.Remove(finalExam);
                }
                discipline.AddActivities(activities, finalExam.Grade, command.SemesterBegin, command.SemesterEnd);
            }
            result.Disciplines = disciplines;
            return result;
        }
    }
}
