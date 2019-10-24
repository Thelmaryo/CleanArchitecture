using College.Entities.EnrollmentContext.Entities;
using System;
using System.Collections.Generic;

namespace College.UseCases.EnrollmentContext.Repositories
{
    public interface IEnrollmentRepository
    {
        public void Create(Enrollment enrollment); 
        public void Confirm(Guid id);
        public void Cancel(Guid id);
        public Enrollment Get(Guid id);
        public Enrollment GetCurrent(Guid studentId);
        public IEnumerable<Enrollment> GetPreEnrollments();
        public IEnumerable<Enrollment> GetByStudent(Guid studentId);
        
    }
}
