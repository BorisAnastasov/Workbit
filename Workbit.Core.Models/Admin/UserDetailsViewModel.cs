namespace Workbit.Core.Models.Admin
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? Role { get; set; }
    }
}
