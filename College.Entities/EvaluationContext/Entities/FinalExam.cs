using College.Entities.EvaluationContext.Interfaces;
using College.Entities.Shared;

namespace College.Entities.EvaluationContext.Entities
{
    public class FinalExam : ActivityBase
    {
        public FinalExam(Student student, decimal grade)
        {
            Student = student;
            Grade = grade;
        }
        public override string Description => "Exame Final";
        protected override decimal GetValue() => 100;
    }
}
