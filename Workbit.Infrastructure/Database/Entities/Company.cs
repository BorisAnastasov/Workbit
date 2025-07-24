using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Infrastructure.Database.Entities.Account;
using static Workbit.Common.DataConstants.Company;

namespace Workbit.Infrastructure.Database.Entities
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
		public virtual ApplicationUser Ceo { get; set; } = null!;

		public virtual List<Department> Departments { get; set; }
	}
}
