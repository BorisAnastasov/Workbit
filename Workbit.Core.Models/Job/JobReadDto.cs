namespace Workbit.Core.Models.Job
{
    public class JobReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DepartmentId { get; set; }
        public double BaseSalary { get; set; }

        public string? DepartmentName { get; set; }
        public List<string>? EmployeeNames { get; set; }
    }
}
