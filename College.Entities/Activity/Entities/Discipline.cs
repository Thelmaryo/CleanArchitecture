using College.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace College.Entities.Activity.Entities
{
    public class Discipline : Entity
    {
        public Discipline(string name)
        {
            Name = name;
        }
        public Discipline()
        {

        }

        public string Name { get; private set; }
    }
}
