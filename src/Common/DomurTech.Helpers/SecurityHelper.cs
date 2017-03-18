using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DomurTech.Helpers
{
    public static class SecurityHelper
    {
        private const string Base64Key = "+eoeofewQqQflkspAwVCXZ43291HOtcrIoe3w+8!9jnP=(&N++5XYi0rU=";
        private static readonly byte[] Key = Convert.FromBase64String(Base64Key);
        private const int KeySize = 256;
        public static string Encrypt(this string plainText)
        {
            if (Key == null || Key.Length <= 0)
            {
                throw new ArgumentNullException();
            }
            byte[] returnValue;
            using (var aes = Aes.Create())
            {
                if (aes == null)
                {
                    throw new Exception("AES not create");
                }
                aes.KeySize = KeySize;
                aes.GenerateIV();
                aes.Mode = CipherMode.CBC;
                var iv = aes.IV;
                if (string.IsNullOrEmpty(plainText))
                {
                    return Convert.ToBase64String(iv);
                }
                var encryptor = aes.CreateEncryptor(Key, iv);
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        var encrypted = msEncrypt.ToArray();
                        returnValue = new byte[encrypted.Length + iv.Length];
                        Array.Copy(iv, returnValue, iv.Length);
                        Array.Copy(encrypted, 0, returnValue, iv.Length, encrypted.Length);
                    }
                }
            }
            return Convert.ToBase64String(returnValue);
        }

        public static string Decrypt(this string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                return string.Empty;
            }
            if (Key == null || Key.Length <= 0)
            {
                throw new ArgumentNullException();
            }

            string plaintext;
            var allBytes = Convert.FromBase64String(cipherText);

            using (var aes = Aes.Create())
            {
                if (aes == null)
                {
                    throw new Exception("AES not create");
                }
                aes.KeySize = KeySize;
                aes.Mode = CipherMode.CBC;
                var iv = new byte[aes.BlockSize / 8];
                if (allBytes.Length < iv.Length)
                {
                    throw new ArgumentException("Message was less than IV size.");
                }
                Array.Copy(allBytes, iv, iv.Length);
                var cipherBytes = new byte[allBytes.Length - iv.Length];
                Array.Copy(allBytes, iv.Length, cipherBytes, 0, cipherBytes.Length);
                var decryptor = aes.CreateDecryptor(Key, iv);
                using (var msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
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
