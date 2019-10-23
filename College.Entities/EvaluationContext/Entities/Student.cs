using College.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.Entities.EvaluationContext.Entities
{
    public class Student : Entity
    {
        public Student(string name)
        {
            Name = name;
        }
        public Student()
        {

        }

        public string Name { get; private set; }
    }
}
