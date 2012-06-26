using System.Web.Mvc;

namespace ECommDemo.Web.Areas.shopJS
{
	public class shopJSAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "shopJS";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"shopJS_default",
				"shopJS/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
