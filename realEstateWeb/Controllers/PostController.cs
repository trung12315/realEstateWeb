using Model.Dao;
using Model.EF;
using Model.ViewModel;
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
    public class PostController : BaseController
    {
        // GET: Post
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new RealEstateCategoryDao();
            var dao1 = new CategoryDao();
            ViewBag.CateID = new SelectList(dao.ListAll(), "CateID", "Name", selectedId);
            ViewBag.CatID = new SelectList(dao1.ListAll(), "CatID", "Name", selectedId);
            //var dao1 = new UserDao();
            //ViewBag.CatID = new SelectList(dao.ListAll(), "UserID", "Phone", selectedId);
        }
        [HttpGet]
        public ActionResult Index()
        {
            SetViewBag();
            return View();

        }
        [HttpGet]
        public ActionResult Create()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
                return Redirect("/");
            else
            {
                var username = session.UserID;
                ViewBag.User = new UserDao().ViewDetail1(username);
               
            }
            SetViewBag();
            return View();


        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(RealEstateViewModel viewModel, string image)
        {
            if (ModelState.IsValid)
            {
                bdsWebContext db = new bdsWebContext();
                RealEstate realestate = new RealEstate();
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                
                var userid = session.UserID;
                var dao = new RealEstateDao();
                var yes = dao.Dangtin(viewModel, userid, image);
                
                if(yes)
                {
                    SetAlert("Đăng tin thành công ", "success");
                    return RedirectToAction("Create", "Post");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng tin thất bại");
                }

            }

            var session1 = (UserLogin)Session[CommonConstants.USER_SESSION];
         
                var username = session1.UserID;
                ViewBag.User = new UserDao().ViewDetail1(username);

            
            SetViewBag();
            return View(viewModel);
        }


        public JsonResult SaveImage(string images)
        {

            bdsWebContext db = new bdsWebContext();
            RealEstate realestate = new RealEstate();

            int foreignKey = realestate.RealEstateID;
            Image image = new Image();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var listImages = serializer.Deserialize<List<string>>(images);
            //XElement xElement = new XElement("");
            //int foreignKey = realestate.RealEstateID;
            //Image image = new Image();
            foreach (var item in listImages)
            {

                var subStringItem = item.Substring(23);

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
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(RealEstateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bdsWebContext db = new bdsWebContext();
                RealEstate realestate = new RealEstate();
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                //realestate.CreateBy = session.Username;
                realestate.UserID = session.UserID;
                DateTime dt = DateTime.Now;
                String.Format("{0:dd/MM/yyyy}", dt);


                realestate.Image = viewModel.LinkImage;
                realestate.CreateDate = dt;
                realestate.Address = viewModel.Address;
                realestate.Name = viewModel.Name;
                realestate.CateID = viewModel.CateID;
                realestate.CatID = viewModel.CatID;
                realestate.Description = viewModel.Description;
                realestate.Acreage = viewModel.Acreage;
                realestate.Detail = viewModel.Detail;
                realestate.Status = viewModel.Status;
                db.RealEstates.Add(realestate);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            SetViewBag();
            return View(viewModel);
        }

    }
}