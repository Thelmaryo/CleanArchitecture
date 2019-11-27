using College.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace College.Helpers
{
    public static class DictionaryStudentStatus
    {
        private static IEnumerable<Tuple<EStatusDiscipline, string, string>> _content = new List<Tuple<EStatusDiscipline, string, string>> {
            new Tuple<EStatusDiscipline, string, string>(EStatusDiscipline.Enrolled, "Matriculado", "Blue"),
            new Tuple<EStatusDiscipline, string, string>(EStatusDiscipline.Fail, "Reprovado", "Red"),
            new Tuple<EStatusDiscipline, string, string>(EStatusDiscipline.Pass, "Aprovado", "Green"),
        };

        public static string Get(EStatusDiscipline status, out string color)
        {
            var result = _content.Single(x => x.Item1 == status);
            color = result.Item3;
            return result.Item2;
        }
    }
}