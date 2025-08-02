using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Database.Entities;

namespace Workbit.Infrastructure.Database.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.HasData(
    new Department
    {
        Id = 1,
        Name = "Human Resources",
        Description = "Handles recruitment, payroll, employee relations, and organizational policies.",
        CompanyId = 1,
    },
    new Department
    {
        Id = 2,
        Name = "IT",
        Description = "Oversees infrastructure, software development, cybersecurity, and IT support.",
        CompanyId = 1,
    },
    new Department
    {
        Id = 3,
        Name = "Finance",
        Description = "Manages budgeting, forecasting, accounting, and financial reporting.",
        CompanyId = 1,
    },
    new Department
    {
        Id = 4,
        Name = "Marketing",
        Description = "Develops brand strategy, campaigns, and customer outreach initiatives.",
        CompanyId = 1,
    }
);

        }
    }
}
