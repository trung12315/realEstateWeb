using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace realEstateWeb.Controllers
{
    public class HomeController : Controller
    {
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new RealEstateCategoryDao();
            //var dao1 = new CategoryDao();
            ViewBag.CateID = new SelectList(dao.ListAll(), "CateID", "Name", selectedId);
        }
            public ActionResult Index(string searchString, int page = 1, int pageSize = 9)
        {
            var productdao = new RealEstateDao();
            var image = new ImageDao();
            var model = productdao.ListAllPaging(searchString, page, pageSize);
            //ViewBag.Image = image.ListAll(id);
            ViewBag.NewProducts = productdao.ListNewRealEstate(9);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        //[ChildActionOnly]

        public ActionResult index1()
        {
            return View();
        }
        //public JsonResult GetAllCountries()
        //{
        //    using (var db = new  bdsWebContext())
        //    {
        //        var data = db.Countries.OrderBy(x => x.CountryCode).ToList();
        //        return Json(data, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public JsonResult GetAllProvinces()
        {
            using (var db = new bdsWebContext())
            {
                var data = db.provinces.OrderBy(x => x.name).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetAllDistrictByProvinceId(string id = "01TTT")
        {
            using (var db = new bdsWebContext())
            {
                var data = db.districts.Where(x => x.provinceid == id).OrderBy(x => x.name).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAllWardByDistrictId(string id = "001HH")
        {
            using (var db = new bdsWebContext())
            {
                var data = db.wards.Where(x => x.districtid == id).OrderBy(x => x.name).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}