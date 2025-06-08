using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VIRTUAL_ASSISTANT.Application.Helpers
{
    public class ApplyHash
    {
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
#pragma warning disable SYSLIB0023
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
#pragma warning restore SYSLIB0023
            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPassword(string password, string salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Convert.FromBase64String(salt);

            byte[] allBytes = new byte[passwordBytes.Length + saltBytes.Length];
            Buffer.BlockCopy(passwordBytes, 0, allBytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, allBytes, passwordBytes.Length, saltBytes.Length);

            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(allBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static string GenerateRandomPassword(int maxLength = 15)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+";
            Random random = new Random();
            StringBuilder password = new StringBuilder();

            int passwordLength = random.Next(8, maxLength + 1);

            for (int i = 0; i < passwordLength; i++)
            {
                password.Append(chars[random.Next(chars.Length)]);
            }

            return password.ToString();
        }
    }
}
