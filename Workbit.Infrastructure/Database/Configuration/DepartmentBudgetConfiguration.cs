using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Database.Entities;

namespace Workbit.Infrastructure.Database.Configuration
{
	public class DepartmentBudgetConfiguration : IEntityTypeConfiguration<DepartmentBudget>
	{
		public void Configure(EntityTypeBuilder<DepartmentBudget> builder)
		{
            builder.HasData(
				new DepartmentBudget
				{
					Id = 1,
					DepartmentId = 1, // HR
					TotalBudget = 50000m,
					BonusPool = 5000m,
					DateAllocated = new DateTime(2025, 7, 1),
					IsDistributed = false
				},
				new DepartmentBudget
				{
					Id = 2,
					DepartmentId = 2, // IT
					TotalBudget = 80000m,
					BonusPool = 10000m,
					DateAllocated = new DateTime(2025, 7, 1),
					IsDistributed = false
				},
				new DepartmentBudget
				{
					Id = 3,
					DepartmentId = 3, // Finance
					TotalBudget = 60000m,
					BonusPool = 7000m,
					DateAllocated = new DateTime(2025, 7, 1),
					IsDistributed = false
				}
			);
		}
	}
}
