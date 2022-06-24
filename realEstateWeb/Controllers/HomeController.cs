using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
        public ActionResult Index(string searchString, int page = 1, int pageSize = 12)
        {
            string markers = "[";
            string conString = ConfigurationManager.ConnectionStrings["bdsWebContext"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SELECT * FROM RealEstate");
            using (SqlConnection con = new SqlConnection(conString))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        
                            markers += "{";
                            markers += string.Format("'title': '{0}',", sdr["Name"]);
                            markers += string.Format("'lat': '{0}',", sdr["Lat"]);
                            markers += string.Format("'lng': '{0}',", sdr["Lng"]);
                            markers += string.Format("'description': '{0}'", sdr["Image"]);
                            markers += "},";

                        
                    }
                }
                con.Close();
            }

            markers += "];";
            ViewBag.Markers = markers;
            var productdao = new RealEstateDao();
            var image = new ImageDao();
            var model = productdao.ListAllPaging(searchString, page, pageSize);
            //ViewBag.Image = image.ListAll(id);
            ViewBag.NewProducts = productdao.ListNewRealEstate(9);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        //[ChildActionOnly]
        public ActionResult IndexMaps(string searchString2, int page2 = 1, int pageSize2 = 4)
        {
                 
            return View();
        }


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