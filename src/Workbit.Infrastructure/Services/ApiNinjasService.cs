using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Workbit.Application.Common.ApiNinjas;
using Workbit.Application.Interfaces;

namespace Workbit.Infrastructure.Services
{
    public class ApiNinjasService : IApiNinjasService
    {
        private readonly HttpClient httpClient; 
        private readonly HybridCache cache;

        public ApiNinjasService(HttpClient httpClient,
            IConfiguration configuration,
            HybridCache cache)
        {
            this.httpClient = httpClient;
            this.cache = cache;
            var apiKey = configuration.GetSection("ApiNinjas:ApiKey").Value;

            this.httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
        }

        public async Task<WorkingDaysApi> GetWorkingDaysAsync(string country, int? month = null)
        {
            var selectedMonth = month ?? DateTime.UtcNow.Month;
            var cacheKey = $"workingdays:{country}:{DateTime.UtcNow.Year}:{selectedMonth}";

            return await cache.GetOrCreateAsync(
                cacheKey,
                async cancel =>
                {
                    var url = $"https://api.api-ninjas.com/v1/workingdays?country={country}&month={selectedMonth}";
                    var json = await httpClient.GetStringAsync(url, cancel);
                    return JsonSerializer.Deserialize<WorkingDaysApi>(json)!;
                });
        }

        public async Task<bool> IsTodayWorkingDayAsync(string country, int? month = null)
        {
            var result = await GetWorkingDaysAsync(country, month);

            return result.WorkingDays.Any(d => d.Date == DateTime.UtcNow.Date);
        }

        public bool IsTodayWorkingDayAsync(WorkingDaysApi response)
        {
            return response.WorkingDays.Any(d => d.Date.Date == DateTime.UtcNow.Date);
        }
    }
}
