using College.Entities.EnrollmentContext.Entities;
using System;
using System.Collections.Generic;

namespace College.UseCases.EnrollmentContext.Repositories
{
    public interface IDisciplineRepository
    {
        IEnumerable<Discipline> GetByCourse(Guid id);
        IEnumerable<Discipline> GetConcluded(Guid studentId);
    }
}
