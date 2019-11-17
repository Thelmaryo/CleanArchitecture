using College.Presenters.Shared;
using System.Collections.Generic;

namespace College.Presenters.ActivityContext
{
    public class ActivityMainMenuViewModel
    {
        public IEnumerable<DisciplineMainMenuItem> Disciplines { get; set; }
    }

    public class DisciplineMainMenuItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public LinkButton ActivityButton => new LinkButton("Atividades");
        public LinkButton ExamButton => new LinkButton("Provas");
    }
}
