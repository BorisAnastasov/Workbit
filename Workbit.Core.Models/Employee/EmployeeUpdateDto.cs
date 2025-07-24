namespace Workbit.Core.Models.Employee
{
    public class EmployeeUpdateDto
    {
        public string Id { get; set; } = null!;       // ApplicationUserId as string
        public int JobId { get; set; }
        public string Level { get; set; } = null!;
    }
}
