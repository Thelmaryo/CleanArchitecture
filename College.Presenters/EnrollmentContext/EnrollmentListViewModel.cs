using College.Presenters.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace College.Presenters.EnrollmentContext
{
    public class EnrollmentListViewModel
    {
        public LinkButton CreateButton => new LinkButton("Nova Matrícula");
        public IEnumerable<EnrollmentListItem> Students { get; set; }
    }

    public class EnrollmentListItem {
        public string Id { get; set; }
        [Display(Name = "Acadêmico")]
        public string Student { get; set; }
        public LinkButton ConfirmButton => new LinkButton("Confirmar");
        public LinkButton DenyButton => new LinkButton("Negar");
    }
}
