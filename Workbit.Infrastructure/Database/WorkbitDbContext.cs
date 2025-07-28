using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
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
        public virtual DbSet<Ceo> Ceos { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<AttendanceEntry> AttendanceEntries { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<DepartmentBudget> DepartmentBudgets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Department -> Budgets (Cascade: budgets are deleted when department is deleted)
            builder.Entity<Department>()
                .HasMany(d => d.DepartmentBudgets)
                .WithOne(b => b.Department)
                .HasForeignKey(b => b.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Department -> Jobs (Cascade: jobs are deleted when department is deleted)
            builder.Entity<Department>()
                .HasMany(d => d.Jobs)
                .WithOne(j => j.Department)
                .HasForeignKey(j => j.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Department -> Managers (SetNull: managers remain, department is set to NULL)
            builder.Entity<Department>()
                .HasMany(d => d.Managers)
                .WithOne(m => m.Department)
                .HasForeignKey(m => m.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            // Job -> Employees (SetNull: employees remain, job is set to NULL)
            builder.Entity<Job>()
                .HasMany(j => j.Employees)
                .WithOne(e => e.Job)
                .HasForeignKey(e => e.JobId)
                .OnDelete(DeleteBehavior.SetNull);

            // ApplicationUser -> Payments (Cascade: payments deleted with user)
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Payments)
                .WithOne(p => p.Recipient)
                .HasForeignKey(p => p.RecipientId)
                .OnDelete(DeleteBehavior.Cascade);

            // ApplicationUser -> AttendanceEntries (Cascade: attendance deleted with user)
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.AttendanceEntries)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Company>()
                .HasMany(c => c.Departments)
                .WithOne(d => d.Company)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new CompanyConfiguration());
            builder.ApplyConfiguration(new CeoConfiguration());
            builder.ApplyConfiguration(new DepartmentConfiguration());
            builder.ApplyConfiguration(new DepartmentBudgetConfiguration());
            builder.ApplyConfiguration(new ManagerConfiguration());
            builder.ApplyConfiguration(new JobConfiguration());
            builder.ApplyConfiguration(new EmployeeConfiguration());
            builder.ApplyConfiguration(new PaymentConfiguration());
            builder.ApplyConfiguration(new AttendanceConfiguration());

        }

    }
}
