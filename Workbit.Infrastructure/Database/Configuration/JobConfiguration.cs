using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Database.Entities;

namespace Workbit.Infrastructure.Database.Configuration
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasOne(j => j.Department)
                   .WithMany(d => d.Jobs)
                   .HasForeignKey(j => j.DepartmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Job
                {
                    Id = 1,
                    Title = "Software Engineer",
                    Description = "Develops and maintains enterprise-grade software applications.",
                    DepartmentId = 2, // IT
                    BaseSalary = 6000 // Monthly
                },
                new Job
                {
                    Id = 2,
                    Title = "HR Specialist",
                    Description = "Manages employee onboarding, benefits, and HR compliance.",
                    DepartmentId = 1, // Human Resources
                    BaseSalary = 4500 // Monthly
                },
                new Job
                {
                    Id = 3,
                    Title = "Financial Analyst",
                    Description = "Analyzes financial data and prepares budget reports for strategic decisions.",
                    DepartmentId = 3, // Finance
                    BaseSalary = 5000 // Monthly
                },
                new Job
                {
                    Id = 4,
                    Title = "Marketing Coordinator",
                    Description = "Supports marketing campaigns, communications, and brand development.",
                    DepartmentId = 4, // Marketing
                    BaseSalary = 4000 // Monthly
                }
            );
        }
    }
}
