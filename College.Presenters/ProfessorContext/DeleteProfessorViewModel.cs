using College.Presenters.Shared;

namespace College.Presenters.ProfessorContext
{
    public class DeleteProfessorViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public BackButton BackButton => new BackButton();
        public DeleteButton DeleteButton => new DeleteButton();
    }
}
