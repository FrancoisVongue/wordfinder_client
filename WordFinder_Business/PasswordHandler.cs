using System;
using System.Security.Cryptography;
using System.Text;
using WordFinder_Domain.Models;

namespace WordFinder_Business
{
    public static class PasswordHandler
    {
        public static bool verifyPassword(string hashedInput, string user_password)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashedInput, user_password) == 0;
        }

        public static string getSalt(int salt_length)
        {
            const string symbols = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            var salt = new StringBuilder();;
            
            for (int i = 0; i < salt_length; i++)
                salt.Append(symbols[random.Next(symbols.Length)]);
            
            return salt.ToString();
        }

        public static string getHashed(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sBuilder = new StringBuilder();
            
                for (int i = 0; i < data.Length; i++)
                    sBuilder.Append(data[i].ToString("x2"));
            
                return sBuilder.ToString();
            }
        }
    }
}