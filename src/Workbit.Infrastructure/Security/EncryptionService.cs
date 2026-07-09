using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Workbit.Application.Common.Models;
using Workbit.Application.Interfaces;

namespace Workbit.Infrastructure.Security
{
    public class EncryptionService : IEncryptionService
    {
        private readonly IDataProtector protector;

        public EncryptionService(IDataProtectionProvider provider)
        {
            protector = provider.CreateProtector("Workbit.Iban");
        }

        public string Encrypt(string plaintext)
        {
            if (string.IsNullOrEmpty(plaintext)) return plaintext;

            return protector.Protect(plaintext);
        }

        public string Decrypt(string ciphertext)
        {
            if (string.IsNullOrEmpty(ciphertext)) return ciphertext;

            return protector.Unprotect(ciphertext);
        }
    }
}
