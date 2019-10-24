
using System;

namespace College.Entities.EvaluationContext.Entities
{
    public class Exam3 : ActivityBase
    {
        public Exam3(Student student, decimal grade, Guid? id)
        {
            if (id != null) Id = (Guid)id;
            Student = student;
            Grade = grade;
        }

        public override string Description => "Prova 03";
        protected override decimal GetValue() => 20;
    }
}
