using College.Entities.EnrollmentContext.Entities;
using College.UseCases.Shared.Commands;

namespace College.UseCases.EnrollmentContext.Result
{
    public class EnrollmentResultQueryGet : IQueryResult
    {
        public Enrollment Enrollment { get; set; }
    }
}
