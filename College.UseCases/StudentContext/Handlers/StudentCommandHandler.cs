using College.Entities.StudentContext.Entities;
using College.UseCases.Shared.Commands;
using College.UseCases.Shared.Result;
using College.UseCases.StudentContext.Inputs;
using College.UseCases.StudentContext.Repositories;

namespace College.UseCases.StudentContext.Handlers
{
    public class StudentCommandHandler : ICommandHandler<StudentInputRegister>
    {
        private readonly IStudentRepository _SREP;

        public StudentCommandHandler(IStudentRepository sREP)
        {
            _SREP = sREP;
        }

        public ICommandResult Handle(StudentInputRegister command)
        {
            var course = new Course(command.CourseId);
            string password = command.CPF.Replace("-", "").Replace(".", "");
            // TO DO: Cryptography
            var student = new Student(course, command.Birthdate, command.FirstName, command.LastName, command.CPF,
                command.Email, command.Phone, command.Gender, command.Country, command.City, command.Address, password);
            var result = new StandardResult();
            if (_SREP.Get(student.CPF.Number) != null)
                result.Notifications.Add("CPF", "CPF já foi cadastrado!");
            if (student.Notifications.Count == 0)
            {
                _SREP.Create(student);
                result.Notifications.Add("Success", "O Acadêmico foi salvo");
            }
            else
            {
                foreach (var notification in student.Notifications)
                    result.Notifications.Add(notification);
            }
            return result;
        }

        public ICommandResult Handle(StudentInputEdit command)
        {
            var course = new Course(command.CourseId);
            // TO DO: Cryptography
            var student = new Student(course, command.Birthdate, command.FirstName, command.LastName,
                command.Email, command.Phone, command.Gender, command.Country, command.City, command.Address);
            var result = new StandardResult();
            if (_SREP.Get(student.CPF.Number) != null)
                result.Notifications.Add("CPF", "CPF já foi cadastrado!");
            if (student.Notifications.Count == 0)
            {
                _SREP.Create(student);
                result.Notifications.Add("Success", "O Acadêmico foi salvo");
            }
            else
            {
                foreach (var notification in student.Notifications)
                    result.Notifications.Add(notification);
            }
            return result;
        }

        public ICommandResult Handle(StudentInputDelete command)
        {
            _SREP.Delete(command.StudentId);
            var result = new StandardResult();
            if (_SREP.Get(command.StudentId) != null)
                result.Notifications.Add("Error", "Não foi possivel deletar Discente!");
            else
                result.Notifications.Add("Success", "Discente Deletado");

            return result;
        }
    }
}
