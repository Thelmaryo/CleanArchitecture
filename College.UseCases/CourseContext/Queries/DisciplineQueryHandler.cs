using College.UseCases.Shared.Commands;
using College.UseCases.CourseContext.Repositories;
using College.UseCases.CourseContext.Result;
using College.UseCases.CourseContext.Inputs;

namespace College.UseCases.CourseContext.Queries
{
    public class DisciplineQueryHandler : IQueryHandler<DisciplineInputGet, DisciplineResultQueryGet>
    {
        private readonly IDisciplineRepository _DREP;

        public DisciplineQueryHandler(IDisciplineRepository DREP)
        {
            _DREP = DREP;
        }
        public DisciplineResultQueryGet Handle(DisciplineInputGet command)
        {
            var result = new DisciplineResultQueryGet();
            var discipline = _DREP.Get(command.DisciplineId);
            if (discipline != null)
            {
                result.Discipline = discipline;
                result.Notifications.Add("Error", "Não foi possivel deletar Discente!");
            }
            else
                result.Notifications.Add("Success", "Discente Deletado");

            return result;
        }

        public DisciplineResultQueryList Handle(DisciplineInputListById command)
        {
            var result = new DisciplineResultQueryList();
            result.Discipline = _DREP.List();
            if (result.Discipline != null)
            {
                result.Notifications.Add("Success", "Lista Criada com sucesso");
            }
            else
                result.Notifications.Add("Error", "Erro na criação da Lista");

            return result;
        }
        public DisciplineResultQueryList Handle(DisciplineInputListByStudent command)
        {
            var result = new DisciplineResultQueryList();
            result.Discipline = _DREP.GetConcluded(command.StudentId);
            if (result.Discipline != null)
            {
                result.Notifications.Add("Success", "Lista Criada com sucesso");
            }
            else
                result.Notifications.Add("Error", "Erro na criação da Lista");

            return result;
        }
        public DisciplineResultQueryList Handle(DisciplineInputListByCourse command)
        {
            var result = new DisciplineResultQueryList();
            result.Discipline = _DREP.GetByCourse(command.CourseId);
            if (result.Discipline != null)
            {
                result.Notifications.Add("Success", "Lista Criada com sucesso");
            }
            else
                result.Notifications.Add("Error", "Erro na criação da Lista");

            return result;
        }
        public DisciplineResultQueryList Handle(DisciplineInputListByEnrollment command)
        {
            var result = new DisciplineResultQueryList();
            result.Discipline = _DREP.GetByEnrollment(command.EnrollmentId);
            if (result.Discipline != null)
            {
                result.Notifications.Add("Success", "Lista Criada com sucesso");
            }
            else
                result.Notifications.Add("Error", "Erro na criação da Lista");

            return result;
        }
    }
}
