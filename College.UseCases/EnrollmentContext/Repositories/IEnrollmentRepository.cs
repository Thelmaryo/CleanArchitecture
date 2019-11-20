using College.Entities.EnrollmentContext.Entities;
using System;
using System.Collections.Generic;

namespace College.UseCases.EnrollmentContext.Repositories
{
    public interface IEnrollmentRepository
    {
        void Create(Enrollment enrollment);
        void Confirm(Guid id);
        void Cancel(Guid id);
        Enrollment Get(Guid id);
        Enrollment GetCurrent(Guid studentId);
        IEnumerable<Enrollment> GetPreEnrollments();
        IEnumerable<Enrollment> GetByStudent(Guid studentId);

    }
}
