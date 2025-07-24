using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Infrastructure.Database.Entities.Account;
using Workbit.Infrastructure.Enumerations;

namespace Workbit.Infrastructure.Database.Entities
{
    public class AttendanceEntry
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;

        public DateTime Timestamp { get; set; }
        public EntryType Type { get; set; }  // CheckIn or CheckOut
    }
}
