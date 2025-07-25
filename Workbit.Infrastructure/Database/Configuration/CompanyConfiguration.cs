using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Database.Entities;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasOne(c => c.Ceo)
                        .WithOne()
                        .HasForeignKey<Company>(c => c.CeoId)
                        .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Departments)
                   .WithOne(d => d.Company)
                   .HasForeignKey(d => d.CompanyId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Company
                {
                    Id = 1,
                    Name = "Workbit Solutions Inc.",
                    Address = "123 Business Blvd, New York, NY, USA",
                    ContactPhone = "+1-555-0000",
                    CeoId = Guid.Parse("9a2f4b30-c2fa-4c77-bf3a-9b6a4cf11801")
                }
            );
        }
    }
}
