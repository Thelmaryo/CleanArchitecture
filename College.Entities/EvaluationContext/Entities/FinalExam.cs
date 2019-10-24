using System;

namespace College.Entities.EvaluationContext.Entities
{
    public class FinalExam : ActivityBase
    {
        public FinalExam(Student student, decimal grade, Guid? id)
        {
            if (id != null) Id = (Guid)id;
            Student = student;
            Grade = grade;
        }
        public override string Description => "Exame Final";
        protected override decimal GetValue() => 100;
    }
}
