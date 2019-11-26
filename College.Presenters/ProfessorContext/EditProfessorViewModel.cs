using College.Entities.ProfessorContext.Enumerators;
using College.Presenters.Shared;
using System.Collections.Generic;

namespace College.Presenters.ProfessorContext
{
    public class EditProfessorViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public EDegree SelectedDegree { get; set; }
        public IEnumerable<ComboboxItem> Degrees { get; set; }
        public SaveButton SaveButton => new SaveButton();
        public BackButton BackButton => new BackButton();
    }
}
