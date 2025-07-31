using Microsoft.EntityFrameworkCore;
using Workbit.Infrastructure.Database.Entities;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureDeleteBehaviourEntities(this ModelBuilder builder)
        {
            // ------------------------
            // ApplicationUser Config
            // ------------------------
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasOne(u => u.Ceo)
                      .WithOne(c => c.ApplicationUser)
                      .HasForeignKey<Ceo>(c => c.ApplicationUserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(u => u.Manager)
                      .WithOne(m => m.ApplicationUser)
                      .HasForeignKey<Manager>(m => m.ApplicationUserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(u => u.Employee)
                      .WithOne(e => e.ApplicationUser)
                      .HasForeignKey<Employee>(e => e.ApplicationUserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.AttendanceEntries)
                      .WithOne(a => a.User)
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Payments)
                      .WithOne(p => p.Recipient)
                      .HasForeignKey(p => p.RecipientId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ------------------------
            // Ceo Config
            // ------------------------
            builder.Entity<Ceo>(entity =>
            {
                entity.HasOne<Company>()
                      .WithOne(c => c.Ceo)
                      .HasForeignKey<Company>(c => c.CeoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ------------------------
            // Company Config
            // ------------------------
            builder.Entity<Company>(entity =>
            {
                entity.HasOne(c => c.Ceo)
                      .WithOne()
                      .HasForeignKey<Company>(c => c.CeoId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(c => c.Departments)
                      .WithOne(d => d.Company)
                      .HasForeignKey(d => d.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ------------------------
            // Department Config
            // ------------------------
            builder.Entity<Department>(entity =>
            {
                entity.HasOne(d => d.Company)
                      .WithMany(c => c.Departments)
                      .HasForeignKey(d => d.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.Jobs)
                      .WithOne(j => j.Department)
                      .HasForeignKey(j => j.DepartmentId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.DepartmentBudgets)
                      .WithOne(b => b.Department)
                      .HasForeignKey(b => b.DepartmentId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Managers remain when Department is deleted (manual nulling needed)
                entity.HasMany(d => d.Managers)
                      .WithOne(m => m.Department)
                      .HasForeignKey(m => m.DepartmentId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // ------------------------
            // Manager Config
            // ------------------------
            builder.Entity<Manager>(entity =>
            {
                entity.HasOne(m => m.ApplicationUser)
                      .WithOne(u => u.Manager)
                      .HasForeignKey<Manager>(m => m.ApplicationUserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(m => m.Department)
                      .WithMany(d => d.Managers)
                      .HasForeignKey(m => m.DepartmentId)
                      .OnDelete(DeleteBehavior.NoAction); // Avoid multiple cascade paths
            });

            // ------------------------
            // DepartmentBudget Config
            // ------------------------
            builder.Entity<DepartmentBudget>(entity =>
            {
                entity.HasOne(b => b.Department)
                      .WithMany(d => d.DepartmentBudgets)
                      .HasForeignKey(b => b.DepartmentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ------------------------
            // Job Config
            // ------------------------
            builder.Entity<Job>(entity =>
            {
                entity.HasOne(j => j.Department)
                      .WithMany(d => d.Jobs)
                      .HasForeignKey(j => j.DepartmentId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Employees remain when Job is deleted (manual nulling needed)
                entity.HasMany(j => j.Employees)
                      .WithOne(e => e.Job)
                      .HasForeignKey(e => e.JobId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // ------------------------
            // Employee Config
            // ------------------------
            builder.Entity<Employee>(entity =>
            {
                entity.HasOne(e => e.ApplicationUser)
                      .WithOne(u => u.Employee)
                      .HasForeignKey<Employee>(e => e.ApplicationUserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Job)
                      .WithMany(j => j.Employees)
                      .HasForeignKey(e => e.JobId)
                      .OnDelete(DeleteBehavior.NoAction); // Avoid multiple cascade paths
            });

            // ------------------------
            // Payment Config
            // ------------------------
            builder.Entity<Payment>(entity =>
            {
                entity.HasOne(p => p.Recipient)
                      .WithMany(u => u.Payments)
                      .HasForeignKey(p => p.RecipientId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ------------------------
            // Attendance Config
            // ------------------------
            builder.Entity<AttendanceEntry>(entity =>
            {
                entity.HasOne(a => a.User)
                      .WithMany(u => u.AttendanceEntries)
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ------------------------
            // Country Config
            // ------------------------
            builder.Entity<Country>(entity =>
            {
                entity.HasMany(c => c.Users)
                            .WithOne(u => u.Country)
                            .HasForeignKey(u => u.CountryCode)
                            .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(c => c.Departments)
                        .WithOne(u => u.Country)
                        .HasForeignKey(u => u.CountryCode)
                        .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(c => c.Companies)
                        .WithOne(u => u.Country)
                        .HasForeignKey(u => u.CountryCode)
                        .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
