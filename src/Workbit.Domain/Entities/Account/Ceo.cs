using System.ComponentModel.DataAnnotations;

namespace Workbit.Domain.Entities.Account
{
    public class Ceo
    {
        [Key]
        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
