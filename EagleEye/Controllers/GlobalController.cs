﻿using EagleEye.CommonHelper;
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

    public class AuthenticateUser : ActionFilterAttribute

        }
                return true;
}