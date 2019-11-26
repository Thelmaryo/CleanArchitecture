using College.Presenters.Shared;
using System;
using System.Collections.Generic;

namespace College.Presenters.EnrollmentContext
{
    public class ChooseStudentViewModel
    {
        public Guid SelectedCourse { get; set; }
        public IEnumerable<ComboboxItem> Courses { get; set; }
        public string StudentCPF { get; set; }
        public SaveButton SaveButton => new SaveButton();
        public BackButton BackButton => new BackButton();
        public string Feedback { get; set; }
    }
}
