using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workbit.Core.Models.Admin
{
    public class AdminUserListViewModel
    {
        // Filters
        public string? SearchTerm { get; set; }
        public string? SelectedRole { get; set; }
        public string? SelectedDepartment { get; set; }

        // Sorting
        public string? SortBy { get; set; } = "Name"; // Default sort by name
        public bool SortDesc { get; set; }


        // The actual user list
        public List<AdminUserDto> Users { get; set; } = new();
    }
}
