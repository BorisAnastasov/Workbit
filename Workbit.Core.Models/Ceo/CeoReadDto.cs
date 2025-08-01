﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workbit.Core.Models.Ceo
{
    public class CeoReadDto
    {
        public string UserId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; } = null!;
    }
}
