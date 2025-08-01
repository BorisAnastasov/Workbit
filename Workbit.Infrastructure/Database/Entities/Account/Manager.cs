﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Workbit.Infrastructure.Attributes;

namespace Workbit.Infrastructure.Database.Entities.Account
{
	public class Manager
    {
        [Key]
        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        [ForeignKey(nameof(Department))]
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [ValidIban]
        public string IBAN { get; set; } = null!;

    }
}
