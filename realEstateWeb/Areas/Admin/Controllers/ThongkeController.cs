using Model.Dao;
using realEstateWeb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace realEstateWeb.Areas.Admin.Controllers
{
    public class ThongkeController : Controller
    {
        // GET: Admin/Thongke
     
        public ActionResult Index()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                var dao1 = new UserDao();
                ViewBag.TongUser1 = dao1.TongUser1();
              
                var b1 = dao1.TongUser2();
                ViewBag.TongUser2 = b1.ToString();
                var b2 = dao1.TongUser3();
                ViewBag.TongUser3 = b2.ToString();
                var dao = new RealEstateDao();
                ViewBag.TongBaiDang1 = dao.BaiDang1().ToString();
                var a2 = dao.BaiDang2();
                ViewBag.TongBaiDang2 = a2.ToString();
                var a3 = dao.BaiDang3();
                ViewBag.TongBaiDang3 = a3.ToString();
                return View();
            }
            
        }
    }
}