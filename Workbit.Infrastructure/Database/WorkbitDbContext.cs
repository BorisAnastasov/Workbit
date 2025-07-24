using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Workbit.Infrastructure.Database.Configuration;
using Workbit.Infrastructure.Database.Entities;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database
{
    public class WorkbitDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public WorkbitDbContext(DbContextOptions<WorkbitDbContext> options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = true;
        }
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Manager> Managers { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<AttendanceEntry> AttendanceEntries { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new CompanyConfiguration());       // Seed company first, CeoId NULL initially
            builder.ApplyConfiguration(new DepartmentConfiguration());     // Departments linked to company
            builder.ApplyConfiguration(new ManagerConfiguration());        // Managers linked to departments
            builder.ApplyConfiguration(new JobConfiguration());            // Jobs linked to departments
            builder.ApplyConfiguration(new EmployeeConfiguration());       // Employees linked to jobs
            builder.ApplyConfiguration(new PaymentConfiguration());        // Payments linked to employees
            builder.ApplyConfiguration(new AttendanceConfiguration());     // Attendance linked to users

        }

    }
}
