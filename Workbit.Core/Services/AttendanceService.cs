using Workbit.Infrastructure.Database.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Attendance;
using Workbit.Infrastructure.Database.Entities;
using Workbit.Infrastructure.Database.Entities.Account;
using Workbit.Infrastructure.Enumerations;
using static Workbit.Common.DataConstants.Constants;
using System;

namespace Workbit.Core.Services
{
    public class AttendanceService:IAttendanceService
    {
        private readonly IRepository repository;

        private readonly UserManager<ApplicationUser> userManager;

        public AttendanceService(IRepository repo, UserManager<ApplicationUser> _userManager)
        {
            repository = repo;
            userManager = _userManager;
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

        public async Task<List<AttendanceEntryViewModel>> GetAttendanceLogsAsync(DateTime start, DateTime end, string role)
        {
            var logs = await repository.AllReadOnly<AttendanceEntry>()
                .Include(a => a.User)
                .Where(a => a.Timestamp >= start && a.Timestamp <= end)
                .OrderBy(a => a.Timestamp)
                .ToListAsync();

            if (!string.IsNullOrEmpty(role) && role != "All")
            {
                var filtered = new List<AttendanceEntry>();
                foreach (var log in logs)
                {
                    var roles = await userManager.GetRolesAsync(log.User);
                    if (roles.Contains(role))
                    {
                        filtered.Add(log);
                    }
                }
                logs = filtered;
            }

            return logs.Select(a => new AttendanceEntryViewModel
            {
                Id = a.Id,
                UserId = a.UserId.ToString(),
                UserName = $"{a.User.FirstName} {a.User.LastName}",
                Timestamp = a.Timestamp.ToString("yyyy-MM-dd HH:mm"),
                Type = a.Type.ToString()
            }).ToList();
        }

        public async Task<IEnumerable<AttendanceEntryViewModel>> GetByDateAsync(DateTime date, string ceoId)
        {
            var dayStart = date.Date;
            var dayEnd = dayStart.AddDays(1);

            return await repository.AllReadOnly<AttendanceEntry>()
                .Where(a => a.User.Employee!.Job!.Department.Company.CeoId == Guid.Parse(ceoId) 
                            && a.Timestamp >= dayStart 
                            && a.Timestamp < dayEnd)
                .Select(a => new AttendanceEntryViewModel
                {
                    Id = a.Id,
                    UserId = a.UserId.ToString(),
                    UserName = a.User.FullName,
                    Timestamp = a.Timestamp.ToString(DateFormatLong),
                    Type = a.Type.ToString()
                })
                .ToListAsync();
        }

        public async Task<AttendanceEntryViewModel> GetByIdAsync(int id)
        {
            var entry = await repository.GetByIdAsync<AttendanceEntry>(id);

            return new AttendanceEntryViewModel
            {
                Id = entry.Id,
                UserId = entry.UserId.ToString(),
                UserName = entry.User.FullName,
                Timestamp = entry.Timestamp.ToString(DateFormatLong),
                Type = entry.Type.ToString()
            };
        }

        public async Task<IEnumerable<AttendanceEntryViewModel>> GetByUserIdAsync(string userId)
        {
            var user = await repository.GetByIdAsync<ApplicationUser>(Guid.Parse(userId));
            return user.AttendanceEntries
                .OrderByDescending(a => a.Timestamp)
                .Select(a => new AttendanceEntryViewModel
                {
                    Id = a.Id,
                    UserId = a.UserId.ToString(),
                    UserName = a.User.FullName,
                    Timestamp = a.Timestamp.ToString(DateFormatLong),
                    Type = a.Type.ToString()
                }).ToList();
        }

        public async Task<List<DailyAttendanceSummaryModel>> GetDailySummaryAsync(
    DateTime startDate, DateTime endDate, string? roleFilter = null)
        {
            // Fetch all entries within the date range
            var entries = await repository.AllReadOnly<AttendanceEntry>()
                .Include(a => a.User)
                .Where(a => a.Timestamp.Date >= startDate.Date && a.Timestamp.Date <= endDate.Date)
                .ToListAsync();

            // Filter by role if provided
            if (!string.IsNullOrEmpty(roleFilter) && roleFilter != "All")
            {
                var filteredEntries = new List<AttendanceEntry>();
                foreach (var entry in entries)
                {
                    var roles = await userManager.GetRolesAsync(entry.User);
                    if (roles.Contains(roleFilter))
                    {
                        filteredEntries.Add(entry);
                    }
                }
                entries = filteredEntries;
            }

            // Group by User + Date (so each day gets its own summary per user)
            var grouped = entries
                .GroupBy(e => new { e.User, e.Timestamp.Date })
                .Select(g =>
                {
                    var date = g.Key.Date;
                    var checkIn = g
                        .Where(x => x.Type == EntryType.CheckIn)
                        .OrderBy(x => x.Timestamp)
                        .FirstOrDefault();

                    var checkOut = g
                        .Where(x => x.Type == EntryType.CheckOut)
                        .OrderByDescending(x => x.Timestamp)
                        .FirstOrDefault();

                    double hoursWorked = 0;
                    if (checkIn != null && checkOut != null)
                    {
                        hoursWorked = (checkOut.Timestamp - checkIn.Timestamp).TotalHours;
                    }

                    string status;
                    if (checkIn == null)
                        status = "Absent";
                    else if (checkIn.Timestamp.TimeOfDay > TimeSpan.FromHours(9)) // Example: Late after 9 AM
                        status = "Late";
                    else
                        status = "Present";

                    return new DailyAttendanceSummaryModel
                    {
                        Date = date,
                        FirstCheckIn = checkIn?.Timestamp.TimeOfDay,
                        LastCheckOut = checkOut?.Timestamp.TimeOfDay,
                        HoursWorked = Math.Round(hoursWorked, 2),
                        Status = status
                    };
                })
                .OrderBy(x => x.Date) // Sort by date
                .ToList();

            return grouped;
        }


        public async Task<IEnumerable<DailyAttendanceSummaryModel>> GetMonthlySummaryAsync(string userId, int year, int month)
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

                    return new DailyAttendanceSummaryModel
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

        public async Task CheckInAsync(string userId)
        {
            var entry = new AttendanceEntry
            {
                UserId = Guid.Parse(userId),
                Timestamp = DateTime.UtcNow,
                Type = EntryType.CheckIn
            };

            await repository.AddAsync(entry);
            await repository.SaveChangesAsync();
        }

        public async Task CheckOutAsync(string userId)
        {
            var entry = new AttendanceEntry
            {
                UserId = Guid.Parse(userId),
                Timestamp = DateTime.UtcNow,
                Type = EntryType.CheckOut
            };

            await repository.AddAsync(entry);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> IsCheckedInAsync(string userId)
        {
            var guid = Guid.Parse(userId);
            var today = DateTime.UtcNow.Date;

            var checkIn = await repository.All<AttendanceEntry>()
                .AnyAsync(a => a.UserId == guid && a.Timestamp.Date == today && a.Type == EntryType.CheckIn);

            return checkIn;
        }

		public async Task<bool> IsCheckedOutAsync(string userId)
		{
			var guid = Guid.Parse(userId);
			var today = DateTime.UtcNow.Date;

			var checkOut = await repository.All<AttendanceEntry>()
				.AnyAsync(a => a.UserId == guid && a.Timestamp.Date == today && a.Type == EntryType.CheckOut);

            return checkOut;
		}
	}
}
