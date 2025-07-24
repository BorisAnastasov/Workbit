namespace Workbit.Core.Models.Employee
{
    public class EmployeeCreateDto
    {
        public string ApplicationUserId { get; set; } = null!;
        public int JobId { get; set; }
        public string Level { get; set; } = null!;
    }
}
