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
        public ActionResult Index(string searchString2, int page2 = 1, int pageSize2 = 4)
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
            var model = productdao.ListAllMaps(searchString2, page2, pageSize2);
            ViewBag.SearchString2 = searchString2;
            return View(model);
      
        }
    }
}