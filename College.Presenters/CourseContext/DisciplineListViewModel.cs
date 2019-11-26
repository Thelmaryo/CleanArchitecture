using College.Presenters.Shared;
using System;
using System.Collections.Generic;

namespace College.Presenters.CourseContext
{
    public class DisciplineListViewModel
    {
        public LinkButton CreateButton => new LinkButton("Adicionar");
        public IEnumerable<DisciplineListItem> Disciplines { get; set; }
    }

    public class DisciplineListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public string Professor { get; set; }
        public int WeeklyWorkload { get; set; }
        public int Period { get; set; }
        public LinkButton EditButton => new LinkButton("Editar");
        public LinkButton DetailsButton => new LinkButton("Detalhes");
        public DeleteButton DeleteButton => new DeleteButton();
    }
}
