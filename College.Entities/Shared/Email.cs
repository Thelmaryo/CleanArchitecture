using System.Text.RegularExpressions;

namespace College.Entities.Shared
{
    public class Email : ValueObject
    {
        public Email(string address)
        {
            Regex rg = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

            if (string.IsNullOrEmpty(address) || !rg.IsMatch(address))
            {
                Notification = "Email Inválido!";
            }
            Address = address;
        }

        public string Address { get; private set; }
    }
}
