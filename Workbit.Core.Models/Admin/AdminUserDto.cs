namespace Workbit.Core.Models.Admin
{
    public class AdminUserDto
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; }
        public bool IsEmployed { get; set; }
    }
}
