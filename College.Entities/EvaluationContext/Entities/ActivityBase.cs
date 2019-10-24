using College.Entities.Shared;

namespace College.Entities.EvaluationContext.Entities
{
    public abstract class ActivityBase : Entity
    {
        public Student Student { get; protected set; }
        public virtual string Description { get; protected set; }
        public decimal Grade { get; protected set; }
        protected abstract decimal GetValue();
        public void GradeValidation()
        {
            if (Grade > GetValue())
                Notifications.Add("Grade", $"A nota deve estar entre 0 e {GetValue()} pontos.");
        }
    }
}
