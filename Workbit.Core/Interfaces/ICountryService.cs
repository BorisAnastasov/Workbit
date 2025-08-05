using Workbit.Core.Models.Country;

namespace Workbit.Core.Interfaces
{
	public interface ICountryService
	{
		Task<List<CountrySummaryModel>> GetCountrySummariesAsync();
	}
}
