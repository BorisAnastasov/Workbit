namespace Workbit.Core.Models.Manager
{
    public class AssignManagerViewModel
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;

        public string? SelectedManagerId { get; set; } // GUID string for dropdown

        public List<ManagerSummaryDto> AvailableManagers { get; set; } = new();
    }
}
