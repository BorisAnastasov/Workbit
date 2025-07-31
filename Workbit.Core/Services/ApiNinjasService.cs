using Microsoft.Extensions.Configuration;
using Workbit.Core.Interfaces;
using Workbit.Core.Models.ApiNinjas;

namespace Workbit.Core.Services
{
    public class ApiNinjasService : IApiNinjasService
    {
        private readonly HttpClient httpClient;

        public ApiNinjasService(HttpClient _httpClient, IConfiguration configuration)
        {
            this.httpClient = _httpClient;

            var apiKey = configuration["ApiNinjas:ApiKey"];

            this.httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
        }

        public async Task<WorkingDaysApi> GetWorkingDaysAsync(string country, int? month = null)
        {
            var selectedMonth = month ?? DateTime.UtcNow.Month;

            var url = $"https://api.api-ninjas.com/v1/workingdays?country={country}&month={selectedMonth}";

            var json = await httpClient.GetStringAsync(url);
            var result = System.Text.Json.JsonSerializer.Deserialize<WorkingDaysApi>(json);

            return result!;
        }

        public bool IsTodayWorkingDayAsync(WorkingDaysApi response) 
        {
            return response.WorkingDays.Any(d=>d.Date == DateTime.Now.Date);
        }
    }
}
