using College.UseCases.Shared.Commands;
using College.UseCases.CourseContext.Repositories;
using College.UseCases.CourseContext.Result;
using College.UseCases.CourseContext.Inputs;

namespace College.UseCases.CourseContext.Queries
{
    public class DisciplineQueryHandler : IQueryHandler<DisciplineInputGet, DisciplineResultQueryGet>, IQueryHandler<DisciplineInputGetByProfessor, DisciplineResultQueryList>
    {
        private readonly IDisciplineRepository _DREP;

        public DisciplineQueryHandler(IDisciplineRepository DREP)
        {
            _DREP = DREP;
        }
        public DisciplineResultQueryGet Handle(DisciplineInputGet command)
        {
            var result = new DisciplineResultQueryGet();
            result.Discipline = _DREP.Get(command.DisciplineId);

            return result;
        }

        public DisciplineResultQueryList Handle(DisciplineInputList command)
        {
            var result = new DisciplineResultQueryList();
            result.Disciplines = _DREP.List();

            return result;
        }

        public DisciplineResultQueryList Handle(DisciplineInputGetByProfessor command)
        {
            var result = new DisciplineResultQueryList();
            result.Disciplines = _DREP.GetByProfessor(command.ProfessorId);
            return result;
        }
    }
}
