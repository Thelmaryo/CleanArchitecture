using College.Entities.EvaluationContext.Interfaces;
using College.Entities.Shared;

namespace College.Entities.EvaluationContext.Entities
{
    public class Exam1 : ActivityBase
    {
        public Exam1(Student student, decimal grade)
        {
            Student = student;
            Grade = grade;
        }

        public override string Description => "Prova 1";
        protected override decimal GetValue() => 20;
        
    }
}
