using LearnSpace.Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Attendance;
using Workbit.Infrastructure.Database.Entities;
using Workbit.Infrastructure.Database.Entities.Account;
using Workbit.Infrastructure.Enumerations;
using static Workbit.Common.DataConstants.Constants;

namespace Workbit.Core.Services
{
    public class AttendanceService:IAttendanceService
    {
        private readonly IRepository repository;

        public AttendanceService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<int> CountAbsencesAsync(string userId, int year, int month)
        {
            var summaries = await GetMonthlySummaryAsync(userId, year, month);
            return summaries.Count(s => s.Status == "Absent");
        }

        public async Task CreateAsync(AttendanceEntryCreateDto dto)
        {
            var entry = new AttendanceEntry
            {
                UserId = Guid.Parse(dto.UserId),
                Timestamp = dto.Timestamp,
                Type = Enum.Parse<EntryType>(dto.Type, true)
            };

            await repository.AddAsync(entry);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await repository.DeleteAsync<AttendanceEntry>(id);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<AttendanceEntryReadDto>> GetByDateAsync(DateTime date)
        {
            var dayStart = date.Date;
            var dayEnd = dayStart.AddDays(1);

            return await repository.AllReadOnly<AttendanceEntry>()
                .Where(a => a.Timestamp >= dayStart && a.Timestamp < dayEnd)
                .Select(a => new AttendanceEntryReadDto
                {
                    Id = a.Id,
                    UserId = a.UserId.ToString(),
                    UserName = a.User.FullName,
                    Timestamp = a.Timestamp.ToString(DateFormat),
                    Type = a.Type.ToString()
                })
                .ToListAsync();
        }

        public async Task<AttendanceEntryReadDto> GetByIdAsync(int id)
        {
            var entry = await repository.GetByIdAsync<AttendanceEntry>(id);

            return new AttendanceEntryReadDto
            {
                Id = entry.Id,
                UserId = entry.UserId.ToString(),
                UserName = entry.User.FullName,
                Timestamp = entry.Timestamp.ToString(DateFormat),
                Type = entry.Type.ToString()
            };
        }

        public async Task<IEnumerable<AttendanceEntryReadDto>> GetByUserIdAsync(string userId)
        {
            var user = await repository.GetByIdAsync<ApplicationUser>(Guid.Parse(userId));
            return user.AttendanceEntries
                .OrderByDescending(a => a.Timestamp)
                .Select(a => new AttendanceEntryReadDto
                {
                    Id = a.Id,
                    UserId = a.UserId.ToString(),
                    UserName = a.User.FullName,
                    Timestamp = a.Timestamp.ToString(DateFormat),
                    Type = a.Type.ToString()
                }).ToList();
        }

        public async Task<IEnumerable<DailyAttendanceSummaryDto>> GetMonthlySummaryAsync(string userId, int year, int month)
        {
            var guid = Guid.Parse(userId);
            var start = new DateTime(year, month, 1);
            var end = start.AddMonths(1);

            var entries = await repository.AllReadOnly<AttendanceEntry>()
                .Where(a => a.UserId == guid && a.Timestamp >= start && a.Timestamp < end)
                .OrderBy(a => a.Timestamp)
                .ToListAsync();

            var grouped = entries
                .GroupBy(e => e.Timestamp.Date)
                .Select(g =>
                {
                    var dailyEntries = g.OrderBy(e => e.Timestamp).ToList();

                    double totalHours = 0;
                    DateTime? firstCheckIn = null;
                    DateTime? currentCheckIn = null;

                    foreach (var entry in dailyEntries)
                    {
                        if (entry.Type == EntryType.CheckIn)
                        {
                            currentCheckIn = entry.Timestamp;
                            if (firstCheckIn == null) firstCheckIn = currentCheckIn; // Track first check-in
                        }
                        else if (entry.Type == EntryType.CheckOut && currentCheckIn.HasValue)
                        {
                            // Pair this checkout with the last check-in
                            totalHours += (entry.Timestamp - currentCheckIn.Value).TotalHours;
                            currentCheckIn = null; // Reset for the next pair
                        }
                    }

                    // Handle unpaired check-in (assume they left at 17:00, or ignore if you want stricter)
                    if (currentCheckIn.HasValue)
                    {
                        var assumedCheckout = currentCheckIn.Value.Date.AddHours(17); // 5 PM
                        totalHours += (assumedCheckout - currentCheckIn.Value).TotalHours;
                    }

                    var lastCheckOut = dailyEntries.LastOrDefault(e => e.Type == EntryType.CheckOut)?.Timestamp.TimeOfDay;

                    // Determine daily status
                    string status = "Absent";
                    if (firstCheckIn.HasValue)
                    {
                        status = firstCheckIn.Value.TimeOfDay > new TimeSpan(9, 0, 0) ? "Late" : "Present";
                    }

                    return new DailyAttendanceSummaryDto
                    {
                        Date = g.Key,
                        FirstCheckIn = firstCheckIn?.TimeOfDay,
                        LastCheckOut = lastCheckOut,
                        HoursWorked = Math.Round(totalHours, 2),
                        Status = status
                    };
                })
                .OrderBy(s => s.Date)
                .ToList();

            return grouped;
        }
    }
}
