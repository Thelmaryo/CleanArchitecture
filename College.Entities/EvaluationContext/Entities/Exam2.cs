using College.Entities.EvaluationContext.Interfaces;
using College.Entities.Shared;

namespace College.Entities.EvaluationContext.Entities
{
    public class Exam2 : ActivityBase
    {
        public Exam2(Student student, decimal grade)
        {
            Student = student;
            Grade = grade;
        }

        public override string Description => "Prova 02";
        protected override decimal GetValue() => 20;
    }
}
