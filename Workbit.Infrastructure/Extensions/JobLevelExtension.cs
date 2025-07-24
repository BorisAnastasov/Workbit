using Workbit.Infrastructure.Enumerations;

namespace Workbit.Infrastructure.Extensions
{
    public static class JobLevelExtension
    {
        public static decimal GetMultiplier(this JobLevel level) => level switch
        {
            JobLevel.Junior => 1.0m,
            JobLevel.Mid => 1.2m,
            JobLevel.Senior => 1.5m,
            JobLevel.Lead => 1.8m,
            _ => 1.0m
        };
    }
}
