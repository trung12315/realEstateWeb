using Model.Dao;
using Model.EF;
using realEstateWeb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Create()
        {
            SetViewBag();
            return View();
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


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(RealEstate model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreateBy = session.Username;
                new RealEstateDao().Create(model);
                //model.ViewCount = 0;
                return RedirectToAction("Index");
            }
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateMaps(RealEstate model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreateBy = session.Username;
                //model.MetaDescriptions = "400";
                //model.ViewCount = 400;
                model.Status = true;
                new RealEstateDao().Create(model);
                return RedirectToAction("Index");
            }
            SetViewBag();
            return View();
        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new RealEstateCategoryDao();
            ViewBag.CatID = new SelectList(dao.ListAll(), "CateID", "Name", selectedId);
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
        public JsonResult GetAllProvinces()
        {
            using (var db = new bdsWebContext())
            {
                var data = db.Provinces.OrderBy(x => x.name).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAllDistrictByProvinceId(string id = "01TTT")
        {
            using (var db = new bdsWebContext())
            {
                var data = db.Districts.Where(x => x.provinceid == id).OrderBy(x => x.name).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAllWardByDistrictId(string id = "001HH")
        {
            using (var db = new bdsWebContext())
            {
                var data = db.Wards.Where(x => x.districtid == id).OrderBy(x => x.name).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAllVillagedByWardId(string id = "00001")
        {
            using (var db = new bdsWebContext())
            {
                var data = db.Villages.Where(x => x.wardid == id).OrderBy(x => x.name).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}