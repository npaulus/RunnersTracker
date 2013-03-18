using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace RunnersTracker.Common
{
    public static class PasswordManagement
    {
        private const int _saltLength = 4;  // set desired salt length here
        

        public static byte[] GenerateSaltedPassword(byte[] password, byte[] salt)
        {
            byte[] PasswordAndSalt = new byte[password.Length + _saltLength];

            HashAlgorithm algorithm = new SHA512Managed(); //set desired hash algorithm
            Array.Copy(password, PasswordAndSalt, password.Length);
            Array.Copy(salt, 0, PasswordAndSalt, password.Length, _saltLength);

            return algorithm.ComputeHash(PasswordAndSalt);

        }

        public static byte[] GenerateSalt()
        {
            byte[] _salt = new byte[_saltLength];

            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

            random.GetNonZeroBytes(_salt);

            return _salt;

        }

        public static bool ComparePasswords(byte[] userPassword, byte[] userEnteredPassword)
        {
            if (userPassword.Length != userEnteredPassword.Length)
            {
                return false;
            }

            for (int i = 0; i < userPassword.Length; i++)
            {
                if (userPassword[i] != userEnteredPassword[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Generates a random string with the given length
        /// </summary>
        /// <param name="size">Size of the string</param>
        /// <param name="lowerCase">If true, generate lowercase string</param>
        /// <returns>Random string</returns>
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

    }
}
