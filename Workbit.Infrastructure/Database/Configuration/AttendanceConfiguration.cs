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
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.User)
                   .WithMany(u => u.AttendanceEntries)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(SeedAttendanceEntries());
        }

        private static List<AttendanceEntry> SeedAttendanceEntries()
        {
            var entries = new List<AttendanceEntry>();
            var employees = new[]
            {
                Guid.Parse("b2222222-0000-0000-0000-000000000001"), // Alice Watson
                Guid.Parse("b2222222-0000-0000-0000-000000000002"), // Bob Thomas
                Guid.Parse("b2222222-0000-0000-0000-000000000003"), // Claire James
                Guid.Parse("b2222222-0000-0000-0000-000000000004")  // Dave Walker
            };

            var currentId = 1;
            var startDate = new DateTime(2025, 7, 14); // Monday

            // Generate entries for Mon–Fri
            for (int day = 0; day < 5; day++)
            {
                var date = startDate.AddDays(day);

                foreach (var employee in employees)
                {
                    // Randomly decide if someone is absent (e.g., Claire misses Wednesday)
                    if (employee == employees[2] && day == 2) continue;

                    // Check-in and Check-out times vary
                    var checkIn = date.AddHours(employee == employees[1] ? 9.75 : 9);  // Bob is often late
                    var checkOut = date.AddHours(employee == employees[0] ? 17 : 16.75); // Alice stays full shift, others leave a bit earlier sometimes

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
