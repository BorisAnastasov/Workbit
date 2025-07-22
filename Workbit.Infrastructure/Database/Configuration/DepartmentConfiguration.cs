using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Database.Entities;

namespace Workbit.Infrastructure.Database.Configuration
{
	public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			builder.HasMany(d => d.DepartmentManagers)
			   .WithOne(dm => dm.Department)
			   .HasForeignKey(dm => dm.DepartmentId);

			builder.HasData(
				new Department { Id = 1, Name = "Human Resources", Description = "Handles recruitment, payroll, and employee relations." },
				new Department { Id = 2, Name = "IT", Description = "Manages infrastructure, development, and support." },
				new Department { Id = 3, Name = "Finance", Description = "Responsible for budgeting, forecasting, and reporting." },
				new Department { Id = 4, Name = "Marketing", Description = "Handles brand strategy and campaigns." }
			);
		}
	}
}
