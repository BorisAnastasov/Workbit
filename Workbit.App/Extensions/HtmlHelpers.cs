using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Workbit.App.Extensions
{
	public static class HtmlHelpers
	{
		public static string IsActive(this IHtmlHelper htmlHelper, string action, string controller)
		{
			var routeData = htmlHelper.ViewContext.RouteData;

			var routeAction = routeData.Values["action"]?.ToString();
			var routeController = routeData.Values["controller"]?.ToString();

			var isActive = (string.Equals(controller, routeController, StringComparison.OrdinalIgnoreCase) &&
							string.Equals(action, routeAction, StringComparison.OrdinalIgnoreCase));

			return isActive ? "active" : "";
		}
	}
}
