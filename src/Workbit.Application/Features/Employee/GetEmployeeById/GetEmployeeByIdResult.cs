namespace Workbit.Application.Features.Employee.GetEmployeeById
{
    public record GetEmployeeByIdResult
    {
        public Guid UserId { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public DateTime DateOfBirth { get; init; }
        public string PhoneNumber { get; init; } = string.Empty;
        public int? JobId { get; init; }
        public string? JobName { get; init; } = string.Empty;
        public string JobLevel { get; init; } = string.Empty;
        public string CountryCode { get; init; } = string.Empty;
        public string CountryName { get; init; } = string.Empty;
    }
}
