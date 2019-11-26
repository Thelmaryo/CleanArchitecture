using College.Presenters.Shared;
using System;
using System.Collections.Generic;

namespace College.Presenters.StudentContext
{
    public class EditStudentViewModel
    {
        public Guid Id { get; set; }
        public DateTime Birthdate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public Guid SelectedCourse { get; set; }
        public IEnumerable<ComboboxItem> Courses { get; set; }
        public SaveButton SaveButton => new SaveButton();
        public BackButton BackButton => new BackButton();
    }
}
