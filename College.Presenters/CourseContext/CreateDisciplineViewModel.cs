using College.Presenters.Shared;
using System;
using System.Collections.Generic;

namespace College.Presenters.CourseContext
{
    public class CreateDisciplineViewModel
    {
        public string Name { get; set; }
        public Guid SelectedCourse { get; set; }
        public IEnumerable<ComboboxItem> Courses { get; set; }
        public IEnumerable<ComboboxItem> Professors { get; set; }
        public Guid SelectedProfessor { get; set; }
        public int WeeklyWorkload { get; set; }
        public int Period { get; set; }
        public SaveButton SaveButton => new SaveButton();
        public BackButton BackButton => new BackButton();
    }
}
