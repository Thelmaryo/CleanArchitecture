using College.UseCases.Shared.Commands;
using College.UseCases.EnrollmentContext.Inputs;
using College.UseCases.EnrollmentContext.Repositories;
using College.UseCases.EnrollmentContext.Result;

namespace College.UseCases.EnrollmentContext.Queries
{
    public class EnrollmentQueryHandler : IQueryHandler<EnrollmentInputGet, EnrollmentResultQueryGet>, IQueryHandler<EnrollmentInputGetByStudent, EnrollmentResultQueryGet>,
        IQueryHandler<EnrollmentInputListByStudent, EnrollmentResultQueryList>, IQueryHandler<EnrollmentInputGetPreEnrollments, EnrollmentResultQueryList>
    {
        private readonly IEnrollmentRepository _EREP;

        public EnrollmentQueryHandler(IEnrollmentRepository EREP)
        {
            _EREP = EREP;
        }
        public EnrollmentResultQueryGet Handle(EnrollmentInputGet command)
        {
            var result = new EnrollmentResultQueryGet();
            result.Enrollment = _EREP.Get(command.EnrollmentId);

            return result;
        }

        public EnrollmentResultQueryList Handle(EnrollmentInputGetPreEnrollments command)
        {
            var result = new EnrollmentResultQueryList();
            result.Enrollment = _EREP.GetPreEnrollments();
            return result;
        }

        public EnrollmentResultQueryGet Handle(EnrollmentInputGetByStudent command)
        {
            var result = new EnrollmentResultQueryGet();
            result.Enrollment = _EREP.GetCurrent(command.StudentId);

            return result;
        }

        public EnrollmentResultQueryList Handle(EnrollmentInputListByStudent command)
        {
            var result = new EnrollmentResultQueryList();
            result.Enrollment = _EREP.GetByStudent(command.StudentId);

            return result;
        }
    }
}
