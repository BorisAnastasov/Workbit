namespace Workbit.Core.Models.Admin
{
    public class AdminUserDto
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // Could be User, Employee, Manager, CEO, Admin
        public string? Department { get; set; } // Null if unemployed
        public bool IsEmployed { get; set; } // True if they have an Employee record
    }
}
