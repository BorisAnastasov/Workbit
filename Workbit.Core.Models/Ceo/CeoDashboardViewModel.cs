namespace Workbit.Core.Models.Ceo
{
	public class CeoDashboardViewModel
	{
        public int CompanyId { get; set; }
        public string CeoName { get; set; }
        public string CompanyName { get; set; }
        public string ContactPhone { get; set; }
        public string CountryName { get; set; }
        public int TotalEmployees { get; set; }
		public int TotalManagers { get; set; }
		public int TotalDepartments { get; set; }
		public int PresentToday { get; set; }
		public int AbsentToday { get; set; }
		public decimal TotalPayrollThisMonth { get; set; }
		public int PaidEmployeesThisMonth { get; set; }

	}
}
