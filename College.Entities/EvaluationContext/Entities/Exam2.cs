using System;

namespace College.Entities.EvaluationContext.Entities
{
    public class Exam2 : ActivityBase
    {
        public Exam2(Student student, decimal grade, Guid? id)
        {
            if (id != null) Id = (Guid)id;
            Student = student;
            Grade = grade;
        }

        public override string Description => "Prova 02";
        protected override decimal GetValue() => 20;
    }
}
