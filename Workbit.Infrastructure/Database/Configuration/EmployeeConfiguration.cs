using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Core.Enumerations;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Configuration
{
	public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.HasOne(e => e.ApplicationUser)
		   .WithOne()
		   .HasForeignKey<Employee>(e => e.ApplicationUserId);

			builder.HasOne(e => e.Job)
				   .WithMany(j => j.Employees)
				   .HasForeignKey(e => e.JobId);

			builder.HasMany(e => e.SalaryPayments)
				   .WithOne(sp => sp.Employee)
				   .HasForeignKey(sp => sp.EmployeeId);

			builder.HasMany(e => e.Attendances)
				   .WithOne(a => a.Employee)
				   .HasForeignKey(a => a.EmployeeId);

			builder.HasData(
				new Employee { ApplicationUserId = Guid.Parse("b2222222-0000-0000-0000-000000000001"), JobId = 1, Level = JobLevel.Mid },
				new Employee { ApplicationUserId = Guid.Parse("b2222222-0000-0000-0000-000000000002"), JobId = 1, Level = JobLevel.Junior },
				new Employee { ApplicationUserId = Guid.Parse("b2222222-0000-0000-0000-000000000003"), JobId = 2, Level = JobLevel.Lead },
				new Employee { ApplicationUserId = Guid.Parse("b2222222-0000-0000-0000-000000000004"), JobId = 2, Level = JobLevel.Senior },
				new Employee { ApplicationUserId = Guid.Parse("b2222222-0000-0000-0000-000000000005"), JobId = 3, Level = JobLevel.Mid },
				new Employee { ApplicationUserId = Guid.Parse("b2222222-0000-0000-0000-000000000006"), JobId = 3, Level = JobLevel.Junior },
				new Employee { ApplicationUserId = Guid.Parse("b2222222-0000-0000-0000-000000000007"), JobId = 4, Level = JobLevel.Senior },
				new Employee { ApplicationUserId = Guid.Parse("b2222222-0000-0000-0000-000000000008"), JobId = 4, Level = JobLevel.Lead }
			);
		}
	}
}
