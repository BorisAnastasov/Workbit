namespace Workbit.Core.Models.Admin
{
    public class AdminUserListViewModel
    {
        public string? SearchTerm { get; set; }
        public string? SelectedRole { get; set; }
        public List<AdminUserDto> Users { get; set; } = new();
    }
}
