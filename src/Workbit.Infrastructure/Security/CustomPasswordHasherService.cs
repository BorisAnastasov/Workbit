using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using Workbit.Application.Interfaces;
using Workbit.Domain.Entities.Account;

namespace Workbit.Infrastructure.Security
{
    public class CustomPasswordHasherService : ICustomPasswordHasherService
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100_000;
        public string HashPassword(ApplicationUser user, string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);

            var hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: HashSize);

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword)
        {
            var parts = hashedPassword.Split('.');
            if (parts.Length != 2)
                return PasswordVerificationResult.Failed;

            byte[] salt, expectedHash;
            try
            {
                salt = Convert.FromBase64String(parts[0]);
                expectedHash = Convert.FromBase64String(parts[1]);
            }
            catch (FormatException)
            {
                return PasswordVerificationResult.Failed;
            }

            var actualHash = KeyDerivation.Pbkdf2(
                password: providedPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: HashSize);

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash)
                ? PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed;
        }
    }
}
