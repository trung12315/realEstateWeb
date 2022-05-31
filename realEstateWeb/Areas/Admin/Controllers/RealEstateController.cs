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

namespace realEstateWeb.Areas.Admin.Controllers
{
    public class RealEstateController : BaseController
    {
        // GET: Admin/RealEstate
        public ActionResult Index(string searchString, int page=1, int pageSize =10)
        {
            var dao = new RealEstateDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;

            return View(model);
        }
        public ActionResult IndexMaps(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new RealEstateDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }



        [HttpGet]
        public ActionResult CreateMaps()
        {
            SetViewBag();
            return View();
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new RealEstateDao();
            var content = dao.GetByID(id);

            SetViewBag(content.CategoryID);
            return View(content);
        }
        [HttpPost]
        public ActionResult Edit(RealEstate model)
        {
            if (ModelState.IsValid)
            {
                var dao = new RealEstateDao();

                var result = dao.Update(model);
                if (result)
                {
                    SetAlert("Cập nhật Tin tức thành công", "success");
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật tin tức thành công");
                }
            }
            SetViewBag(model.CategoryID);
            return View("Index");
        }

        [HttpGet]
        public ActionResult EditMaps(long id)
        {
            var dao = new RealEstateDao();
            var content = dao.GetByID(id);

            SetViewBag(content.CategoryID);
            return View(content);
        }
        [HttpPost]
        public ActionResult EditMaps(RealEstate model)
        {
            if (ModelState.IsValid)
            {
                var dao = new RealEstateDao();
                //model.ViewCount = 400;
                var result = dao.UpdateMaps(model);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("IndexMaps", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thành công");
                }
            }
            SetViewBag(model.CategoryID);
            return View("IndexMaps");
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(RealEstateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bdsWebContext db = new bdsWebContext();
                RealEstate realestate = new RealEstate();
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                realestate.CreateBy = session.Username;
                realestate.UserID = session.UserID;
                DateTime dt = DateTime.Now;
                String.Format("{0:dd/MM/yyyy}", dt);
                //Xử lý alias
                //if (string.IsNullOrEmpty(realestate.MetaTile))
                //{
                //    realestate.MetaTile = StringHelper.ToUnsignString(realestate.Name);
                //}
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
                int foreignKey = realestate.RealEstateID;
                Image image = new Image();
                image.RealEstateID = foreignKey;
                image.LinkImage = viewModel.LinkImage;
                db.Images.Add(image);
                db.SaveChanges();
                Image image1 = new Image();
                image1.LinkImage = viewModel.LinkImage1;
                image1.RealEstateID = foreignKey;
                db.Images.Add(image1);
                db.SaveChanges();
                //model.ViewCount = 0;
                return RedirectToAction("Index");
            }
            SetViewBag();
            return View(viewModel);
        }

        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult CreateMaps(RealEstate model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var session = (UserLogin)Session[CommonConstants.USER_SESSION];
        //        model.CreateBy = session.Username;
        //        //model.MetaDescriptions = "400";
        //        //model.ViewCount = 400;
        //        model.Status = true;
        //        new RealEstateDao().Create(model);
        //        return RedirectToAction("Index");
        //    }
        //    SetViewBag();
        //    return View();
        //}

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new RealEstateCategoryDao();
            var dao1 = new CategoryDao();
            ViewBag.CateID = new SelectList(dao.ListAll(), "CateID", "Name", selectedId);
            ViewBag.CatID = new SelectList(dao1.ListAll(), "CatID", "Name", selectedId);
            //var dao1 = new UserDao();
            //ViewBag.CatID = new SelectList(dao.ListAll(), "UserID", "Phone", selectedId);
        }



        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new RealEstateDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult changeStatus(long id)
        {
            var result = new RealEstateDao().changeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        //public JsonResult Test()
        //{
        //    using (var db = new bdsWebContext())
        //    {
        //        var data = db.Provinces.ToList();
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

        public JsonResult GetAllDistrictByProvinceId(string id="01TTT" )
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
        public JsonResult GetAllVillagedByWardId(string id = "00001")
        {
            using (var db = new bdsWebContext())
            {
                var data = db.villages.Where(x => x.wardid == id).OrderBy(x => x.name).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult LoadImages(int id)
        {
            RealEstateDao dao = new RealEstateDao();
            var realestate = dao.ViewDetail(id);
            var images = realestate.MoreImage;
            XElement xImages = XElement.Parse(images);
            List<string> listImagesReturn = new List<string>();

            foreach (XElement element in xImages.Elements())
            {
                listImagesReturn.Add(element.Value);
            }
            return Json(new
            {
                data = listImagesReturn
            }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SaveImages(int id, string images)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var listImages = serializer.Deserialize<List<string>>(images);
            XElement xElement = new XElement("Images");
            foreach (var item in listImages)
            {
                var subStringItem = item.Substring(23);
                xElement.Add(new XElement("Image", subStringItem));
            }
            RealEstateDao dao = new RealEstateDao();
            try
            {
                dao.UpdateImages(id, xElement.ToString());
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