using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace College.Presenters.ProfessorContext
{
    public class ProfessorViewModelCreate
    {

        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        [Display(Name = "Grau")]
        public SelectList Degree { get; set; }
    }
}
