using College.UseCases.Shared.Commands;
using College.UseCases.Shared.Result;
using College.UseCases.CourseContext.Inputs;
using College.UseCases.CourseContext.Repositories;
using College.Entities.CourseContext.Entities;

namespace College.UseCases.CourseContext.Handlers
{
    public class DisciplineCommandHandler : ICommandHandler<DisciplineInputRegister>
    {
        private readonly IDisciplineRepository _DREP;
        private readonly IProfessorRepository _PREP;

        public DisciplineCommandHandler(IDisciplineRepository dREP, IProfessorRepository pREP)
        {
            _DREP = dREP;
            _PREP = pREP;
        }

        public ICommandResult Handle(DisciplineInputRegister command)
        {
            int professorWorkload = _PREP.GetWorkload(command.ProfessorId);
            var discipline = new Discipline(command.Name, new Course(command.CourseId), new Professor(command.ProfessorId), command.WeeklyWorkload, command.Period, professorWorkload);
            var result = new StandardResult();
            result.AddRange(discipline.Notifications);
            if (result.Notifications.Count == 0)
            {
                _DREP.Create(discipline);
                result.Notifications.Add("Success", "O Acadêmico foi salvo");
            }
            return result;
        }

        public ICommandResult Handle(DisciplineInputUpdate command)
        {
            int professorWorkload = _PREP.GetWorkload(command.ProfessorId);
            var discipline = new Discipline(command.Name, new Course(command.CourseId), new Professor(command.ProfessorId), command.WeeklyWorkload, command.Period, command.DisciplineId, professorWorkload);
            var result = new StandardResult();
            result.AddRange(discipline.Notifications);
            if (result.Notifications.Count == 0)
            {
                _DREP.Update(discipline);
                result.Notifications.Add("Success", "O Acadêmico foi Editado");
            }
            return result;
        }

        public ICommandResult Handle(DisciplineInputDelete command)
        {
            _DREP.Delete(command.DisciplineId);
            var result = new StandardResult();
            if (_DREP.Get(command.DisciplineId) != null)
                result.Notifications.Add("Error", "Não foi possivel deletar Discente!");
            else
                result.Notifications.Add("Success", "Discente Deletado");

            return result;
        }
    }
}
