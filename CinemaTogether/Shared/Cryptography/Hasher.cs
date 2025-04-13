using System.Security.Cryptography;

namespace Shared.Cryptography
{
    public static class Hasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int IterationsCount = 50000;
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

        public static string Hash(string input)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(input, salt, IterationsCount, Algorithm, KeySize);
            var result = new byte[SaltSize + KeySize];
            Array.Copy(salt, 0, result, 0, SaltSize);
            Array.Copy(hash, 0, result, SaltSize, KeySize);
            return Convert.ToHexString(result);
        }

        public static bool Verify(string input, string hashString)
        {
            var bytes = Convert.FromHexString(hashString);
            var salt = bytes[0..SaltSize];
            var hash = bytes[SaltSize..];
            var inputHash = Rfc2898DeriveBytes.Pbkdf2(input, salt, IterationsCount, Algorithm, KeySize);
            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }
    }
}
