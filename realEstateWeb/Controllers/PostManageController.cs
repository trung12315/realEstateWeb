using Model.Dao;
using realEstateWeb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace realEstateWeb.Controllers
{
    public class PostManageController : Controller
    {
        // GET: PostManage
        public ActionResult Index()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
                return Redirect("/");
            else
            {
                var userid = session.UserID;
                var model = new RealEstateDao().Manage(userid);
                return View(model);
            }
            
        }
    }
}