using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Database.Entities;

namespace Workbit.Infrastructure.Database.Configuration
{
	public class DepartmentManagerConfiguration : IEntityTypeConfiguration<DepartmentManager>
	{
		public void Configure(EntityTypeBuilder<DepartmentManager> builder)
		{
			builder.HasKey(dm => new { dm.ManagerId, dm.DepartmentId });

			builder.HasOne(dm => dm.Manager)
				   .WithMany(m => m.DepartmentManagers)
				   .HasForeignKey(dm => dm.ManagerId);

			builder.HasOne(dm => dm.Department)
				   .WithMany(d => d.DepartmentManagers)
				   .HasForeignKey(dm => dm.DepartmentId);

			builder.HasData(
				new DepartmentManager { ManagerId = Guid.Parse("a1111111-0000-0000-0000-000000000002"), DepartmentId = 1 }, // Lisa - HR
				new DepartmentManager { ManagerId = Guid.Parse("a1111111-0000-0000-0000-000000000003"), DepartmentId = 2 }, // Carl - IT
				new DepartmentManager { ManagerId = Guid.Parse("a1111111-0000-0000-0000-000000000004"), DepartmentId = 3 }  // Nina - Finance
			);
		}

		private static List<DepartmentManager> SeedDepartmentManagers()
		{
			return new List<DepartmentManager>
			{
                // John Lewis manages HR & Engineering
                new DepartmentManager
				{
					ManagerId = Guid.Parse("a1111111-0000-0000-0000-000000000001"),
					DepartmentId = 1
				},
				new DepartmentManager
				{
					ManagerId = Guid.Parse("a1111111-0000-0000-0000-000000000001"),
					DepartmentId = 2
				},

                // Lisa Anderson manages Marketing
                new DepartmentManager
				{
					ManagerId = Guid.Parse("a1111111-0000-0000-0000-000000000002"),
					DepartmentId = 3
				},

                // Carl Morgan manages Finance & Engineering
                new DepartmentManager
				{
					ManagerId = Guid.Parse("a1111111-0000-0000-0000-000000000003"),
					DepartmentId = 2
				},
				new DepartmentManager
				{
					ManagerId = Guid.Parse("a1111111-0000-0000-0000-000000000003"),
					DepartmentId = 4
				},

                // Nina Hughes manages HR
                new DepartmentManager
				{
					ManagerId = Guid.Parse("a1111111-0000-0000-0000-000000000004"),
					DepartmentId = 1
				}
			};
		}
	}
}
