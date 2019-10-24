using System;

namespace College.Entities.EvaluationContext.Entities
{
    public class Exam1 : ActivityBase
    {
        public Exam1(Student student, decimal grade, Guid? id)
        {
            if (id != null) Id = (Guid)id;
            Student = student;
            Grade = grade;
        }

        public override string Description => "Prova 1";
        protected override decimal GetValue() => 20;

    }
}
