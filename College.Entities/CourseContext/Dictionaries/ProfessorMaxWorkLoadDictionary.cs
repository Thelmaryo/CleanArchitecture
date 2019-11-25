using College.Entities.CourseContext.Enumerators;
using System.Collections.Generic;
using System.Linq;

namespace College.Entities.CourseContext.Dictionaries
{
    public class ProfessorMaxWorkLoadDictionary
    {
        private readonly Dictionary<EDegree, int> _content = new Dictionary<EDegree, int> {
            { EDegree.Bachelor, 40 },
            { EDegree.Master, 30 },
            { EDegree.Doctor, 25 }
        };

        public int Get(EDegree degree) => _content.Single(x => x.Key == degree).Value;
    }
}
