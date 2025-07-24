namespace Workbit.Core.Models.Manager
{
    public class ManagerReadDto
    {
        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string OfficePhone { get; set; } = null!;
        public bool IsCeo { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;

        public List<string> TeamEmployees { get; set; } = new();
    }
}
