using College.Entities.Account.Entities;
using College.Entities.Professor.Enumerators;
using College.Entities.Shared;

namespace College.Entities.Professor.Entities
{
    public class Professor : User
    {
        public Professor(string firstName, string lastName, string cpf, string email, string phone, EDegree degree) : base(email, cpf)
        {

            if (string.IsNullOrEmpty(firstName) || firstName.Length < 3)
            {
                Notifications.Add("FirstName", "O Nome deve ter no minimo 3 caracteres");
            }
            if (string.IsNullOrEmpty(lastName) || lastName.Length < 3)
            {
                Notifications.Add("LastName", "O Sobrenome deve ter no minimo 3 caracteres");
            }
            if (string.IsNullOrEmpty(phone) || phone.Length < 8)
            {
                Notifications.Add("Telefone", "O Telefone deve ter no minimo 8 caracteres");
            }

            Email = new Email(email);
            Notifications.Add("Email", Email.Notification);
            CPF = new CPF(cpf);
            Notifications.Add("CPF", Email.Notification);

            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Degree = degree;
        }

        public string Name { get => $"{FirstName} {LastName}"; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public CPF CPF { get; private set; }

        public Email Email { get; private set; }

        public string Phone { get; private set; }

        public EDegree Degree { get; private set; }

        // Editar apenas EDegree da Professor
        public void UpdateEntity(EDegree degree)
        {
            Degree = degree;
        }
        // Editar Professor
        public void UpdateEntity(string firstName, string lastName, string cpf, string email, string phone, EDegree degree)
        {

            if (string.IsNullOrEmpty(firstName) || firstName.Length < 3)
            {
                Notifications.Add("FirstName", "O Nome deve ter no minimo 3 caracteres");
            }
            if (string.IsNullOrEmpty(lastName) || lastName.Length < 3)
            {
                Notifications.Add("LastName", "O Sobrenome deve ter no minimo 3 caracteres");
            }
            if (string.IsNullOrEmpty(phone) || phone.Length < 8)
            {
                Notifications.Add("Telefone", "O Telefone deve ter no minimo 8 caracteres");
            }

            Email = new Email(email);
            Notifications.Add("Email", Email.Notification);
            CPF = new CPF(cpf);
            Notifications.Add("CPF", Email.Notification);

            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Degree = degree;
        }
    }
}
