using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace realEstateWeb.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        //{
        //    var productdao = new RealEstateDao();
        //    var model = productdao.ListAllPaging(searchString, page, pageSize);
        //    ViewBag.NewProducts = productdao.ListNewRealEstate(10);
        //    ViewBag.SearchString = searchString;
        //    return View(model);
        //}
        [ChildActionOnly]
        public PartialViewResult CategoryShared()
        {
            var model = new CategoryDao().ListAll();
            return PartialView(model);
        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new RealEstateCategoryDao();
            //var dao1 = new CategoryDao();
            ViewBag.CateID = new SelectList(dao.ListAll(), "CateID", "Name", selectedId);
        }
        public ActionResult Search()
        {
            SetViewBag();
            return View();
        }
        public ActionResult SearchCategory(string search, string searchString, int page = 1, int pageSize = 2)
        {
            //int totalRecord = 0;
            var model = new RealEstateDao().Search(search, searchString, page, pageSize);
            ViewBag.NewProducts = new RealEstateDao().ListNewRealEstate(10);
            ViewBag.searchString = searchString;
            //ViewBag.Total = totalRecord;
            //ViewBag.pageIndex = page;
            ViewBag.search = search;
            ViewBag.T = searchString;
            //ViewBag.searchString= searchString;
            //int maxpage = 5;
            //int totalpage = 0;
            //totalpage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            //ViewBag.TotalPage = totalpage;
            //ViewBag.MaxPage = maxpage;
            //ViewBag.First = 1;
            //ViewBag.Last = totalpage;
            //ViewBag.Next = page + 1;
            //ViewBag.Prev = page - 1;

            return View(model);
        }
        public ActionResult Category(int id, int page = 1, int pageSize = 3)
        {
            var category = new CategoryDao().ViewDetail(id);
            ViewBag.Category = category;
            int totalRecord = 0;
            var model = new RealEstateDao().ListByCategoryId(id, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.pageIndex = page;
            int maxpage = 5;
            int totalpage = 0;
            totalpage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalpage;
            ViewBag.MaxPage = maxpage;
            ViewBag.First = 1;
            ViewBag.Last = totalpage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }
    }
}