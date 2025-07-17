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

			builder.HasData(SeedDepartments());
		}

		private static List<Department> SeedDepartments()
		{
			return new List<Department>
			{
				new Department
				{
					Id = 1,
					Name = "Human Resources",
					Description = "Handles recruitment, training, and compliance."
				},
				new Department
				{
					Id = 2,
					Name = "Engineering",
					Description = "Builds and maintains software systems."
				},
				new Department
				{
					Id = 3,
					Name = "Marketing",
					Description = "Drives growth through branding and campaigns."
				},
				new Department
				{
					Id = 4,
					Name = "Finance",
					Description = "Manages budgeting, payroll, and expenses."
				}
			};
		}
	}
}
