using College.Presenters.Shared;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

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
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        [Display(Name = "Telefone")]
        public string Phone { get; set; }
        public LinkButton EditButton => new LinkButton("Editar");
        public LinkButton DetailsButton => new LinkButton("Detalhes");
        public DeleteButton DeleteButton => new DeleteButton();
    }
}
