using System;
using System.ComponentModel.DataAnnotations;

namespace College.Presenters.StudentContext
{
    public class StudentViewModelDelete
    {
        public Guid Id { get; set; }
        [Display(Name = "Nome")]
        public string FirstName { get; set; }
    }
}
