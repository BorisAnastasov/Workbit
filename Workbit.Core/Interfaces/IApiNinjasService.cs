namespace Workbit.Core.Interfaces
{
    public interface IApiNinjasService
    {
        Task<int> GetWorkingDaysAsync(string country, int? month = null);
    }
}
