
using System.Security.Cryptography;
using System.Text;

using Trade_Test.Models;
using Trade_Test.Models.Enums;

namespace Trade_Test.Utilities.Extensions
{
    public class Security
    {
        private int _saltSize = 256;
        private uint _hashingParameter = 5000;
        private byte[] _hash;
        private byte[] _salt;
        private bool IsValid = false;

        private static readonly byte[] SaltForSalt = Convert.FromBase64String("1029385yr756ry3ye0e8tu54");

        public static string GenerateToken()
        {
            string availableChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            byte[] tokenData = new byte[32];
            var rngCsp = RandomNumberGenerator.Create();
            rngCsp.GetBytes(tokenData);
            var chars = tokenData.Select(b => availableChars[b % availableChars.Length]);
            return new string(chars.ToArray());
        }

        public static string HashUsingSalt(string ValueToHash, string UserSaltBase64Encoded)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(ValueToHash);
            byte[] Salt = GetSaltHash(UserSaltBase64Encoded);
            KeyedHashAlgorithm hashAlgorithm = new HMACSHA512(Salt);
            return Convert.ToBase64String(hashAlgorithm.ComputeHash(bytes));
        }

        private static byte[] GetSaltHash(string salt)
        {
            byte[] src = Convert.FromBase64String(salt);
            KeyedHashAlgorithm hashAlgorithm = new HMACSHA512(SaltForSalt);
            byte[] result = hashAlgorithm.ComputeHash(src);
            return result;
        }

        public string Hash
        {
            get
            {
                return Convert.ToBase64String(_hash);
            }
        }

        public string Salt
        {
            get
            {
                return Convert.ToBase64String(_salt);
            }
        }

        public static Security GeneratePasswordFromStrategy(HashStrategyKind hashStrategy, string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
            {
                throw new ArgumentNullException(plainPassword);
            }

            var securedPassword = new Security();

            securedPassword.SetHashStrategy(hashStrategy);

            switch (hashStrategy)
            {
                case HashStrategyKind.Pbkdf25009Iterations:
                case HashStrategyKind.Pbkdf28000Iterations:
                    using (var deriveBytes = new Rfc2898DeriveBytes(plainPassword, securedPassword._saltSize, (int)securedPassword._hashingParameter))
                    {
                        securedPassword._salt = deriveBytes.Salt;
                        securedPassword._hash = deriveBytes.GetBytes(securedPassword._saltSize);
                    }
                    break;

                case HashStrategyKind.HMACSHA512:
                    byte[] data = new byte[0x10];
                    var rngCsp = RandomNumberGenerator.Create();
                    rngCsp.GetBytes(data);
                    securedPassword._salt = data;
                    KeyedHashAlgorithm hashAlgorithmSalt = new HMACSHA512(SaltForSalt);
                    var saltForSalt = hashAlgorithmSalt.ComputeHash(data);
                    KeyedHashAlgorithm hashAlgorithm = new HMACSHA512(saltForSalt);
                    securedPassword._hash = hashAlgorithm.ComputeHash(Encoding.Unicode.GetBytes(plainPassword));
                    break;
            }

            return securedPassword;
        }

        public static bool IsValidPassword(string PasswordToValidate, PasswordOptions PasswordToMatch)
        {

            var securedPassword = new Security
            {
                _hash = PasswordToMatch.HashedPassword,
                _salt = PasswordToMatch.Salt
            };
            securedPassword.SetHashStrategy(PasswordToMatch.HashStrategy);

            byte[] newKey;

            switch (PasswordToMatch.HashStrategy)
            {
                case HashStrategyKind.Pbkdf25009Iterations:
                case HashStrategyKind.Pbkdf28000Iterations:
                    using (var deriveBytes = new Rfc2898DeriveBytes(PasswordToValidate, PasswordToMatch.Salt, (int)securedPassword._hashingParameter))
                    {
                        newKey = deriveBytes.GetBytes(securedPassword._saltSize);
                        securedPassword.IsValid = newKey.SequenceEqual(PasswordToMatch.HashedPassword);
                    }
                    break;

                //TODO: implement / use a .net core compatible password hasher 
                //case HashStrategyKind.Argon248KWorkCost:
                //    var argon2Hasher = new PasswordHasher(memoryCost: securedPassword._hashingParameter);
                //    newKey = Encoding.ASCII.GetBytes(argon2Hasher.Hash(Encoding.ASCII.GetBytes(PasswordToValidate), PasswordToMatch.Salt));
                //    securedPassword.IsValid = newKey.SequenceEqual(PasswordToMatch.HashedPassword);
                //    break;

                case HashStrategyKind.HMACSHA512:
                    KeyedHashAlgorithm hashAlgorithmSalt = new HMACSHA512(SaltForSalt);
                    var saltForSalt = hashAlgorithmSalt.ComputeHash(PasswordToMatch.Salt);
                    KeyedHashAlgorithm hashAlgorithmPassword = new HMACSHA512(saltForSalt);
                    newKey = hashAlgorithmPassword.ComputeHash(Encoding.Unicode.GetBytes(PasswordToValidate));
                    securedPassword.IsValid = newKey.SequenceEqual(PasswordToMatch.HashedPassword);

                    break;
                default:
                    throw new ArgumentException($"hashStrategy {PasswordToMatch.HashStrategy} is not defined");
            }


            return securedPassword.IsValid;
        }

        private void SetHashStrategy(HashStrategyKind hashStrategy)
        {
            switch (hashStrategy)
            {
                case HashStrategyKind.Pbkdf25009Iterations:
                    _hashingParameter = 5009;
                    _saltSize = 256;
                    break;
                case HashStrategyKind.Pbkdf28000Iterations:
                    _hashingParameter = 8000;
                    _saltSize = 256;
                    break;
                case HashStrategyKind.Argon248KWorkCost:
                    _hashingParameter = 48000;
                    _saltSize = 0;
                    break;
                case HashStrategyKind.HMACSHA512:
                    break;

                default:
                    throw new ArgumentException($"hashStrategy {hashStrategy} is not defined");
            }
        }
    }
}
