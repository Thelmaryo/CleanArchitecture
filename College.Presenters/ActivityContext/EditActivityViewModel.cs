using College.Presenters.Shared;
using System;

namespace College.Presenters.ActivityContext
{
    public class EditActivityViewModel
    {
        public Guid Id { get; set; }
        public Guid DisciplineId { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string Date { get; set; }
        public SaveButton SaveButton => new SaveButton();
        public BackButton BackButton => new BackButton();
    }
}
