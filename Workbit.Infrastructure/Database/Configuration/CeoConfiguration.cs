using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Configuration
{
    public class CeoConfiguration : IEntityTypeConfiguration<Ceo>
    {
        public void Configure(EntityTypeBuilder<Ceo> builder)
        {
            builder.HasData(
                new Ceo
                {
                    ApplicationUserId = Guid.Parse("9a2f4b30-c2fa-4c77-bf3a-9b6a4cf11801"), // John Lewis
                }
            );
        }
    }
}
