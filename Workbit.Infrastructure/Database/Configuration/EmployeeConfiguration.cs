using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

			builder.HasData(SeedEmployees());
		}

		private static List<Employee> SeedEmployees()
		{
			return new List<Employee>
			{
				new Employee
				{
					ApplicationUserId = Guid.Parse("b2222222-0000-0000-0000-000000000001"), // Alice Watson
                    JobId = 1 // HR Assistant
                },
				new Employee
				{
					ApplicationUserId = Guid.Parse("b2222222-0000-0000-0000-000000000002"), // Bob Thomas
                    JobId = 2 // HR Manager
                },
				new Employee
				{
					ApplicationUserId = Guid.Parse("b2222222-0000-0000-0000-000000000003"), // Claire James
                    JobId = 3 // Software Engineer
                },
				new Employee
				{
					ApplicationUserId = Guid.Parse("b2222222-0000-0000-0000-000000000004"), // Dave Walker
                    JobId = 4 // Tech Lead
                },
				new Employee
				{
					ApplicationUserId = Guid.Parse("b2222222-0000-0000-0000-000000000005"), // Emily Young
                    JobId = 5 // Content Strategist
                },
				new Employee
				{
					ApplicationUserId = Guid.Parse("b2222222-0000-0000-0000-000000000006"), // Frank Scott
                    JobId = 6 // Marketing Director
                },
				new Employee
				{
					ApplicationUserId = Guid.Parse("b2222222-0000-0000-0000-000000000007"), // Grace Adams
                    JobId = 7 // Junior Accountant
                },
				new Employee
				{
					ApplicationUserId = Guid.Parse("b2222222-0000-0000-0000-000000000008"), // Harry Brooks
                    JobId = 8 // Finance Manager
                }
			};
		}
	}
}
