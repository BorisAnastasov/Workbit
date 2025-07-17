using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Configuration
{
	public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.HasData(SeedApplicationUsers());
		}

		private static List<ApplicationUser> SeedApplicationUsers()
		{
			var hasher = new PasswordHasher<ApplicationUser>();
			var users = new List<ApplicationUser>();

			var userInfos = new (Guid Id, string Username, string FirstName, string LastName, string Email, DateTime Dob)[]
			{
                // Managers
                (Guid.Parse("a1111111-0000-0000-0000-000000000001"), "john.m.lewis", "John", "Lewis", "john.m.lewis@workbit.com", new DateTime(1980, 3, 15)),
				(Guid.Parse("a1111111-0000-0000-0000-000000000002"), "lisa.r.anderson", "Lisa", "Anderson", "lisa.r.anderson@workbit.com", new DateTime(1982, 5, 10)),
				(Guid.Parse("a1111111-0000-0000-0000-000000000003"), "carl.t.morgan", "Carl", "Morgan", "carl.t.morgan@workbit.com", new DateTime(1978, 11, 20)),
				(Guid.Parse("a1111111-0000-0000-0000-000000000004"), "nina.v.hughes", "Nina", "Hughes", "nina.v.hughes@workbit.com", new DateTime(1985, 9, 5)),

                // Employees
                (Guid.Parse("b2222222-0000-0000-0000-000000000001"), "alice.k.watson", "Alice", "Watson", "alice.k.watson@workbit.com", new DateTime(1995, 1, 12)),
				(Guid.Parse("b2222222-0000-0000-0000-000000000002"), "bob.c.thomas", "Bob", "Thomas", "bob.c.thomas@workbit.com", new DateTime(1994, 3, 8)),
				(Guid.Parse("b2222222-0000-0000-0000-000000000003"), "claire.b.james", "Claire", "James", "claire.b.james@workbit.com", new DateTime(1992, 6, 22)),
				(Guid.Parse("b2222222-0000-0000-0000-000000000004"), "dave.r.walker", "Dave", "Walker", "dave.r.walker@workbit.com", new DateTime(1993, 4, 14)),
				(Guid.Parse("b2222222-0000-0000-0000-000000000005"), "emily.d.young", "Emily", "Young", "emily.d.young@workbit.com", new DateTime(1991, 2, 10)),
				(Guid.Parse("b2222222-0000-0000-0000-000000000006"), "frank.h.scott", "Frank", "Scott", "frank.h.scott@workbit.com", new DateTime(1990, 8, 18)),
				(Guid.Parse("b2222222-0000-0000-0000-000000000007"), "grace.l.adams", "Grace", "Adams", "grace.l.adams@workbit.com", new DateTime(1996, 7, 30)),
				(Guid.Parse("b2222222-0000-0000-0000-000000000008"), "harry.n.brooks", "Harry", "Brooks", "harry.n.brooks@workbit.com", new DateTime(1997, 12, 2))
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
					DateOfBirth = dob
				};

				user.PasswordHash = hasher.HashPassword(user, "123456");
				users.Add(user);
			}

			return users;
		}
	}
}
