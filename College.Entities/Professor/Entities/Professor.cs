using College.Entities.Professor.Enumerators;
using College.Entities.Shared;
using System.ComponentModel.DataAnnotations;

namespace College.Entities.Professor.Entities
{
    public class Professor
    {
        public Professor(string firstName, string lastName, string cpf, string email, string phone, EDegree degree)
        {
            FirstName = firstName;
            LastName = lastName;
            CPF = cpf;
            Email = email;
            Phone = phone;
            Degree = degree;
        }

        public string Name { get => $"{FirstName} {LastName}"; }


        [Required(ErrorMessage = "O {0} é  um campo Obrigatorio")]
        [MinLength(3, ErrorMessage = "O {0} deve ter no minimo 3 caracteres")]
        [Display(Name = "Nome")]
        public string FirstName { get; private set; }

        [Required(ErrorMessage = "O {0} é  um campo Obrigatorio")]
        [MinLength(3, ErrorMessage = "O {0} deve ter no minimo 3 caracteres")]
        [Display(Name = "Sobrenome")]
        public string LastName { get; private set; }

        public CPF CPF { get; private set; }

        public Email Email { get; private set; }

        [Required(ErrorMessage = "O {0} é  um campo Obrigatorio")]
        [MinLength(3, ErrorMessage = "O {0} deve ter no minimo 8 caracteres")]
        [Display(Name = "Telefone")]
        public string Phone { get; private set; }

        [Required]
        [Display(Name = "Titulação")]
        public EDegree Degree { get; private set; }

        // Editar apenas EDegree da Professor
        public void UpdateEntity(EDegree degree)
        {
            Degree = degree;
        }
        // Editar Professor
        public void UpdateEntity(string firstName, string lastName, string cpf, string email, string phone, EDegree degree)
        {
            FirstName = firstName;
            LastName = lastName;
            CPF = cpf;
            Email = email;
            Phone = phone;
            Degree = degree;
        }
    }
}
