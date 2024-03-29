﻿using Model.Dao;
using Model.EF;
using realEstateWeb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace realEstateWeb.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 6)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        public JsonResult changeStatus(long id)
        {
            var result = new UserDao().changeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Create(Custommer user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                var encryptedMD5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMD5Pas;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Thêm người dùng thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm người dùng thành công");
                }
            }
            return View("Index");

        }
    }
}