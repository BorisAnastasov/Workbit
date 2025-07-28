using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Database.Entities;

namespace Workbit.Infrastructure.Database.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasOne(p => p.Recipient)
                   .WithMany(e => e.Payments)
                   .HasForeignKey(p => p.RecipientId);

            builder.HasData(SeedPayments());
        }

        private static List<Payment> SeedPayments()
        {
            return new List<Payment>
            {
                new Payment
                {
                    Id = 1,
                    RecipientId = Guid.Parse("f92e7b0f-5123-40c8-9d28-8834a3c93005"), // Alice Watson
                    PaymentDate = new DateTime(2025, 7, 1),
                    Salary = 2800,
                    Bonus = 200,
                    Taxes = 100,
                    Notes = "Full salary + punctuality bonus"
                },
                new Payment
                {
                    Id = 2,
					RecipientId = Guid.Parse("2b06417a-1460-4b10-8454-51069dfb2d06"), // Bob Thomas
                    PaymentDate = new DateTime(2025, 7, 1),
                    Salary = 5000,   // Reduced from 5200
                    Bonus = 100,     // Lower bonus due to lateness
                    Taxes = 240,
                    Notes = "Late arrivals detected, adjusted salary and reduced bonus"
                },
                new Payment
                {
                    Id = 3,
					RecipientId = Guid.Parse("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"), // Claire James
                    PaymentDate = new DateTime(2025, 7, 1),
                    Salary = 4700,
                    Bonus = 100,
                    Taxes = 190,
                    Notes = "One absence day deducted"
                },
                new Payment
                {
                    Id = 4,
					RecipientId = Guid.Parse("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08"), // Dave Walker
                    PaymentDate = new DateTime(2025, 7, 1),
                    Salary = 6900,
                    Bonus = 400,
                    Taxes = 280,
                    Notes = "Early leaves adjusted, bonus for extra work on other days"
                },
                new Payment
                {
                    Id = 5,
					RecipientId = Guid.Parse("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09"), // Emily Young
                    PaymentDate = new DateTime(2025, 7, 1),
                    Salary = 4200,
                    Bonus = 100,
                    Taxes = 150,
                    Notes = "Standard payout"
                },
                new Payment
                {
                    Id = 6,
					RecipientId = Guid.Parse("a73cb7de-df18-4b6e-a573-0dcf1f703e10"), // Frank Scott
                    PaymentDate = new DateTime(2025, 7, 1),
                    Salary = 6500,
                    Bonus = 400,
                    Taxes = 250,
                    Notes = "Exceeded marketing targets"
                },
                new Payment
                {
                    Id = 7,
					RecipientId = Guid.Parse("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11"), // Grace Adams
                    PaymentDate = new DateTime(2025, 7, 1),
                    Salary = 3000,
                    Bonus = 0,
                    Taxes = 120,
                    Notes = "One day unpaid leave adjustment"
                },
                new Payment
                {
                    Id = 8,
					RecipientId = Guid.Parse("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012"), // Harry Brooks
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
