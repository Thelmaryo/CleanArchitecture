using System;
using System.Collections.Generic;
using System.Linq;

namespace College.Entities.Shared
{
    public class RequiredGrade
    {
        private IEnumerable<Tuple<EStatusDiscipline, decimal, decimal>> _content = new List<Tuple<EStatusDiscipline, decimal, decimal>> {
            new Tuple<EStatusDiscipline, decimal, decimal>(EStatusDiscipline.Fail, 0, 40),
            new Tuple<EStatusDiscipline, decimal, decimal>(EStatusDiscipline.FinalExam, 40, 60),
            new Tuple<EStatusDiscipline, decimal, decimal>(EStatusDiscipline.Pass, 60, (decimal)100.1),
        };

        public EStatusDiscipline GetStatus(decimal grade, decimal finalExamGrade)
        {
            var status = _content.Single(x => x.Item2 <= grade && x.Item3 > grade).Item1;
            if(status == EStatusDiscipline.FinalExam)
                status = _content.Single(x => x.Item2 <= finalExamGrade && x.Item3 > grade).Item1;
            return status;
        }
        
    }
}
