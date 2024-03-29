﻿using College.Entities.StudentContext.Entities;
using College.UseCases.Services;
using College.UseCases.Shared.Commands;
using College.UseCases.Shared.Result;
using College.UseCases.StudentContext.Inputs;
using College.UseCases.StudentContext.Repositories;

namespace College.UseCases.StudentContext.Handlers
{
    public class StudentCommandHandler : ICommandHandler<StudentInputRegister>
    {
        private readonly IStudentRepository _SREP;
        IEncryptor _encryptor;

        public StudentCommandHandler(IStudentRepository SREP, IEncryptor encryptor)
        {
            _SREP = SREP;
            _encryptor = encryptor;
        }

        public ICommandResult Handle(StudentInputRegister command)
        {
            var course = new Course(command.CourseId);
            string password = string.Empty;
            string salt = string.Empty;
            if (!string.IsNullOrEmpty(command.CPF))
                password = _encryptor.Encrypt(command.CPF.Replace("-", "").Replace(".", ""), out salt);

            var student = new Student(course, command.Birthdate, command.FirstName, command.LastName, command.CPF, command.Email, command.Phone, command.Gender, command.Country, command.City, command.Address, password, salt);

            var result = new StandardResult();
            if (_SREP.Get(student.CPF.Number) != null)
                result.Notifications.Add("CPF", "CPF já foi cadastrado!");
            result.AddRange(student.Notifications);
            if (result.Notifications.Count == 0)
            {
                _SREP.Create(student);
                result.Notifications.Add("Success", "O Acadêmico foi salvo");
            }
            return result;
        }

        public ICommandResult Handle(StudentInputUpdate command)
        {
            var course = new Course(command.CourseId);

            var student = new Student(course, command.Birthdate, command.FirstName, command.LastName, command.Email, command.Phone, command.Gender, command.Country, command.City, command.Address, command.StudentId);
            var result = new StandardResult();
            result.AddRange(student.Notifications);
            if (student.Notifications.Count == 0)
            {
                _SREP.Update(student);
                result.Notifications.Add("Success", "O Acadêmico foi Editado");
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
