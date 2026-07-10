using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Workbit.Application.Interfaces;
using Workbit.Domain.Entities;
using Workbit.Domain.Entities.Account;
using Workbit.Infrastructure.Extensions;
using Workbit.Infrastructure.Persistance.Configuration;

namespace Workbit.Infrastructure.Persistance
{
    public class WorkbitDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly IEncryptionService encryptionService;
        public WorkbitDbContext(DbContextOptions<WorkbitDbContext> options,
                          IEncryptionService encryptionService) : base(options)
        {
            this.encryptionService = encryptionService;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureIbanConverter(encryptionService);

            builder.ConfigureDeleteBehaviourEntities();

            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new CompanyConfiguration());
            builder.ApplyConfiguration(new CeoConfiguration());
            builder.ApplyConfiguration(new DepartmentConfiguration());
            builder.ApplyConfiguration(new DepartmentBudgetConfiguration());
            builder.ApplyConfiguration(new ManagerConfiguration(encryptionService));
            builder.ApplyConfiguration(new JobConfiguration());
            builder.ApplyConfiguration(new EmployeeConfiguration(encryptionService));
            builder.ApplyConfiguration(new PaymentConfiguration());
            builder.ApplyConfiguration(new AttendanceConfiguration());
        }
    }
}
