﻿using Workbit.Core.Models.Payment;

namespace Workbit.Core.Models.Employee
{
    public class EmployeeReadDto
    {
        public string Id { get; set; } = null!;      
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int? JobId { get; set; }
        public string JobTitle { get; set; } = string.Empty!;
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty!;
        public string Level { get; set; } = string.Empty!;
        public List<PaymentSummaryDto> Payments { get; set; } = new();
    }
}
