using College.Entities.EvaluationContext.Entities;
using System;

namespace College.UseCases.EvaluationContext.Repositories
{
    public interface IActivityRepository
    {
        public void Create(ActivityBase activity);
        public void Update(ActivityBase activity);
        public ActivityBase GetByStudent(Guid studentId, Guid activityId);
    }
}
