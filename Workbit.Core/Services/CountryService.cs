using Microsoft.EntityFrameworkCore;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.Country;
using Workbit.Infrastructure.Database.Entities;
using Workbit.Infrastructure.Database.Repository;

namespace Workbit.Core.Services
{
	public class CountryService:ICountryService
	{
		private readonly IRepository repository;

		public CountryService(IRepository _repository)
		{
			repository = _repository;
		}

		public async Task<List<CountrySummaryModel>> GetCountrySummariesAsync()
		{
			return await repository.AllReadOnly<Country>()
				.OrderBy(c => c.Name)
				.Select(c => new CountrySummaryModel
				{
					Code = c.Code,
					Name = c.Name
				})
				.ToListAsync();
		}
	}
}
