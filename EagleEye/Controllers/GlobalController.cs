using EagleEye.CommonHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EagleEye.Controllers
{
    public class GlobalController : Controller
    {
        // GET: Global
        public ActionResult Index()
        {
            return View();
        }
    }

    public class AuthenticateUser : ActionFilterAttribute    {                public override void OnActionExecuting(ActionExecutingContext context)        {            base.OnActionExecuting(context);            if (!IsAthenticatedUser())            {                context.Result = new RedirectToRouteResult(                new RouteValueDictionary                {                        {"controller", "Users"},                        {"action", "Login"}                });                return;            }

        }        public bool IsAthenticatedUser()        {            if (!string.IsNullOrWhiteSpace(Utility.GetCookie("LoggedUserId")))
                return true;            else                return false;        }    }
}