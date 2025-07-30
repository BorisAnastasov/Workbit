using System.ComponentModel.DataAnnotations;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Entities
{
	public class Country
	{
		public Country()
		{
			Users = new List<ApplicationUser>();
		}

		[Key]
		[StringLength(2)]
		public string Code { get; set; } = null!;

		[Required]
		[StringLength(100)]
		public string Name { get; set; } = null!;

		[StringLength(3)]
		public string CurrencyCode { get; set; }

		[StringLength(50)]
		public string Continent { get; set; } = null!;

		public virtual IEnumerable<ApplicationUser>? Users { get; set; }
	}
}
