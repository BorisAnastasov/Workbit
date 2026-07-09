using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Domain.Entities.Account;

namespace Workbit.Infrastructure.Persistance.Configuration
{
    public class CeoConfiguration : IEntityTypeConfiguration<Ceo>
    {
        public void Configure(EntityTypeBuilder<Ceo> builder)
        {
            builder.HasData(
                new Ceo
                {
                    ApplicationUserId = Guid.Parse("9a2f4b30-c2fa-4c77-bf3a-9b6a4cf11801") // John Lewis
                }
            );
        }
    }
}