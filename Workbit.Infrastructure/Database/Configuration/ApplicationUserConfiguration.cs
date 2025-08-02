using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasData(SeedApplicationUsers());
        }

        private static List<ApplicationUser> SeedApplicationUsers()
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var users = new List<ApplicationUser>();
            var random = new Random();

            string GeneratePhoneNumber()
            {
                // Generates a phone number like +1-555-123-4567
                int part1 = random.Next(100, 999);
                int part2 = random.Next(1000, 9999);
                return $"+1-555-{part1}-{part2}";
            }

            // Each GUID is a valid, static identifier (hexadecimal only)
            var userInfos = new (Guid Id, string Username, string FirstName, string LastName, string Email, DateTime Dob)[]
            {
    // CEO
                (Guid.Parse("9a2f4b30-c2fa-4c77-bf3a-9b6a4cf11801"), "john.m.lewis", "John", "Lewis", "john.m.lewis@workbit.com", new DateTime(1980, 3, 15)),

    // Managers
    // Existing managers:
(Guid.Parse("f83d8c21-0b43-4b15-8fd1-20f4e5c72f02"), "lisa.r.anderson", "Lisa", "Anderson", "lisa.r.anderson@workbit.com", new DateTime(1982, 5, 10)),
// NEW manager for UK/GB
(Guid.Parse("befa88e8-83fc-4b64-b4ce-7de0b97b6e51"), "michael.j.evans", "Michael", "Evans", "michael.j.evans@workbit.com", new DateTime(1983, 8, 18)),

(Guid.Parse("64d07af7-8ed1-4620-b34d-bd0a4cb81d03"), "carl.t.morgan", "Carl", "Morgan", "carl.t.morgan@workbit.com", new DateTime(1978, 11, 20)),
// NEW manager for Germany/DE
(Guid.Parse("b06c8a25-b13c-4d31-bb49-113aa0cc46b8"), "julia.p.schmidt", "Julia", "Schmidt", "julia.p.schmidt@workbit.com", new DateTime(1984, 2, 28)),

(Guid.Parse("b0cf2834-19c5-43f5-b29e-9bb85a5a5d04"), "nina.v.hughes", "Nina", "Hughes", "nina.v.hughes@workbit.com", new DateTime(1985, 9, 5)),
// NEW manager for France/FR
(Guid.Parse("b23ae748-2292-4712-8778-3eb591c2f001"), "antoine.c.leblanc", "Antoine", "LeBlanc", "antoine.c.leblanc@workbit.com", new DateTime(1981, 10, 9)),


    // Employees
    (Guid.Parse("f92e7b0f-5123-40c8-9d28-8834a3c93005"), "alice.k.watson", "Alice", "Watson", "alice.k.watson@workbit.com", new DateTime(1995, 1, 12)),
    (Guid.Parse("2b06417a-1460-4b10-8454-51069dfb2d06"), "bob.c.thomas", "Bob", "Thomas", "bob.c.thomas@workbit.com", new DateTime(1994, 3, 8)),
    (Guid.Parse("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"), "claire.b.james", "Claire", "James", "claire.b.james@workbit.com", new DateTime(1992, 6, 22)),
    (Guid.Parse("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08"), "dave.r.walker", "Dave", "Walker", "dave.r.walker@workbit.com", new DateTime(1993, 4, 14)),
    (Guid.Parse("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09"), "emily.d.young", "Emily", "Young", "emily.d.young@workbit.com", new DateTime(1991, 2, 10)),
    (Guid.Parse("a73cb7de-df18-4b6e-a573-0dcf1f703e10"), "frank.h.scott", "Frank", "Scott", "frank.h.scott@workbit.com", new DateTime(1990, 8, 18)),
    (Guid.Parse("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11"), "grace.l.adams", "Grace", "Adams", "grace.l.adams@workbit.com", new DateTime(1996, 7, 30)),
    (Guid.Parse("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012"), "harry.n.brooks", "Harry", "Brooks", "harry.n.brooks@workbit.com", new DateTime(1997, 12, 2)),

    // Admin
    (Guid.Parse("d5e7f9a2-0ac3-4b6d-8c64-6fd8e4c0c013"), "adminadmin", "Admin", "Adminov", "admin@gmail.com", new DateTime(1994, 12, 2)),
};


            foreach (var (id, username, firstName, lastName, email, dob) in userInfos)
            {
                var user = new ApplicationUser
                {
                    Id = id,
                    UserName = username,
                    NormalizedUserName = username.ToUpper(),
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dob,
                    PhoneNumber = GeneratePhoneNumber(),
                };

                user.PasswordHash = hasher.HashPassword(user, "123456");
                users.Add(user);
            }

            return users;
        }
    }
}
