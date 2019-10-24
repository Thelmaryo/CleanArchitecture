﻿using College.Entities.StudentContext.Entities;
using College.UseCases.Commands;
using College.UseCases.Shared.Result;
using College.UseCases.StudentContext.Inputs;
using College.UseCases.StudentContext.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace College.UseCases.StudentContext.Handlers
{
    public class StudentCommandHandler : ICommandHandler<StudentInputRegister>
    {
        private readonly IStudentRepository _SREP;

        public StudentCommandHandler(IStudentRepository SREP)
        {
            _SREP = SREP;
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
    }
}
