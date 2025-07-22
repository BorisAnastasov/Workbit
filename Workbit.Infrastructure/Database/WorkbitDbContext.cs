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
        public virtual DbSet<DepartmentManager> DepartmentsManagers { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Manager> Managers { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<SalaryPayment> SalaryPayments { get; set; } = null!;
        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyConfiguration(new ApplicationUserConfiguration());
			builder.ApplyConfiguration(new DepartmentConfiguration());
			builder.ApplyConfiguration(new ManagerConfiguration());
			builder.ApplyConfiguration(new CompanyConfiguration());
			builder.ApplyConfiguration(new DepartmentManagerConfiguration());
			builder.ApplyConfiguration(new JobConfiguration());
			builder.ApplyConfiguration(new EmployeeConfiguration());
			builder.ApplyConfiguration(new SalaryPaymentConfiguration());
			builder.ApplyConfiguration(new AttendanceConfiguration());
		}

	}
}
