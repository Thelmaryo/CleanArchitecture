using College.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                Options.Add(new Checkbox {
                    Checked = false,
                    Text = discipline.Name,
                    Value = discipline.Id.ToString()
                });
            }
        }
        public List<Checkbox> Options { get; set; }
    }
}