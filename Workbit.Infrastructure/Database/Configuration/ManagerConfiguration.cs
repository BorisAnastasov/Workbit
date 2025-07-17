using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Configuration
{
	public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
	{
		public void Configure(EntityTypeBuilder<Manager> builder)
		{
			builder.HasKey(m => m.ApplicationUserId);

			builder.HasOne(m => m.ApplicationUser)
				   .WithOne()
				   .HasForeignKey<Manager>(m => m.ApplicationUserId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasData(SeedManagers());
		}

		private static List<Manager> SeedManagers()
		{
			return new List<Manager>
			{
				new Manager
				{
					ApplicationUserId = Guid.Parse("a1111111-0000-0000-0000-000000000001"),
					OfficePhone = "555-111-1001"
				},
				new Manager
				{
					ApplicationUserId = Guid.Parse("a1111111-0000-0000-0000-000000000002"),
					OfficePhone = "555-111-1002"
				},
				new Manager
				{
					ApplicationUserId = Guid.Parse("a1111111-0000-0000-0000-000000000003"),
					OfficePhone = "555-111-1003"
				},
				new Manager
				{
					ApplicationUserId = Guid.Parse("a1111111-0000-0000-0000-000000000004"),
					OfficePhone = "555-111-1004"
				}
			};
		}
	}
}
