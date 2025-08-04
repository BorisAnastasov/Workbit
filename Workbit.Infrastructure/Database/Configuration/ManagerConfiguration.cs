using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Configuration
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        private readonly IDataProtector protector;

        public ManagerConfiguration(IDataProtector _protector)
        {
            protector = _protector;
        }
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            var converter = new ValueConverter<string, string>(
                    plain => plain == null ? null! : protector.Protect(plain),
                    cipher => cipher == null ? null! : protector.Unprotect(cipher));

            builder.Property(e => e.IBAN)
                                .HasConversion(converter)
                                .HasColumnType("nvarchar(max)")
                                .IsRequired();

            builder.HasData(
    // UK Managers
    new Manager
    {
        ApplicationUserId = Guid.Parse("f83d8c21-0b43-4b15-8fd1-20f4e5c72f02"),
        DepartmentId = 1,
        IBAN = "GB29NWBK60161331926819"
    },
    new Manager
    {
        ApplicationUserId = Guid.Parse("befa88e8-83fc-4b64-b4ce-7de0b97b6e51"),
        DepartmentId = 1,
        IBAN = "GB82WEST12345698765432"
    },

    // Germany Managers
    new Manager
    {
        ApplicationUserId = Guid.Parse("64d07af7-8ed1-4620-b34d-bd0a4cb81d03"),
        DepartmentId = 2,
        IBAN = "DE89370400440532013000"
    },
    new Manager
    {
        ApplicationUserId = Guid.Parse("b06c8a25-b13c-4d31-bb49-113aa0cc46b8"),
        DepartmentId = 2,
        IBAN = "DE21100100101234567893"
    },

    // France Managers
    new Manager
    {
        ApplicationUserId = Guid.Parse("b0cf2834-19c5-43f5-b29e-9bb85a5a5d04"),
        DepartmentId = 3,
        IBAN = "FR1420041010050500013M02606"
    },
    new Manager
    {
        ApplicationUserId = Guid.Parse("b23ae748-2292-4712-8778-3eb591c2f001"),
        DepartmentId = 3,
        IBAN = "FR7630006000011234567890189"
    }
);

        }

    }
}
