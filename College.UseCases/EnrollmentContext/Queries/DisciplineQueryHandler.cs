using College.UseCases.EnrollmentContext.Inputs;
using College.UseCases.EnrollmentContext.Repositories;
using College.UseCases.EnrollmentContext.Result;
using College.UseCases.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace College.UseCases.EnrollmentContext.Queries
{
    public class DisciplineQueryHandler : IQueryHandler<DisciplineInputGetNotConcluded, DisciplineResultQueryList>
    {
        private readonly IDisciplineRepository _DREP;

        public DisciplineQueryHandler(IDisciplineRepository dREP)
        {
            _DREP = dREP;
        }

        public DisciplineResultQueryList Handle(DisciplineInputGetNotConcluded command)
        {
            var allDisciplines = _DREP.GetByCourse(command.CourseId);
            var concludedDisciplines = _DREP.GetConcluded(command.StudentId);
            return new DisciplineResultQueryList {
                Disciplines = allDisciplines.Where(x => !concludedDisciplines.Any(y => y.Id == x.Id))
            };
        }
    }
}
