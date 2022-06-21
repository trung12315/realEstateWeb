using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace realEstateWeb.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        // GET: Admin/Repost
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new RepostDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;

            return View(model);
           
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new RepostDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult changeStatus(long id)
        {
            var result = new RepostDao().changeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}