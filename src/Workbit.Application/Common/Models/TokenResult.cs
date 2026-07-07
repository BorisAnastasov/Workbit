namespace Workbit.Application.Common.Models
{
    public class TokenResult
    {
        public Guid UserId { get; set; }
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
    }
}
