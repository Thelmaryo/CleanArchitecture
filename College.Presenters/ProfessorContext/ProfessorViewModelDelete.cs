using System;
using System.ComponentModel.DataAnnotations;

namespace College.Presenters.ProfessorContext
{
    public class ProfessorViewModelDelete
    {
        public Guid Id { get; set; }
        [Display(Name = "Nome")]
        public string FirstName { get; set; }
    }
}
