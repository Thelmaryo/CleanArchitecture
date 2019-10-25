using College.Entities.ProfessorContext.Entities;
using College.Entities.ProfessorContext.Enumerators;
using College.UseCases.ProfessorContext.Inputs;
using College.UseCases.ProfessorContext.Repositories;
using College.UseCases.Shared.Commands;
using College.UseCases.Shared.Result;

namespace College.UseCases.ProfessorContext.Handlers
{
    public class ProfessorCommandHandler : ICommandHandler<ProfessorInputRegister>
    {
        private readonly IProfessorRepository _PREP;

        public ProfessorCommandHandler(IProfessorRepository PREP)
        {
            _PREP = PREP;
        }
        public ICommandResult Handle(ProfessorInputRegister command)
        {
            string password = command.CPF.Replace("-", "").Replace(".", "");
            EDegree degree = (EDegree)command.Degree;
            // TO DO: Cryptography
            var professor = new Professor(command.FirstName, command.LastName, command.CPF,
                command.Email, command.Phone, degree, password);
            var result = new StandardResult();
            result.AddRange(professor.Notifications);
            if (result.Notifications.Count == 0)
            {
                _PREP.Create(professor);
                result.Notifications.Add("Success", "O Professor foi salvo");
            }
            return result;
        }

        public ICommandResult Handle(ProfessorInputEdit command)
        {
            EDegree degree = (EDegree)command.Degree;

            var professor = new Professor(command.ProfessorId, command.FirstName, command.LastName,
                command.Email, command.Phone, degree);
            var result = new StandardResult();
            result.AddRange(professor.Notifications);
            if (result.Notifications.Count == 0)
            {
                _PREP.Edit(professor);
                result.Notifications.Add("Success", "O Professor foi Editado");
            }
            return result;
        }

        public ICommandResult Handle(ProfessorInputDelete command)
        {
            _PREP.Disable(command.ProfessorId);
            var result = new StandardResult();
            if (_PREP.Get(command.ProfessorId).Active != false)
                result.Notifications.Add("Error", "Não foi possivel deletar Docente!");
            else
                result.Notifications.Add("Success", "Professor Deletado");

            return result;
        }
    }
}
