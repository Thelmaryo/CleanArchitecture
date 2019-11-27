using College.Entities.EvaluationContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace College.UseCases.EvaluationContext.Repositories
{
    public interface IDisciplineRepository
    {
        IEnumerable<Discipline> GetByEnrollment(Guid enrollmentId);
    }
}
