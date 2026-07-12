using Workbit.Application.Common.ApiNinjas;

namespace Workbit.Application.Interfaces
{
    public interface IApiNinjasService
    {
        Task<WorkingDaysApi> GetWorkingDaysAsync(string country, int? month = null);

        Task<bool> IsTodayWorkingDayAsync(string country, int? month = null);

        bool IsTodayWorkingDayAsync(WorkingDaysApi response);

    }
}
