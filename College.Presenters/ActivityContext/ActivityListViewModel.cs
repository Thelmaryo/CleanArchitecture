using College.Presenters.Shared;
using System;
using System.Collections.Generic;

namespace College.Presenters.ActivityContext
{
    public class ActivityListViewModel
    {
        public Guid DisciplineId { get; set; }
        public string DisciplineName { get; set; }
        public IEnumerable<ActivityListItem> Activities { get; set; }
        public LinkButton CreateButton => new LinkButton("Nova Atividade");
    }

    public class ActivityListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Date { get; set; }
        public LinkButton GradesButton => new LinkButton("Notas");
        public LinkButton DetailsButton => new LinkButton("Detalhes");
        public LinkButton EditButton => new LinkButton("Editar");
        public DeleteButton DeleteButton => new DeleteButton();
    }
}
