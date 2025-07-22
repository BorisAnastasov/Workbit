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
			   .HasForeignKey(j => j.DepartmentId);

			builder.HasData(
				new Job { Id = 1, Title = "Software Engineer", Description = "Develops and maintains applications.", DepartmentId = 2, BaseSalary = 70000 },
				new Job { Id = 2, Title = "HR Specialist", Description = "Manages hiring and employee support.", DepartmentId = 1, BaseSalary = 50000 },
				new Job { Id = 3, Title = "Financial Analyst", Description = "Prepares financial reports and analysis.", DepartmentId = 3, BaseSalary = 60000 },
				new Job { Id = 4, Title = "Marketing Coordinator", Description = "Supports campaigns and communications.", DepartmentId = 4, BaseSalary = 45000 }
			);
		}
	}
}
