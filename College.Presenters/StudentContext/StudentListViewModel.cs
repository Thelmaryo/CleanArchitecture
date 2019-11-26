using College.Presenters.Shared;
using System.Collections.Generic;

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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Course { get; set; }
        public LinkButton EditButton => new LinkButton("Editar");
        public LinkButton DetailsButton => new LinkButton("Detalhes");
        public DeleteButton DeleteButton => new DeleteButton();
    }
}
