using Model.Dao;
using Model.EF;
using realEstateWeb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace realEstateWeb.Controllers
{
    public class profileUserController : BaseController
    {
        // GET: profileUser
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit()
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            var UserID = session.UserID;
            var user= new UserDao().ViewDetail1(UserID);

            ViewBag.User = user;
            return View(user);
            //var sessiona = (UserLogin)Session[realEstateWeb.Common.CommonConstants.USER_SESSION];
            //var id = sessiona.UserID;
            //var user = new UserDao().ViewDetail(id);
            //return View(user);
        }
        [HttpPost]
        public ActionResult Edit(Custommer user)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];

            var userid = session.UserID;
            ViewBag.User = new UserDao().ViewDetail1(userid);
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                //if (!string.IsNullOrEmpty(user.Password))
                //{
                //    var encryptedMD5Pas = Encryptor.MD5Hash(user.Password);
                //    user.Password = encryptedMD5Pas;
                //}

                var result = dao.Update(user, userid);
                if (result)
                {
                    SetAlert("Cập nhật người dùng thành công", "success");
                    return RedirectToAction("Edit", "profileUser");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật người dùng thất bại");
                }
            }
           
            return View("Edit");

        }
    }
}