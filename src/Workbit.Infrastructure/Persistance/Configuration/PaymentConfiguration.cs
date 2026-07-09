using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Domain.Entities;

namespace Workbit.Infrastructure.Persistance.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasData(SeedPayments());
        }

        private static List<Payment> SeedPayments()
        {
            var employees = new (Guid Id, decimal BaseSalary, string Note)[]
            {
                (Guid.Parse("f92e7b0f-5123-40c8-9d28-8834a3c93005"), 2800m, "Full salary + punctuality bonus"),
                (Guid.Parse("2b06417a-1460-4b10-8454-51069dfb2d06"), 5000m, "Late arrivals detected, adjusted salary"),
                (Guid.Parse("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"), 4700m, "One absence day deducted"),
                (Guid.Parse("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08"), 6900m, "Early leaves adjusted, bonus for extra work"),
                (Guid.Parse("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09"), 4200m, "Standard payout"),
                (Guid.Parse("a73cb7de-df18-4b6e-a573-0dcf1f703e10"), 6500m, "Exceeded marketing targets"),
                (Guid.Parse("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11"), 3000m, "One day unpaid leave adjustment"),
                (Guid.Parse("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012"), 6000m, "On-time delivery and initiative bonus"),
                (Guid.Parse("e1a2b3c4-1111-4a1a-8b2b-111111111101"), 4800m, "Strong sales pipeline growth"),
                (Guid.Parse("e1a2b3c4-1111-4a1a-8b2b-111111111102"), 6200m, "Senior engineering deliverables on track"),
                (Guid.Parse("e1a2b3c4-1111-4a1a-8b2b-111111111103"), 4500m, "New hire ramp-up period"),
                (Guid.Parse("e1a2b3c4-1111-4a1a-8b2b-111111111104"), 5100m, "Standard payout")
            };

            var random = new Random(42);
            var payments = new List<Payment>();
            var currentId = 1;

            foreach (var month in new[] { 6, 7 })
            {
                foreach (var (id, baseSalary, note) in employees)
                {
                    var bonus = Math.Round(baseSalary * (decimal)(random.NextDouble() * 0.08), 2);
                    var taxes = Math.Round((baseSalary + bonus) * 0.2m, 2);

                    payments.Add(new Payment
                    {
                        Id = currentId++,
                        RecipientId = id,
                        PaymentDate = new DateTime(2025, month, 1),
                        Salary = baseSalary,
                        Bonus = bonus,
                        Taxes = taxes,
                        Notes = note
                    });
                }
            }

            return payments;
        }
    }
}