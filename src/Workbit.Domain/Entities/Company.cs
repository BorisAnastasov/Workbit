using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Domain.Entities.Account;
using static Workbit.Domain.Constants.DataConstants.Company;

namespace Workbit.Domain.Entities
{
    public class Company
    {
        public Company()
        {
            this.Departments = new List<Department>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLen)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLen)]
        public string Address { get; set; } = null!;

        [Required]
        [Phone]
        [StringLength(ContactPhoneMaxLen)]
        public string ContactPhone { get; set; } = null!;

        [ForeignKey(nameof(Ceo))]
        public Guid CeoId { get; set; }
        public virtual Ceo Ceo { get; set; } = null!;

        [ForeignKey(nameof(Country))]
        public string CountryCode { get; set; }
        public virtual Country Country { get; set; } = null!;

        public virtual List<Department> Departments { get; set; }
    }
}
