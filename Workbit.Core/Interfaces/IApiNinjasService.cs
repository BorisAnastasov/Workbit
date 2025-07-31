using Workbit.Core.Models.ApiNinjas;

namespace Workbit.Core.Interfaces
{
    public interface IApiNinjasService
    {
        Task<WorkingDaysApi> GetWorkingDaysAsync(string country, int? month = null);
        bool IsTodayWorkingDayAsync(WorkingDaysApi response);

	}
}
