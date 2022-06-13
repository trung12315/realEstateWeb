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
    public class PostController : Controller
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
                
                realestate.UserID = session.UserID;
                DateTime dt = DateTime.Now;
                String.Format("{0:dd/MM/yyyy}", dt);



                realestate.CreateDate = dt;
                realestate.Address = viewModel.Address;
                realestate.Name = viewModel.Name;
                realestate.CateID = viewModel.CateID;
                realestate.Price = viewModel.Price;
                realestate.CatID = viewModel.CatID;
                realestate.Description = viewModel.Description;
                realestate.Acreage = viewModel.Acreage;
                string[] arrListStr1 = image.Split(',');
                realestate.Image = arrListStr1[0];
                db.RealEstates.Add(realestate);
                db.RealEstates.Add(realestate);
                db.SaveChanges();

                int foreignKey = realestate.RealEstateID;
                string chuoicon = ",";
                int strt = 0, cnt = -1, idx = -1;

                while (strt != -1)
                {
                    strt = image.IndexOf(chuoicon, idx + 1);
                    cnt += 1;
                    idx = strt;
                }

                if (cnt == 1)
                {
                    string[] arrListStr = image.Split(',');
                    Image image1 = new Image();

                    image1.LinkImage = arrListStr[0];
                    image1.RealEstateID = foreignKey;
                    
                    db.Images.Add(image1);
                    db.SaveChanges();
                    Image image2 = new Image();
                    image2.LinkImage = arrListStr[1];
                    image2.RealEstateID = foreignKey;
                    db.Images.Add(image2);
                    db.SaveChanges();
                }
                if (cnt == 2)
                {
                    string[] arrListStr = image.Split(',');
                    Image image1 = new Image();

                    image1.LinkImage = arrListStr[0];
                    db.Images.Add(image1);
                    db.SaveChanges();

                    image1.RealEstateID = foreignKey;
                    Image image2 = new Image();
                    image2.LinkImage = arrListStr[1];
                    image2.RealEstateID = foreignKey;
                    db.Images.Add(image2);
                    db.SaveChanges();

                    Image image3 = new Image();
                    image3.LinkImage = arrListStr[2];
                    image3.RealEstateID = foreignKey;
                    db.Images.Add(image3);
                    db.SaveChanges();
                }
                if (cnt == 3)
                {
                    string[] arrListStr = image.Split(',');
                    Image image1 = new Image();
                    image1.LinkImage = arrListStr[0];
                    image1.RealEstateID = foreignKey;
                    db.Images.Add(image1);
                    db.SaveChanges();

                    Image image2 = new Image();
                    image2.LinkImage = arrListStr[1];
                    image2.RealEstateID = foreignKey;
                    db.Images.Add(image2);
                    db.SaveChanges();

                    Image image3 = new Image();
                    image3.LinkImage = arrListStr[2];
                    image3.RealEstateID = foreignKey;
                    db.Images.Add(image3);
                    db.SaveChanges();

                    Image image4 = new Image();
                    image4.LinkImage = arrListStr[2];
                    image4.RealEstateID = foreignKey;
                    db.Images.Add(image4);
                    db.SaveChanges();
                }

             

                return RedirectToAction("Create");
            }

            var session1 = (UserLogin)Session[CommonConstants.USER_SESSION];
         
                var username = session1.UserID;
                ViewBag.User = new UserDao().ViewDetail1(username);

            
            SetViewBag();
            return View(viewModel);
        }
        //public JsonResult SaveImages( string images)
        //{
        //    RealEstate realestate = new RealEstate();
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    var listImages = serializer.Deserialize<List<string>>(images);
        //    int foreignKey = realestate.RealEstateID;
        //    Image image = new Image();
        //    foreach (var item in listImages)
        //    {
        //        var subStringItem = item.Substring(23);
        //        image.LinkImage = subStringItem;
        //        image.RealEstateID = foreignKey;
        //    }
        //    RealEstateDao dao = new RealEstateDao();
        //    try
        //    {
        //        dao.UpdateImage(subStringItem.ToString());

        //        return Json(new
        //        {
        //            status = true
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new
        //        {
        //            status = false
        //        });
        //    }

        //}

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