namespace Workbit.Core.Models.Ceo
{
    public class CeoReadDto
    {
        public string UserId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; } = null!;
    }
}
