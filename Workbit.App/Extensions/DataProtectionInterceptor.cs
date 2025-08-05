using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Workbit.Infrastructure.Database.Entities.Account;

namespace Workbit.App.Extensions
{
    public class DataProtectionInterceptor : IMaterializationInterceptor
    {
        private readonly IDataProtector _employeeProtector;
        private readonly IDataProtector _managerProtector;

        public DataProtectionInterceptor(IDataProtectionProvider provider)
        {
            _employeeProtector = provider.CreateProtector("Employee.IBAN");
            _managerProtector = provider.CreateProtector("Manager.IBAN");
        }

        public object InitializedInstance(MaterializationInterceptionData data, object entity)
        {
            if (entity is Employee e) e.SetProtector(_employeeProtector);
            if (entity is Manager m) m.SetProtector(_managerProtector);
            return entity;
        }

        public object CreatedInstance(MaterializationInterceptionData data, object entity)
    => InitializedInstance(data, entity);

        public ValueTask<object> CreatedInstanceAsync(MaterializationInterceptionData data, object entity, CancellationToken ct)
            => new(InitializedInstance(data, entity));
    }

}
