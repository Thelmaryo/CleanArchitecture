using College.Presenters.Shared;
using System;

namespace College.Presenters.CourseContext
{
    public class DeleteDisciplineViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public string Professor { get; set; }
        public int WeeklyWorkload { get; set; }
        public int Period { get; set; }
        public DeleteButton DeleteButton => new DeleteButton();
        public BackButton BackButton => new BackButton();
    }
}
