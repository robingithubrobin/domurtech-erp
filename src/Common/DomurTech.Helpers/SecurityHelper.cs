using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DomurTech.Helpers
{
    public static class SecurityHelper
    {
        private const string InitVector = "6X#ny0Y-!i1WbJ%2";
        private const int KeySize = 256;
        private const int PasswordIterations = 10000;
        private const string SaltValue = "1jT?!C6rSm-05By%b8#W7+Fo@Gf32z#XwE9!4q+Y@9PeZ%g6-2HcpQ?1?5Jta+8R";
        private const string PassPhrase = "y+H6#A5p";

        public static string Encrypt(this string plainText)
        {
            string encryptedText;
            var initVectorBytes = Encoding.UTF8.GetBytes(InitVector);
            var passwordBytes = Encoding.UTF8.GetBytes(PassPhrase);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var saltValueBytes = Encoding.UTF8.GetBytes(SaltValue);
            var password = new Rfc2898DeriveBytes(passwordBytes, saltValueBytes, PasswordIterations);
            var keyBytes = password.GetBytes(KeySize / 8);
            var rijndaelManaged = new RijndaelManaged { Mode = CipherMode.CBC };
            using (var encryptor = rijndaelManaged.CreateEncryptor(keyBytes, initVectorBytes))
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
                        )
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        var cipherTextBytes = memoryStream.ToArray();
                        encryptedText = Convert.ToBase64String(cipherTextBytes);
                    }
                }
            }
            return encryptedText;
        }

        public static string Decrypt(this string encryptedText)
        {
            var encryptedTextBytes = Convert.FromBase64String(encryptedText);
            var initVectorBytes = Encoding.UTF8.GetBytes(InitVector);
            var passwordBytes = Encoding.UTF8.GetBytes(PassPhrase);
            string plainText;
            var saltValueBytes = Encoding.UTF8.GetBytes(SaltValue);
            var password = new Rfc2898DeriveBytes(passwordBytes, saltValueBytes, PasswordIterations);
            var keyBytes = password.GetBytes(KeySize / 8);
            var rijndaelManaged = new RijndaelManaged { Mode = CipherMode.CBC };
            using (var decryptor = rijndaelManaged.CreateDecryptor(keyBytes, initVectorBytes))
            {
                using (var memoryStream = new MemoryStream(encryptedTextBytes))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        var plainTextBytes = new byte[encryptedTextBytes.Length];
                        var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                        plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                    }
                }
            }
            return plainText;
        }
        
        public static bool ValidatePassword(string password, int minLength = 8, int numUpper = 1, int numLower = 1, int numNumbers = 1, int numSpecial = 1)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            var upper = new System.Text.RegularExpressions.Regex("[A-Z]");
            var lower = new System.Text.RegularExpressions.Regex("[a-z]");
            var number = new System.Text.RegularExpressions.Regex("[0-9]");
            var special = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]");

            if (password.Length < minLength)
            {
                return false;
            }

            if (upper.Matches(password).Count < numUpper)
            {
                return false;
            }
            if (lower.Matches(password).Count < numLower)
            {
                return false;
            }
            if (number.Matches(password).Count < numNumbers)
            {
                return false;
            }
            return special.Matches(password).Count >= numSpecial;
        }

        public static string ToSha512(this string str)
        {
            using (var hashTool = SHA512.Create())
            {
                var encryptedBytes = hashTool.ComputeHash(Encoding.UTF8.GetBytes(str));
                var stringBuilder = new StringBuilder();
                foreach (var t in encryptedBytes)
                {
                    stringBuilder.Append(t.ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }

        public static string CreatePassword(int length)
        {
            const string upperCaseChars = "ABCDEFGHJKLMNPQRSTWXYZ";
            const string lowerCaseChars = "abcdefgijkmnopqrstwxyz";
            const string numericChars = "0123456789";
            const string specialChars = "%#+-!@?";
            var charGroups = new[]
            {
                upperCaseChars.ToCharArray(),
                lowerCaseChars.ToCharArray(),
                numericChars.ToCharArray(),
                specialChars.ToCharArray()
            };
            var charsLeftInGroup = new int[charGroups.Length];
            for (var i = 0; i < charsLeftInGroup.Length; i++)
            {
                charsLeftInGroup[i] = charGroups[i].Length;
            }
            var leftGroupsOrder = new int[charGroups.Length];
            for (var i = 0; i < leftGroupsOrder.Length; i++)
            {
                leftGroupsOrder[i] = i;
            }
            var randomBytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            var seed = BitConverter.ToInt32(randomBytes, 0);
            var random = new Random(seed);
            var password = new char[length];
            var lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
            for (var i = 0; i < password.Length; i++)
            {
                var nextLeftGroupsOrderIdx = lastLeftGroupsOrderIdx == 0 ? 0 : random.Next(0, lastLeftGroupsOrderIdx);
                var nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];
                var lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;
                var nextCharIdx = lastCharIdx == 0 ? 0 : random.Next(0, lastCharIdx + 1);
                password[i] = charGroups[nextGroupIdx][nextCharIdx];
                if (lastCharIdx == 0)
                {
                    charsLeftInGroup[nextGroupIdx] = charGroups[nextGroupIdx].Length;
                }
                else
                {
                    if (lastCharIdx != nextCharIdx)
                    {
                        var temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] = charGroups[nextGroupIdx][nextCharIdx];
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }
                    charsLeftInGroup[nextGroupIdx]--;
                }
                if (lastLeftGroupsOrderIdx == 0)
                {
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                }
                else
                {
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        var temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] = leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }
                    lastLeftGroupsOrderIdx--;
                }
            }
            return new string(password);
        }
    }
}
