using System.Text.Json.Serialization;

namespace Workbit.Core.Models.ApiNinjas
{
    public class NonWorkingDay
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; }

        [JsonPropertyName("holiday_name")]
        public string HolidayName { get; set; }
    }
}
