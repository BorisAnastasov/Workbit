using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Database.Entities;

namespace Workbit.Infrastructure.Database.Configuration
{
	public class SalaryPaymentConfiguration : IEntityTypeConfiguration<SalaryPayment>
	{
		public void Configure(EntityTypeBuilder<SalaryPayment> builder)
		{
			builder.HasOne(sp => sp.Employee)
			   .WithMany(e => e.SalaryPayments)
			   .HasForeignKey(sp => sp.EmployeeId);

			builder.HasData(SeedSalaryPayments());
		}

		private static List<SalaryPayment> SeedSalaryPayments()
		{
			return new List<SalaryPayment>
			{
				new SalaryPayment
				{
					Id = 1,
					EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000001"), // Alice Watson
                    DateOfPayment = new DateTime(2025, 7, 1),
					Payment = 2800,
					Bonus = 200,
					Deduction = 100,
					Notes = "Monthly salary + punctuality bonus"
				},
				new SalaryPayment
				{
					Id = 2,
					EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000002"), // Bob Thomas
                    DateOfPayment = new DateTime(2025, 7, 1),
					Payment = 5200,
					Bonus = 300,
					Deduction = 0,
					Notes = "Excellent performance"
				},
				new SalaryPayment
				{
					Id = 3,
					EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000003"), // Claire James
                    DateOfPayment = new DateTime(2025, 7, 1),
					Payment = 4800,
					Bonus = 150,
					Deduction = 50,
					Notes = null
				},
				new SalaryPayment
				{
					Id = 4,
					EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000004"), // Dave Walker
                    DateOfPayment = new DateTime(2025, 7, 1),
					Payment = 7000,
					Bonus = 500,
					Deduction = 200,
					Notes = "Late project delivery"
				},
				new SalaryPayment
				{
					Id = 5,
					EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000005"), // Emily Young
                    DateOfPayment = new DateTime(2025, 7, 1),
					Payment = 4200,
					Bonus = 100,
					Deduction = 0,
					Notes = null
				},
				new SalaryPayment
				{
					Id = 6,
					EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000006"), // Frank Scott
                    DateOfPayment = new DateTime(2025, 7, 1),
					Payment = 6500,
					Bonus = 400,
					Deduction = 150,
					Notes = "Exceeded marketing targets"
				},
				new SalaryPayment
				{
					Id = 7,
					EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000007"), // Grace Adams
                    DateOfPayment = new DateTime(2025, 7, 1),
					Payment = 3000,
					Bonus = 0,
					Deduction = 50,
					Notes = "One day unpaid leave"
				},
				new SalaryPayment
				{
					Id = 8,
					EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000008"), // Harry Brooks
                    DateOfPayment = new DateTime(2025, 7, 1),
					Payment = 6000,
					Bonus = 350,
					Deduction = 0,
					Notes = "On-time delivery and initiative"
				}
			};
		}
	}
}
