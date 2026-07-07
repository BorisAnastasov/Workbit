using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Workbit.Domain.Entities;
using Workbit.Domain.Entities.Account;
using Workbit.Infrastructure.Database.Configuration;
using Workbit.Infrastructure.Extensions;

namespace Workbit.Infrastructure.Persistance
{
    public class WorkbitDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public WorkbitDbContext(DbContextOptions<WorkbitDbContext> opts,
                          IDataProtectionProvider dpProvider) : base(opts)
        {
            var provider = dpProvider ?? DataProtectionProvider.Create("Workbit");
        }
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Manager> Managers { get; set; } = null!;
        public virtual DbSet<Ceo> Ceos { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<AttendanceEntry> AttendanceEntries { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<DepartmentBudget> DepartmentBudgets { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>()
                .Ignore(e => e.Iban);

            builder.ConfigureDeleteBehaviourEntities();

            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new CompanyConfiguration());
            builder.ApplyConfiguration(new CeoConfiguration());
            builder.ApplyConfiguration(new DepartmentConfiguration());
            builder.ApplyConfiguration(new DepartmentBudgetConfiguration());
            builder.ApplyConfiguration(new ManagerConfiguration(managerProtector));
            builder.ApplyConfiguration(new JobConfiguration());
            builder.ApplyConfiguration(new EmployeeConfiguration(employeeProtector));
            builder.ApplyConfiguration(new PaymentConfiguration());
            builder.ApplyConfiguration(new AttendanceConfiguration());
        }
    }
}
