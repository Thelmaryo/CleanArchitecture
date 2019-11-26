using College.Presenters.Shared;
using System;

namespace College.Presenters.StudentContext
{
    public class DeleteStudentViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public BackButton BackButton => new BackButton();
        public DeleteButton DeleteButton => new DeleteButton();
    }
}
