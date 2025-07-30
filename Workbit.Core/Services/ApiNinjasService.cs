using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using Workbit.Core.Interfaces;

namespace Workbit.Core.Services
{
    public class ApiNinjasService:IApiNinjasService
    {
        private readonly HttpClient _httpClient;

        public ApiNinjasService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            var apiKey = configuration["ApiNinjas:ApiKey"]
                      ?? throw new InvalidOperationException("API key not configured.");

            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
        }

        public async Task<int> GetWorkingDaysAsync(string country, int? month = null)
        {
            var selectedMonth = month ?? DateTime.UtcNow.Month;

            var url = $"https://api.api-ninjas.com/v1/workingdays?country={country}&month={selectedMonth}";

            var response = await _httpClient.GetFromJsonAsync<int>(url);
            return response;
        }
    }
}
