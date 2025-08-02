using System.ComponentModel.DataAnnotations;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Entities
{
	public class Country
	{
		public Country()
		{
			Companies = new List<Company>();
			Employees = new List<Employee>();
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

		public virtual IEnumerable<Employee> Employees { get; set; }
		public virtual IEnumerable<Company> Companies { get; set; }
	}
}
