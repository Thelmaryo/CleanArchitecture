using College.UseCases.Services;
using System;
using System.Security.Cryptography;

namespace Cryptography.EncryptContext
{
    public class Encryptor : IEncryptor
    {
        // Retorna uma nova senha encriptografada criando um novo sal
        public string Encrypt(string password, out string salt)
        {
            // Cria um salt aleatório de 64 bits
            byte[] hash = new byte[8];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                // Enche o array com um valor aleatório
                rngCsp.GetBytes(hash);
            }
            // Escolha o valor mais alto que seja "tolerável"
            // 100 000 era um valor razoável em 2011, não sei se é suficiente hoje
            int myIterations = 100000;
            Rfc2898DeriveBytes k = new Rfc2898DeriveBytes(password, hash, myIterations);
            salt = String.Join(",", hash);

            var Password = Convert.ToBase64String(k.GetBytes(32));
            return Password;
        }
        // Retorna uma senha encriptografada com base em um sal já existente
        public string Encrypt(string password, string salt)
        {

        }

    }
}
