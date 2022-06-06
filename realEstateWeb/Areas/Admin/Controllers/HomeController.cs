using Model.Dao;
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
            ViewBag.SoNguoiTruyCap = HttpContext.Application["PageView"];
            return View();
      
        }
    }
}