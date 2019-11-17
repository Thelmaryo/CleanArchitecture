using College.Presenters.Shared;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace College.Presenters.StudentContext
{
    public class StudentListViewModel
    {
        public LinkButton CreateButton => new LinkButton("Novo Acadêmico");
        public IEnumerable<StudentListItem> Students { get; set; }
    }

    public class StudentListItem
    {
        public string Id { get; set; }
        [Display(Name = "Nome")]
        public string FirstName { get; set; }
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        [Display(Name = "Cidade")]
        public string City { get; set; }
        [Display(Name = "Curso")]
        public string Course { get; set; }
        public LinkButton EditButton => new LinkButton("Editar");
        public LinkButton DetailsButton => new LinkButton("Detalhes");
        public DeleteButton DeleteButton => new DeleteButton();
    }
}
