using College.Entities.EnrollmentContext.Entities;
using College.UseCases.Shared.Commands;
using System.Collections.Generic;

namespace College.UseCases.EnrollmentContext.Result
{
    public class EnrollmentResultQueryList : IQueryResult
    {
        public IEnumerable<Enrollment> Enrollment { get; set; }
        public IEnumerable<Student> Student { get; set; }
    }
}
