using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Database.Entities;

namespace Workbit.Infrastructure.Database.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasOne(c => c.Ceo)
                   .WithMany()  // No back-reference from Manager
                   .HasForeignKey(c => c.CeoId)
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
                    CeoId = Guid.Parse("a1111111-0000-0000-0000-000000000001") // John Lewis (CEO)
                }
            );
        }
    }
}
