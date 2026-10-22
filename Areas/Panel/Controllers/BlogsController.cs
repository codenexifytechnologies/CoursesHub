using CoursesHub.Controllers;
using CoursesHub.Filter;
using CoursesHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Areas.Panel.Controllers
{
    public class BlogsController : BaseController
    {
        // GET: Panel/Blogs
        #region Blog

        [SessionCheck]
        public ActionResult ViewBlogs()
        {
            Common.errorlog("","ViewBlogs", "BlogsController");
            if (Session["Registration"] != null)
            {
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "Login", new { area = "" });
            }
            return View();
        }

        [SessionCheck]
        public ActionResult AddBlog()
        {
            if (Session["Registration"] != null)
            {
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "Login", new { area = "" });
            }
            return View();
        }

        [SessionCheck]
        public ActionResult EditBlog()
        {
            if (Session["Registration"] != null)
            {
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "Login", new { area = "" });
            }
            return View();
        }
        #endregion
    }
}