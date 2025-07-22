using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Core.Enumerations;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Entities
{
	public class Attendance
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Employee))]
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; } = null!;

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public TimeSpan? CheckIn { get; set; }
        public TimeSpan? CheckOut { get; set; }

        public AttendanceStatus Status { get; set; } = AttendanceStatus.Absent;
    }

}
