using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace TDD.Password
{
    public class UserPassword
    {
        public string Password { get; set; }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(Password) && Regex.IsMatch(Password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{5,10}$");
            }
        }

        public int Length
        {
            get
            {
                return Password.Length;
            }
        }

        public UserPassword(string password)
        {
            Password = password;
        }

        public UserPassword()
        {
            var rnd = new Random();
            var strLength = rnd.Next(5, 10);
            Password = GeneratePassword(strLength);
        }

        private string GeneratePassword(int length)
        {
            var complexity = 10;
            System.Security.Cryptography.RNGCryptoServiceProvider csp =
                new System.Security.Cryptography.RNGCryptoServiceProvider();
            char[][] classes =
            {
                @"abcdefghijklmnopqrstuvwxyz".ToCharArray(),
                @"ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),
                @"0123456789".ToCharArray(),
                @" !""#$%&'()*+,./:;<>?@[\]^_{|}~".ToCharArray(),
            };

            complexity = Math.Max(1, Math.Min(classes.Length, complexity));

            char[] allchars = classes.Take(complexity).SelectMany(c => c).ToArray();
            byte[] bytes = new byte[allchars.Length];
            csp.GetBytes(bytes);
            for (int i = 0; i < allchars.Length; i++)
            {
                char tmp = allchars[i];
                allchars[i] = allchars[bytes[i] % allchars.Length];
                allchars[bytes[i] % allchars.Length] = tmp;
            }

            Array.Resize(ref bytes, length);
            char[] result = new char[length];

            while (true)
            {
                csp.GetBytes(bytes);

                for (int i = 0; i < length; i++)
                {
                    result[i] = allchars[bytes[i] % allchars.Length];
                }

                if (Char.IsWhiteSpace(result[0]) || Char.IsWhiteSpace(result[(length - 1) % length]))
                {
                    continue;
                }
                    
                string testResult = new string(result);

                if (0 != classes.Take(complexity).Count(c => testResult.IndexOfAny(c) < 0))
                {
                    continue;
                }   

                return testResult;
            }
        }
    }
}
