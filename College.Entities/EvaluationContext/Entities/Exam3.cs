using College.Entities.EvaluationContext.Interfaces;
using College.Entities.Shared;

namespace College.Entities.EvaluationContext.Entities
{
    public class Exam3 : ActivityBase
    {
        public Exam3(Student student, decimal grade)
        {
            Student = student;
            Grade = grade;
        }

        public override string Description => "Prova 03";
        protected override decimal GetValue() => 20;
    }
}
