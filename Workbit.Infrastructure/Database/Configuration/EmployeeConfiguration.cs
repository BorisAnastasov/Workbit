using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Enumerations;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasOne(e => e.ApplicationUser)
                   .WithOne()
                   .HasForeignKey<Employee>(e => e.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Job)
                   .WithMany(j => j.Employees)
                   .HasForeignKey(e => e.JobId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Payments)
                   .WithOne(p => p.Employee)
                   .HasForeignKey(p => p.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Employee { ApplicationUserId = Guid.Parse("f92e7b0f-5123-40c8-9d28-8834a3c93005"), JobId = 1, Level = JobLevel.Mid },     // Alice Watson
                new Employee { ApplicationUserId = Guid.Parse("2b06417a-1460-4b10-8454-51069dfb2d06"), JobId = 1, Level = JobLevel.Junior }, // Bob Thomas
                new Employee { ApplicationUserId = Guid.Parse("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"), JobId = 2, Level = JobLevel.Lead },   // Claire James
                new Employee { ApplicationUserId = Guid.Parse("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08"), JobId = 2, Level = JobLevel.Senior }, // Dave Walker
                new Employee { ApplicationUserId = Guid.Parse("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09"), JobId = 3, Level = JobLevel.Mid },    // Emily Young
                new Employee { ApplicationUserId = Guid.Parse("a73cb7de-df18-4b6e-a573-0dcf1f703e10"), JobId = 3, Level = JobLevel.Junior }, // Frank Scott
                new Employee { ApplicationUserId = Guid.Parse("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11"), JobId = 4, Level = JobLevel.Senior }, // Grace Adams
                new Employee { ApplicationUserId = Guid.Parse("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012"), JobId = 4, Level = JobLevel.Lead }    // Harry Brooks
            );
        }
    }
}
