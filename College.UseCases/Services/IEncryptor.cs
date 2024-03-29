﻿namespace College.UseCases.Services
{
    public interface IEncryptor
    {
        // Retorna uma nova senha encriptografada criando um novo sal
        string Encrypt(string password, out string salt);
        // Retorna uma senha encriptografada com base em um sal já existente
        string Encrypt(string password, string salt);
    }
}
