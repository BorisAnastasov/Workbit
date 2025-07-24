using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Database.Entities;

namespace Workbit.Infrastructure.Database.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasOne(p => p.Employee)
                   .WithMany(e => e.SalaryPayments)
                   .HasForeignKey(p => p.EmployeeId);

            builder.HasData(SeedPayments());
        }

        private static List<Payment> SeedPayments()
        {
            return new List<Payment>
            {
                new Payment
                {
                    Id = 1,
                    EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000001"), // Alice Watson (always punctual)
                    PaymentDate = new DateTime(2025, 7, 1),
                    Salary = 2800,
                    Bonus = 200,
                    Taxes = 100,
                    Notes = "Full salary + punctuality bonus"
                },
                new Payment
                {
                    Id = 2,
                    EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000002"), // Bob Thomas (late)
                    PaymentDate = new DateTime(2025, 7, 1),
                    Salary = 5000,   // Reduced from 5200
                    Bonus = 100,     // Lower bonus due to lateness
                    Taxes = 240,
                    Notes = "Late arrivals detected, adjusted salary and reduced bonus"
                },
                new Payment
                {
                    Id = 3,
                    EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000003"), // Claire James (absent one day)
                    PaymentDate = new DateTime(2025, 7, 1),
                    Salary = 4700,
                    Bonus = 100,
                    Taxes = 190,
                    Notes = "One absence day deducted"
                },
                new Payment
                {
                    Id = 4,
                    EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000004"), // Dave Walker (early leave some days)
                    PaymentDate = new DateTime(2025, 7, 1),
                    Salary = 6900,
                    Bonus = 400,
                    Taxes = 280,
                    Notes = "Early leaves adjusted, bonus for extra work on other days"
                },
                new Payment
                {
                    Id = 5,
                    EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000005"), // Emily Young
                    PaymentDate = new DateTime(2025, 7, 1),
                    Salary = 4200,
                    Bonus = 100,
                    Taxes = 150,
                    Notes = "Standard payout"
                },
                new Payment
                {
                    Id = 6,
                    EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000006"), // Frank Scott
                    PaymentDate = new DateTime(2025, 7, 1),
                    Salary = 6500,
                    Bonus = 400,
                    Taxes = 250,
                    Notes = "Exceeded marketing targets"
                },
                new Payment
                {
                    Id = 7,
                    EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000007"), // Grace Adams
                    PaymentDate = new DateTime(2025, 7, 1),
                    Salary = 3000,
                    Bonus = 0,
                    Taxes = 120,
                    Notes = "One day unpaid leave adjustment"
                },
                new Payment
                {
                    Id = 8,
                    EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000008"), // Harry Brooks
                    PaymentDate = new DateTime(2025, 7, 1),
                    Salary = 6000,
                    Bonus = 350,
                    Taxes = 200,
                    Notes = "On-time delivery and initiative bonus"
                }
            };
        }
    }
}
