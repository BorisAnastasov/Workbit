namespace Workbit.Application.Features.Auth.Shared
{
    public class AuthResult
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public List<string> Roles { get; set; } = new();
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
        public string RefreshToken { get; set; } = null!;
        public DateTime RefreshTokenExpires { get; set; }
    }
}
