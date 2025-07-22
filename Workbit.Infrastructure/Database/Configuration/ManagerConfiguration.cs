using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Configuration
{
	public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
	{
		public void Configure(EntityTypeBuilder<Manager> builder)
		{
			builder.HasData(
				new Manager
				{
					ApplicationUserId = Guid.Parse("a1111111-0000-0000-0000-000000000001"), // John Lewis (CEO)
					OfficePhone = "+1-555-1010",
					IsCeo = true
				},
				new Manager
				{
					ApplicationUserId = Guid.Parse("a1111111-0000-0000-0000-000000000002"), // Lisa Anderson
					OfficePhone = "+1-555-2020",
					IsCeo = false
				},
				new Manager
				{
					ApplicationUserId = Guid.Parse("a1111111-0000-0000-0000-000000000003"), // Carl Morgan
					OfficePhone = "+1-555-3030",
					IsCeo = false
				},
				new Manager
				{
					ApplicationUserId = Guid.Parse("a1111111-0000-0000-0000-000000000004"), // Nina Hughes
					OfficePhone = "+1-555-4040",
					IsCeo = false
				}
			);
		}
	}
}
