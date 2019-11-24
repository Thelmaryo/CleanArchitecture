using System;
using System.ComponentModel.DataAnnotations;
using College.Presenters.Shared;

namespace College.Presenters.StudentContext
{
    public class DeleteStudentViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Nome")]
        public string FirstName { get; set; }
       
        public BackButton BackButton => new BackButton();
        public DeleteButton DeleteButton => new DeleteButton();
    }
}
