using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Core.Enumerations;
using Workbit.Infrastructure.Database.Entities;

namespace Workbit.Infrastructure.Database.Configuration
{
	public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
	{
		public void Configure(EntityTypeBuilder<Attendance> builder)
		{
			builder.HasKey(a => a.Id);

			builder.HasOne(a => a.Employee)
				   .WithMany(e => e.Attendances)
				   .HasForeignKey(a => a.EmployeeId)
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasData(SeedAttendance());
		}

		private static List<Attendance> SeedAttendance()
		{
			return new List<Attendance>
			{
				new Attendance
				{
					Id = 1,
					EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000001"),
					Date = new DateTime(2025, 7, 16),
					CheckIn = new TimeSpan(9, 0, 0),
					CheckOut = new TimeSpan(17, 0, 0),
					Status = AttendanceStatus.Present
				},
				new Attendance
				{
					Id = 2,
					EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000002"),
					Date = new DateTime(2025, 7, 16),
					CheckIn = new TimeSpan(9, 30, 0),
					CheckOut = new TimeSpan(17, 0, 0),
					Status = AttendanceStatus.Late
				},
				new Attendance
				{
					Id = 3,
					EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000003"),
					Date = new DateTime(2025, 7, 16),
					CheckIn = null,
					CheckOut = null,
					Status = AttendanceStatus.Absent
				},
				new Attendance
				{
					Id = 4,
					EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000004"),
					Date = new DateTime(2025, 7, 16),
					CheckIn = new TimeSpan(8, 45, 0),
					CheckOut = new TimeSpan(16, 30, 0),
					Status = AttendanceStatus.Present
				},
				new Attendance
				{
					Id = 5,
					EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000005"),
					Date = new DateTime(2025, 7, 16),
					CheckIn = null,
					CheckOut = null,
					Status = AttendanceStatus.Absent
				},
				new Attendance
				{
					Id = 6,
					EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000006"),
					Date = new DateTime(2025, 7, 16),
					CheckIn = new TimeSpan(9, 15, 0),
					CheckOut = new TimeSpan(17, 15, 0),
					Status = AttendanceStatus.Late
				},
				new Attendance
				{
					Id = 7,
					EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000007"),
					Date = new DateTime(2025, 7, 16),
					CheckIn = new TimeSpan(9, 0, 0),
					CheckOut = new TimeSpan(17, 0, 0),
					Status = AttendanceStatus.Present
				},
				new Attendance
				{
					Id = 8,
					EmployeeId = Guid.Parse("b2222222-0000-0000-0000-000000000008"),
					Date = new DateTime(2025, 7, 16),
					CheckIn = null,
					CheckOut = null,
					Status = AttendanceStatus.Absent
				}
			};
		}
	}
}
