using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Database.Entities;
using Workbit.Infrastructure.Enumerations;

namespace Workbit.Infrastructure.Database.Configuration
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<AttendanceEntry>
    {
        public void Configure(EntityTypeBuilder<AttendanceEntry> builder)
        {
            builder.HasData(SeedAttendanceEntries());
        }

        private static List<AttendanceEntry> SeedAttendanceEntries()
        {
            var entries = new List<AttendanceEntry>();

            // Updated GUIDs to match ApplicationUserConfiguration
            var employees = new[]
            {
                Guid.Parse("f92e7b0f-5123-40c8-9d28-8834a3c93005"), // Alice Watson
                Guid.Parse("2b06417a-1460-4b10-8454-51069dfb2d06"), // Bob Thomas
                Guid.Parse("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"), // Claire James
                Guid.Parse("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08")  // Dave Walker
            };

            var currentId = 1;
            var startDate = new DateTime(2025, 7, 14); // Monday

            // Generate entries for Mon–Fri
            for (int day = 0; day < 5; day++)
            {
                var date = startDate.AddDays(day);

                foreach (var employee in employees)
                {
                    // Skip Claire on Wednesday (absent)
                    if (employee == employees[2] && day == 2) continue;

                    // Check-in and check-out times
                    var checkIn = date.AddHours(employee == employees[1] ? 9.75 : 9);  // Bob is late often
                    var checkOut = date.AddHours(employee == employees[0] ? 17 : 16.75); // Alice stays full shift, others slightly early

                    entries.Add(new AttendanceEntry
                    {
                        Id = currentId++,
                        UserId = employee,
                        Timestamp = checkIn,
                        Type = EntryType.CheckIn
                    });

                    entries.Add(new AttendanceEntry
                    {
                        Id = currentId++,
                        UserId = employee,
                        Timestamp = checkOut,
                        Type = EntryType.CheckOut
                    });
                }
            }

            return entries;
        }
    }
}
