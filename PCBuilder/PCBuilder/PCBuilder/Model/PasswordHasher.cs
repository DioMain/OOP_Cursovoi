using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PCBuilder.Model
{
    public static class PasswordHasher
    {
        public const string Salt = "PCBUILDER";

        public static string GetHash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(Salt + password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool Compare(string hash, string password)
        {
            string passHash = GetHash(password);

            return passHash == hash;
        }
    }
}
