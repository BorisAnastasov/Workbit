using System.Text.Json.Serialization;

namespace Workbit.Application.Common.ApiNinjas
{
    public class NonWorkingDay
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; } = string.Empty;

        [JsonPropertyName("holiday_name")]
        public string HolidayName { get; set; } = string.Empty;
    }
}
