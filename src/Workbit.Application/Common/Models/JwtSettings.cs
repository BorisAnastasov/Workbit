namespace Workbit.Application.Common.Models
{
    public class JwtSettings
    {
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int ExpiredDays { get; set; } = 1;
    }
}
