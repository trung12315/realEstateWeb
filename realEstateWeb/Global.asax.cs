﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace realEstateWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["PageView"] = 0;
            Application["OnLine"] = 0;
        }
        protected void Session_Start()
        {
            Application.Lock();
            Application["PageView"] = (int)Application["PageView"] + 1;
            Application["OnLine"] = (int)Application["OnLine"] + 1;
            Application.UnLock();
        }
        protected void Session_End()
        {
            Application.Lock();           
            Application["OnLine"] = (int)Application["OnLine"] - 1;
            Application.UnLock();
        }
    }
}
