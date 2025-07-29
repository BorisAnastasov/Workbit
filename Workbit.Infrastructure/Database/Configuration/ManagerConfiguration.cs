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
                    ApplicationUserId = Guid.Parse("f83d8c21-0b43-4b15-8fd1-20f4e5c72f02"), // Lisa Anderson (HR Manager)
                    DepartmentId = 1
                },
                new Manager
                {
                    ApplicationUserId = Guid.Parse("64d07af7-8ed1-4620-b34d-bd0a4cb81d03"), // Carl Morgan (IT Manager)
                    DepartmentId = 2
                },
                new Manager
                {
                    ApplicationUserId = Guid.Parse("b0cf2834-19c5-43f5-b29e-9bb85a5a5d04"), // Nina Hughes (Finance Manager)
                    DepartmentId = 3
                }
            );
        }
    }
}
