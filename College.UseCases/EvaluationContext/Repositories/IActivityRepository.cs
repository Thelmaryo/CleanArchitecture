using College.Entities.EvaluationContext.Entities;
using System;

namespace College.UseCases.EvaluationContext.Repositories
{
    public interface IActivityRepository
    {
        public void Create(Activity activity);
        public void Edit(Activity activity);
        public Activity GetByStudent(Guid studentId, Guid activityId);
    }
}
