using College.Presenters.Shared;
using System;
using System.Collections.Generic;

namespace College.Presenters.ProfessorContext
{
    public class ProfessorListViewModel
    {
        public LinkButton CreateButton => new LinkButton("Novo Docente");
        public IEnumerable<ProfessorListItem> Professors { get; set; }
    }
    public class ProfessorListItem 
    { 
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public LinkButton EditButton => new LinkButton("Editar");
        public LinkButton DetailsButton => new LinkButton("Detalhes");
        public DeleteButton DeleteButton => new DeleteButton();
    }
}
