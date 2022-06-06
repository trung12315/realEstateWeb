using Model.Dao;
using realEstateWeb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace realEstateWeb.Controllers
{
    public class RealEstateClientController : Controller
    {
        // GET: RealEstateClient
      
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(int id)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            var realestate = new RealEstateDao().ViewDetail(id);
            ViewBag.Category = new RealEstateCategoryDao().ViewDetail(realestate.CatID.Value);
            ViewBag.relatedProducts = new RealEstateDao().Listrelatedproducts(id);
            ViewBag.Image = new RealEstateDao().ListImage(id);



            return View(realestate);
        }
    }
}