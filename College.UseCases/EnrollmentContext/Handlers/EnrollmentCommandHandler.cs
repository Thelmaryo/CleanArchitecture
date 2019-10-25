using College.Entities.EnrollmentContext.Entities;
using College.Entities.EnrollmentContext.Enumerators;
using College.UseCases.EnrollmentContext.Inputs;
using College.UseCases.EnrollmentContext.Repositories;
using College.UseCases.Shared;
using College.UseCases.Shared.Commands;
using College.UseCases.Shared.Result;

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
            var semester = new Semester();
            var enrollment = new Enrollment(new Student(command.StudentId), semester.Begin, semester.End, EStatusEnrollment.PreEnrollment);
            foreach (var discipline in command.Disciplines)
                enrollment.AddDiscipline(new Discipline(discipline));
            var result = new StandardResult();
            result.AddRange(enrollment.Notifications);
            if (result.Notifications.Count == 0)
            {
                _EREP.Create(enrollment);
                result.Notifications.Add("Success", "A Matrícula foi efetuada e está aguardando confirmação.");
            }
            return result;
        }
    }
}
