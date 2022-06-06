using Model.Dao;
using realEstateWeb.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace realEstateWeb.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["PageView"].ToString();
            ViewBag.SoNguoiDangTruyCap = HttpContext.Application["OnLine"].ToString();
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                return Redirect("/Admin/Login");
            }
            else
            { 
            

            var dao = new RealEstateDao();
            var a = dao.TongBaiDang();
            ViewBag.TongBaiDang = a.ToString();
            return View();
        }
        }
    }
}