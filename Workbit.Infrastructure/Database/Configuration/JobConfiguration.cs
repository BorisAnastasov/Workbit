using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Core.Enumerations;
using Workbit.Infrastructure.Database.Entities;

namespace Workbit.Infrastructure.Database.Configuration
{
	public class JobConfiguration : IEntityTypeConfiguration<Job>
	{
		public void Configure(EntityTypeBuilder<Job> builder)
		{
			builder.HasOne(j => j.Department)
			   .WithMany(d => d.Jobs)
			   .HasForeignKey(j => j.DepartmentId);

			builder.HasData(SeedJobs());
		}

		private static List<Job> SeedJobs()
		{
			return new List<Job>
			{
                // HR
                new Job
				{
					Id = 1,
					Title = "HR Assistant",
					Description = "Supports HR operations including scheduling and onboarding.",
					DepartmentId = 1,
					Level = JobLevel.Junior,
					BaseSalary = 2800
				},
				new Job
				{
					Id = 2,
					Title = "HR Manager",
					Description = "Oversees recruitment, compliance, and employee engagement.",
					DepartmentId = 1,
					Level = JobLevel.Senior,
					BaseSalary = 5200
				},

                // Engineering
                new Job
				{
					Id = 3,
					Title = "Software Engineer",
					Description = "Develops backend services and APIs for core platform.",
					DepartmentId = 2,
					Level = JobLevel.Mid,
					BaseSalary = 4800
				},
				new Job
				{
					Id = 4,
					Title = "Tech Lead",
					Description = "Leads engineering team, architecture design, and code reviews.",
					DepartmentId = 2,
					Level = JobLevel.Lead,
					BaseSalary = 7000
				},

                // Marketing
                new Job
				{
					Id = 5,
					Title = "Content Strategist",
					Description = "Plans and produces content to support digital marketing goals.",
					DepartmentId = 3,
					Level = JobLevel.Mid,
					BaseSalary = 4200
				},
				new Job
				{
					Id = 6,
					Title = "Marketing Director",
					Description = "Drives brand awareness and manages cross-channel campaigns.",
					DepartmentId = 3,
					Level = JobLevel.Senior,
					BaseSalary = 6500
				},

                // Finance
                new Job
				{
					Id = 7,
					Title = "Junior Accountant",
					Description = "Assists with invoicing, expense tracking, and data entry.",
					DepartmentId = 4,
					Level = JobLevel.Junior,
					BaseSalary = 3000
				},
				new Job
				{
					Id = 8,
					Title = "Finance Manager",
					Description = "Manages budgeting, forecasting, and financial reporting.",
					DepartmentId = 4,
					Level = JobLevel.Senior,
					BaseSalary = 6000
				}
			};
		}
	}
}
