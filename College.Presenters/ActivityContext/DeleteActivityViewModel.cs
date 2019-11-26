using College.Presenters.Shared;
using System;

namespace College.Presenters.ActivityContext
{
    public class DeleteActivityViewModel
    {
        public Guid Id { get; set; }
        public string Discipline { get; set; }
        public Guid DisciplineId { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string Date { get; set; }
        public BackButton BackButton => new BackButton();
        public DeleteButton DeleteButton => new DeleteButton();
    }
}
