namespace College.UseCases.Services
{
    public interface IEncrypt
    {
        // Retorna uma nova senha encriptografada criando um novo sal
        string Encrypt(string password);
        // Retorna uma senha encriptografada com base em um sal já existente
        string Encrypt(string password, string salt);
    }
}
