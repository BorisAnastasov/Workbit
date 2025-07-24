namespace Workbit.Core.Models.Department
{
    public class DepartmentReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CompanyId { get; set; }

        public List<string> ManagerNames { get; set; } = new List<string>();
        public List<string> JobTitles { get; set; } = new List<string>();
    }
}
