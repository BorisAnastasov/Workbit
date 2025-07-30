using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Configuration
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasData(
                new Manager
                {
                    ApplicationUserId = Guid.Parse("f83d8c21-0b43-4b15-8fd1-20f4e5c72f02"), // Lisa Anderson (UK/GB)
                    DepartmentId = 1,
                    IBAN = "GB29NWBK60161331926819" // United Kingdom
                },
                new Manager
                {
                    ApplicationUserId = Guid.Parse("64d07af7-8ed1-4620-b34d-bd0a4cb81d03"), // Carl Morgan (Germany/DE)
                    DepartmentId = 2,
                    IBAN = "DE89370400440532013000" // Germany
                },
                new Manager
                {
                    ApplicationUserId = Guid.Parse("b0cf2834-19c5-43f5-b29e-9bb85a5a5d04"), // Nina Hughes (France/FR)
                    DepartmentId = 3,
                    IBAN = "FR1420041010050500013M02606" // France
                }
            );
        }

    }
}
