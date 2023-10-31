using System.Security.Cryptography;

namespace PasswordHasher
{
    public class PasswordHasherHelper
    {
        private const int _SaltSize      = 128 / 8;
        private const int _KeySize       = 256 / 8;
        private const int _Iterations    = 10000;
        private readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;

        public byte[] GenerateHash(byte[] salt, string password, out string hashedPass)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _Iterations, _hashAlgorithmName, _KeySize);
            hashedPass = Convert.ToBase64String(hash);
            return hash;
        }


        public byte[] GenerateSalt(out string salted)
        {
            var salt = RandomNumberGenerator.GetBytes(_SaltSize);
            salted = Convert.ToBase64String(salt);
            return salt;
        }


        public (string Hash, string Salt) CreateHash(string password)
        {
            string saltString = string.Empty;
            byte[] salt = GenerateSalt(out saltString);

            string hashString = string.Empty;
            byte[] hash = GenerateHash(salt, password ,out hashString);

            return (hashString, saltString);
        }


        public bool VerifyPassword(byte[] salt, byte[] hash, string inputPassword)
        {
            var inputHash = GenerateHash(salt, inputPassword, out _);
            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }


        public bool VerifyPassword(string salt, string hash, string inputPassword)
        {
            byte[] saltByte = Convert.FromBase64String(salt);
            byte[] hashByte = Convert.FromBase64String(hash);

            var inputHash = GenerateHash(saltByte, inputPassword, out _);
            return CryptographicOperations.FixedTimeEquals(hashByte, inputHash);
        }
    }
}