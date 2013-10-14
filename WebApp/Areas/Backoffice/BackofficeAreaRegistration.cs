using System.Web.Mvc;

namespace WebApp.Areas.Backoffice
{
    public class BackofficeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Backoffice";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name:"Backoffice_default",
                url:"Backoffice/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebApp.Areas.Backoffice.Controllers" }
            ).DataTokens["UseNamespaceFallback"] = false;
        }
    }
}
