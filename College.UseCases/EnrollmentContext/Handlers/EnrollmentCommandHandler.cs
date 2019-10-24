using College.Entities.EnrollmentContext.Entities;
using College.Entities.EnrollmentContext.Enumerators;
using College.UseCases.EnrollmentContext.Inputs;
using College.UseCases.EnrollmentContext.Repositories;
using College.UseCases.Shared.Commands;
using College.UseCases.Shared.Result;
using System;

namespace College.UseCases.EnrollmentContext.Handlers
{
    public class EnrollmentCommandHandler : ICommandHandler<EnrollmentInputRegister>
    {
        private readonly IEnrollmentRepository _EREP;

        public EnrollmentCommandHandler(IEnrollmentRepository EREP)
        {
            _EREP = EREP;
        }

        public ICommandResult Handle(EnrollmentInputRegister command)
        {
            DateTime begin;
            DateTime end;
            if (DateTime.Now.Month <= 6)
            {
                begin = new DateTime(DateTime.Now.Year, 1, 1);
                end = new DateTime(DateTime.Now.Year, 6, 30);
            }
            else
            {
                begin = new DateTime(DateTime.Now.Year, 7, 1);
                end = new DateTime(DateTime.Now.Year, 12, 30);
            }
            var student = new Student(command.StudentId, command.StudentName);
            var enrollment = new Enrollment(student, begin, end, EStatusEnrollment.PreEnrollment);
            foreach (var discipline in command.Disciplines)
                enrollment.AddDiscipline(new Discipline(discipline));
            var result = new StandardResult();
            if (enrollment.Notifications.Count == 0)
            {
                _EREP.Create(enrollment);
                result.Notifications.Add("Success", "A Matrícula foi efetuada e está aguardando confirmação.");
            }
            else
            {
                foreach (var notification in enrollment.Notifications)
                    result.Notifications.Add(notification);
            }
            return result;
        }
    }
}
