using Model.Dao;
using realEstateWeb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace realEstateWeb.Controllers
{
    public class ReportController : BaseController
    {
        // GET: Report
        [HttpPost]
        public ActionResult Create(int id, string baocao)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
               
                    var userid = session.UserID;
                    var dao = new RepostDao();
                    var yes = dao.Dangtin(baocao, userid, id);

                    if (yes)
                    {
                        SetAlert("Cảm ơn đã để lại báo cáo chúng tôi sẽ xem xét ", "success");
                        return RedirectToAction("Detail", "RealEstateClient", new { id = id });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Báo cáo không thành công");
                    }
                
            }
            return View();
        }
    }
}