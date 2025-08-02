using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workbit.Infrastructure.Enumerations;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.Infrastructure.Database.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(
                new Employee
                {
                    ApplicationUserId = Guid.Parse("f92e7b0f-5123-40c8-9d28-8834a3c93005"),
                    JobId = 1,
                    Level = JobLevel.Mid,
                    IBAN = "BG80BNBG96611020345678",
                    CountryCode = "BG"// Bulgaria
                },
                new Employee
                {
                    ApplicationUserId = Guid.Parse("2b06417a-1460-4b10-8454-51069dfb2d06"),
                    JobId = 1,
                    Level = JobLevel.Junior,
                    IBAN = "IT60X0542811101000000123456",
                    CountryCode = "IT"// Italy
                },
                new Employee
                {
                    ApplicationUserId = Guid.Parse("ac2a1d43-b460-4f4e-8617-c2cfb61a8c07"),
                    JobId = 2,
                    Level = JobLevel.Lead,
                    IBAN = "ES9121000418450200051332",
                    CountryCode = "ES"// Spain
                },
                new Employee
                {
                    ApplicationUserId = Guid.Parse("90e3b7f8-7088-4b4e-b0fa-847fe4c6bc08"),
                    JobId = 2,
                    Level = JobLevel.Senior,
                    IBAN = "RO49AAAA1B31007593840000",
                    CountryCode = "RO" // Romania
                },
                new Employee
                {
                    ApplicationUserId = Guid.Parse("30c2adf9-9ab8-4c59-b356-2f8bb6c82d09"),
                    JobId = 3,
                    Level = JobLevel.Mid,
                    IBAN = "NL91ABNA0417164300",
                    CountryCode = "NL" // Netherlands
                },
                new Employee
                {
                    ApplicationUserId = Guid.Parse("a73cb7de-df18-4b6e-a573-0dcf1f703e10"),
                    JobId = 3,
                    Level = JobLevel.Junior,
                    IBAN = "PL61109010140000071219812874",
                    CountryCode = "PL" // Poland
                },
                new Employee
                {
                    ApplicationUserId = Guid.Parse("cf9f7b3e-6cdb-4b9a-a0b4-4e2f7d527e11"),
                    JobId = 4,
                    Level = JobLevel.Senior,
                    IBAN = "SE3550000000054910000003",
                    CountryCode = "SE" // Sweden
                },
                new Employee
                {
                    ApplicationUserId = Guid.Parse("a802de4b-6a4a-4e15-a8c3-41f5a6c8d012"),
                    JobId = 4,
                    Level = JobLevel.Lead,
                    IBAN = "IE29AIBK93115212345678",
                    CountryCode = "IE" // Ireland
                }
            );
        }

    }
}
