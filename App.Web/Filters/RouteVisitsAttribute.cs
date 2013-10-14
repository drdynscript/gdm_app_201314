using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Security;
using Data.orm;
using Models;

namespace App.Web.Filters
{
    public class RouteVisitsAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
            {
                var routeData = filterContext.RouteData;
                var controllerName = routeData.GetRequiredString("controller");
                var actionName = routeData.GetRequiredString("action");

                UnitOfWork unitOfWork = new UnitOfWork();

                RouteVisit visit = new RouteVisit();
                visit.Id = Guid.NewGuid();
                visit.Controller = controllerName.ToString();
                visit.Action = actionName.ToString();
                visit.SessionId = filterContext.HttpContext.Session.SessionID;
                visit.UserAgent = filterContext.HttpContext.Request.UserAgent;
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                    visit.User = unitOfWork.UserRepository.Get(u => u.UserName.Equals(filterContext.HttpContext.User.Identity.Name)).FirstOrDefault();

                unitOfWork.RouteRepository.Insert(visit);
                unitOfWork.Save();
            }
        }
    }
}