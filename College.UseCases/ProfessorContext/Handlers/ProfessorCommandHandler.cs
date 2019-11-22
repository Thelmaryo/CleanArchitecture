using College.Entities.ProfessorContext.Entities;
using College.Entities.ProfessorContext.Enumerators;
using College.UseCases.ProfessorContext.Inputs;
using College.UseCases.ProfessorContext.Repositories;
using College.UseCases.Services;
using College.UseCases.Shared.Commands;
using College.UseCases.Shared.Result;

namespace College.UseCases.ProfessorContext.Handlers
{
    public class ProfessorCommandHandler : ICommandHandler<ProfessorInputRegister>
    {
        readonly IProfessorRepository _PREP;
        IEncryptor _encryptor;

        public ProfessorCommandHandler(IProfessorRepository PREP, IEncryptor encryptor)
        {
            _PREP = PREP;
            _encryptor = encryptor;
        }
        public ICommandResult Handle(ProfessorInputRegister command)
        {
            string password = string.Empty;
            string salt = string.Empty;
            if (!string.IsNullOrEmpty(command.CPF))
                password = _encryptor.Encrypt(command.CPF.Replace("-", "").Replace(".", ""), out salt);

            var professor = new Professor(command.FirstName, command.LastName, command.CPF,
                command.Email, command.Phone, command.Degree, password, salt);

            var result = new StandardResult();
            result.AddRange(professor.Notifications);
            if (result.Notifications.Count == 0)
            {
                _PREP.Create(professor);
                result.Notifications.Add("Success", "O Professor foi salvo");
            }
            return result;
        }

        public ICommandResult Handle(ProfessorInputUpdate command)
        {
            var professor = new Professor(command.ProfessorId, command.FirstName, command.LastName,
                command.Email, command.Phone, command.Degree);
            var result = new StandardResult();
            result.AddRange(professor.Notifications);
            if (result.Notifications.Count == 0)
            {
                _PREP.Update(professor);
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
