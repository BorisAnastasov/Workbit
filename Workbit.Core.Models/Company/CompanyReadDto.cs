namespace Workbit.Core.Models.Company
{
    public class CompanyReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string ContactPhone { get; set; } = null!;

        public string CeoId { get; set; } = null!;
        public string CeoName { get; set; } = null!;

        public List<string> Departments { get; set; } = new();
    }
}
