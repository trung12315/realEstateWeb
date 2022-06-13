using Model.Dao;
using realEstateWeb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace realEstateWeb.Controllers
{
    public class PostManageController : Controller
    {
        // GET: PostManage
        public ActionResult Index(string searchString, int page = 1, int pageSize = 3)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
                return Redirect("/");
            else
            {
                var userid = session.UserID;
                var model = new RealEstateDao().Manage(userid, searchString, page, pageSize);
                ViewBag.SearchString = searchString;
                return View(model);
            }
            
        }
        public JsonResult LoadImages(int id)
        {
            RealEstateDao dao = new RealEstateDao();
            var image = dao.ViewDetail2(id);
            List<string> listImagesReturn = new List<string>();

            foreach (var element in image)
            {
                listImagesReturn.Add(element.LinkImage);
                
            }

            return Json(new
            {
                data = listImagesReturn
            }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult SaveImages(int id, string images)
        {
            RealEstateDao dao = new RealEstateDao();
            dao.DeleteImage(id);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var listImages = serializer.Deserialize<List<string>>(images);

            foreach (var item in listImages)
            {
                
               
                dao.UpdateImages(id, item);
            }
           
            try
            {
               
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }

        }
    }
}