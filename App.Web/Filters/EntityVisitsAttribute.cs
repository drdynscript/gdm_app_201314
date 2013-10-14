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
    public class EntityVisitsAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var routeData = filterContext.RouteData;
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var entityId = Convert.ToInt64(filterContext.ActionParameters["id"]);

            UnitOfWork unitOfWork = new UnitOfWork();

            //CHECK IF SESSION AND ENTITY IS ALREADY PRESENT IN EntyVisit REPO
            var alreadyPresent = unitOfWork.VisitRepository.Get(v => v.EntityId == entityId).Where(v => v.SessionId == filterContext.HttpContext.Session.SessionID).Count();

            if (alreadyPresent == 0)
            {
                EntityVisit visit = new EntityVisit();
                visit.Entity = unitOfWork.EntityRepository.GetByID(entityId);
                visit.SessionId = filterContext.HttpContext.Session.SessionID;
                visit.UserAgent = filterContext.HttpContext.Request.UserAgent;

                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    visit.User = unitOfWork.UserRepository.Get(u => u.UserName.Equals(filterContext.HttpContext.User.Identity.Name)).FirstOrDefault();

                    alreadyPresent = unitOfWork.VisitRepository.Get(v => v.EntityId == entityId).Where(v => v.UserId == visit.User.Id).Count();

                    if (alreadyPresent == 0)
                    {
                        unitOfWork.VisitRepository.Insert(visit);
                        unitOfWork.Save();
                    }
                }
                else
                {
                    unitOfWork.VisitRepository.Insert(visit);
                    unitOfWork.Save();
                }
            }
        }
    }
}