using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Domain.Entities;
using Workbit.Domain.Enumerations;

namespace Workbit.Infrastructure.Persistance.Configuration
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<AttendanceEntry>
    {
        public void Configure(EntityTypeBuilder<AttendanceEntry> builder)
        {
            builder.HasData(SeedAttendanceEntries());
        }

        private static List<AttendanceEntry> SeedAttendanceEntries()
        {
            var employees = new[]
            {
                Guid.Parse("f92e7b0f-5123-40c8-9d28-8834a3c93005"),
                Guid.Parse("2b06417a-1460-4b10-8454-51069dfb2d06"),
                Guid.Parse("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"),
                Guid.Parse("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08"),
                Guid.Parse("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09"),
                Guid.Parse("a73cb7de-df18-4b6e-a573-0dcf1f703e10"),
                Guid.Parse("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11"),
                Guid.Parse("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012"),
                Guid.Parse("e1a2b3c4-1111-4a1a-8b2b-111111111101"),
                Guid.Parse("e1a2b3c4-1111-4a1a-8b2b-111111111102"),
                Guid.Parse("e1a2b3c4-1111-4a1a-8b2b-111111111103"),
                Guid.Parse("e1a2b3c4-1111-4a1a-8b2b-111111111104")
            };

            var random = new Random(42);
            var entries = new List<AttendanceEntry>();
            var currentId = 1;
            var startDate = new DateTime(2025, 6, 2);

            for (int week = 0; week < 8; week++)
            {
                for (int day = 0; day < 5; day++)
                {
                    var date = startDate.AddDays(week * 7 + day);

                    foreach (var employee in employees)
                    {
                        if (random.NextDouble() < 0.08) continue;

                        var checkInMinutes = random.Next(-15, 30);
                        var checkIn = date.AddHours(9).AddMinutes(checkInMinutes);

                        var checkOutMinutes = random.Next(-30, 30);
                        var checkOut = date.AddHours(17).AddMinutes(checkOutMinutes);

                        entries.Add(new AttendanceEntry { Id = currentId++, UserId = employee, Timestamp = checkIn, Type = EntryType.CheckIn });
                        entries.Add(new AttendanceEntry { Id = currentId++, UserId = employee, Timestamp = checkOut, Type = EntryType.CheckOut });
                    }
                }
            }

            return entries;
        }
    }
}