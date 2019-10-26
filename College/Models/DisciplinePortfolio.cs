using College.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace College.Models
{
    public class DisciplinePortfolio
    {
        public DisciplinePortfolio()
        {

        }
        public DisciplinePortfolio(IEnumerable<Discipline> disciplines)
        {
            Options = new List<Checkbox>();
            foreach (var discipline in disciplines)
            {
                Options.Add(new Checkbox
                {
                    Checked = false,
                    Text = discipline.Name,
                    Value = discipline.Id.ToString()
                });
            }
        }
        [Display(Name = "Opções de Curso")]
        public List<Checkbox> Options { get; set; }
    }
}