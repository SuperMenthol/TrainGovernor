using System.Security.Cryptography;

namespace Infrastructure.Helpers.Login
{
    public static class PasswordEncryptor
    {
        // https://stackoverflow.com/a/10402129
        public static string GetEncryptedPassword(string input)
        {
            byte[] salt = new byte[16];
            RandomNumberGenerator.Create().GetBytes(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(input, salt, 11);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }
    }
}