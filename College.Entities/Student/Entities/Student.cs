using College.Entities.Account.Entities;
using College.Entities.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace College.Entities.Student.Entities
{
    public class Student : User
    {
        public Student(Course course, DateTime birthdate, string firstName, string lastName, CPF cpf, Email email, string phone, string gender, string country, string city, string address)
        {
            Course = course;
            Birthdate = birthdate;
            FirstName = firstName;
            LastName = lastName;
            CPF = cpf;
            Email = email;
            Phone = phone;
            Gender = gender;
            Country = country;
            City = city;
            Address = address;
        }

        public Course Course { get; private set; }

        public DateTime Birthdate { get; private set; }

        [Required(ErrorMessage = "O {0} é  um campo Obrigatorio")]
        [MinLength(3, ErrorMessage = "O {0} deve ter no minimo 3 caracteres")]
        [Display(Name = "Nome")]
        public string FirstName { get; private set; }

        [Required(ErrorMessage = "O {0} é  um campo Obrigatorio")]
        [MinLength(3, ErrorMessage = "O {0} deve ter no minimo 3 caracteres")]
        [Display(Name="Sobrenome")]
        public string LastName { get; private set; }

        public CPF CPF { get; private set; }

        public Email Email { get; private set; }

        [Required(ErrorMessage = "O {0} é  um campo Obrigatorio")]
        [MinLength(3, ErrorMessage = "O {0} deve ter no minimo 8 caracteres")]
        [Display(Name = "Telefone")]
        public string Phone { get; private set; }

        [Display(Name = "Genero")]
        public string Gender { get; private set; }

        [Required(ErrorMessage = "O {0} é  um campo Obrigatorio")]
        [Display(Name = "País")]
        public string Country { get; private set; }

        [Required(ErrorMessage = "O {0} é  um campo Obrigatorio")]
        [Display(Name = "Cidade")]
        public string City { get; private set; }

        [Required(ErrorMessage = "O {0} é  um campo Obrigatorio")]
        [Display(Name = "Endereço")]
        public string Address { get; private set; }

        // Editar apenas Course da Student
        public void UpdateEntity(Course course)
        {
            Course = course;
        }
        // Editar Student
        public void UpdateEntity(Course course, DateTime birthdate, string firstName, string lastName, CPF cpf, Email email, string phone, string gender, string country, string city, string address)
        {
            Course = course;
            Birthdate = birthdate;
            FirstName = firstName;
            LastName = lastName;
            CPF = cpf;
            Email = email;
            Phone = phone;
            Gender = gender;
            Country = country;
            City = city;
            Address = address;
        }

        // private void Validation(Course course, DateTime birthdate, string firstName, string lastName,  string //cpf, string email, string phone, string gender, string country, string city, string address)
        // {
        //     if (FirstName.Length + 1 < 3 || FirstName == null)
        //     {
        //         ModelState.AddModelError("FirstName", "O Nome deve ter no minimo 3 caracteres");
        //         
        //     }
        //     if (LastName.Length + 1 < 3 || LastName == null)
        //     {
        //         ModelState.AddModelError("LastName", "O Sobrenome deve ter no minimo 3 caracteres");
        //         
        //     }
        //     if (Phone.Length + 1 < 8 || Phone == null)
        //     {
        //         ModelState.AddModelError("Telefone", "O Telefone deve ter no minimo 8 caracteres");
        //         
        //     }
        // 
        //     Regex rg = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~ \w])/*)/(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA- Z]//{2,6}))$");
        // 
        //     if (!rg.IsMatch(Email))
        //     {
        //         ModelState.AddModelError("Email", "Email Inválido!");
        //         
        //     }
        // 
        //     if (!IsCpf(CPF))
        //     {
        //         ModelState.AddModelError("CPF", "CPF Inválido!");
        //         
        //     }
        // 
        //     if (student1.CPF == CPF)
        //     {
        //         ModelState.AddModelError("CPF", "CPF já foi cadastrado!");
        //         
        //     }
        // 
        //     if (Address == string.Empty || Address == null)
        //     {
        //         ModelState.AddModelError("Address", "O endereço é campo obrigatorio!");
        //         
        //     }
        //     if (City == string.Empty || City == null)
        //     {
        //         ModelState.AddModelError("City", "A cidade é campo obrigatorio!");
        //         
        //     }
        //     if (Country == string.Empty || Country == null)
        //     {
        //         ModelState.AddModelError("Country", "O país é campo obrigatorio!");
        //         
        //     }
        //     if (Email == string.Empty || Email == null)
        //     {
        //         ModelState.AddModelError("Email", "O Email é campo obrigatorio!");
        //         
        //     }
        // }
    }
}
