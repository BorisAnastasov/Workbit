namespace Workbit.Core.Models.Ceo
{
	public class CeoDashboardViewModel
	{
		public int TotalEmployees { get; set; }
		public int TotalManagers { get; set; }
		public int TotalDepartments { get; set; }
		public int PresentToday { get; set; }
		public int AbsentToday { get; set; }
		public decimal TotalPayrollThisMonth { get; set; }
		public int PaidEmployeesThisMonth { get; set; }

	}
}
