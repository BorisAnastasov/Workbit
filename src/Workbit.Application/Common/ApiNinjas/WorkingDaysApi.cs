using System.Text.Json.Serialization;

namespace Workbit.Application.Common.ApiNinjas
{
    public class WorkingDaysApi
    {
        [JsonPropertyName("num_working_days")]
        public int NumWorkingDays { get; set; }

        [JsonPropertyName("num_non_working_days")]
        public int NumNonWorkingDays { get; set; }

        [JsonPropertyName("working_days")]
        public List<DateTime> WorkingDays { get; set; } = new();

        [JsonPropertyName("non_working_days")]
        public List<NonWorkingDay> NonWorkingDays { get; set; } = new();
    }
}
