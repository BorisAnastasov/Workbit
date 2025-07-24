using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Configuration
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            // Each manager belongs to a department now (no join table)
            builder.HasOne(m => m.Department)
                   .WithMany(d => d.Managers)
                   .HasForeignKey(m => m.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Manager
                {
                    ApplicationUserId = Guid.Parse("a1111111-0000-0000-0000-000000000001"), // John Lewis (CEO)
                    OfficePhone = "+1-555-1010",
                    DepartmentId = 1, // Assume he oversees Human Resources as CEO
                    IsCeo = true
                },
                new Manager
                {
                    ApplicationUserId = Guid.Parse("a1111111-0000-0000-0000-000000000002"), // Lisa Anderson (HR Manager)
                    OfficePhone = "+1-555-2020",
                    DepartmentId = 1,
                    IsCeo = false
                },
                new Manager
                {
                    ApplicationUserId = Guid.Parse("a1111111-0000-0000-0000-000000000003"), // Carl Morgan (IT Manager)
                    OfficePhone = "+1-555-3030",
                    DepartmentId = 2,
                    IsCeo = false
                },
                new Manager
                {
                    ApplicationUserId = Guid.Parse("a1111111-0000-0000-0000-000000000004"), // Nina Hughes (Finance Manager)
                    OfficePhone = "+1-555-4040",
                    DepartmentId = 3,
                    IsCeo = false
                }
            );
        }
    }
}
