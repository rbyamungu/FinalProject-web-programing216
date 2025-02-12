using System;
using System.Security.Cryptography;

namespace HalfAndHalf.Helpers
{
    public class PasswordHelper
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;

        public static string HashPassword(string password, out string salt)
        {
            byte[] saltBytes = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            salt = Convert.ToBase64String(saltBytes);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedSalt, string storedHash)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, saltBytes, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] enteredHash = pbkdf2.GetBytes(HashSize);
                return Convert.ToBase64String(enteredHash) == storedHash;
            }
        }
    }
}
