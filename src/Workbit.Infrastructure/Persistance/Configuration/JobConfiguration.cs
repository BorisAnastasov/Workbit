using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Domain.Entities;

namespace Workbit.Infrastructure.Persistance.Configuration
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasData(
                new Job
                {
                    Id = 1,
                    Title = "Software Engineer",
                    Description = "Develops and maintains enterprise-grade software applications.",
                    DepartmentId = 2, // IT
                    BaseSalary = 6000
                },
                new Job
                {
                    Id = 2,
                    Title = "HR Specialist",
                    Description = "Manages employee onboarding, benefits, and HR compliance.",
                    DepartmentId = 1, // Human Resources
                    BaseSalary = 4500
                },
                new Job
                {
                    Id = 3,
                    Title = "Financial Analyst",
                    Description = "Analyzes financial data and prepares budget reports for strategic decisions.",
                    DepartmentId = 3, // Finance
                    BaseSalary = 5000
                },
                new Job
                {
                    Id = 4,
                    Title = "Marketing Coordinator",
                    Description = "Supports marketing campaigns, communications, and brand development.",
                    DepartmentId = 4, // Marketing
                    BaseSalary = 4000
                },
                new Job
                {
                    Id = 5,
                    Title = "Sales Representative",
                    Description = "Manages client relationships and drives new business acquisition.",
                    DepartmentId = 5, // Sales
                    BaseSalary = 4800
                }
            );
        }
    }
}