using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Domain.Attributes;

namespace Workbit.Domain.Entities.Account
{
    public class Manager
    {
        [Key]
        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        [ForeignKey(nameof(Department))]
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; } = null!;

        [ValidIban]
        [Required]
        public string Iban { get; set; } = null!;
    }
}
