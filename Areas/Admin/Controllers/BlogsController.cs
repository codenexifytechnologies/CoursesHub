using CoursesHub.Areas.Admin.Filter;
using CoursesHub.Areas.Admin.Models;
using CoursesHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Areas.Admin.Controllers
{
    [CheckLogin]
    public class BlogsController : Controller
    {
        BlogOperations objBlogOperations = new BlogOperations();

        #region AddBlog       

        public ActionResult AddBlog()
        {
            if (Session["AdminUser"] != null)
            {
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddBlog(Blogs objBlogs)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["AdminUser"] != null)
                    {
                        objBlogs.CreatedBy = Convert.ToString(Session["AdminUserId"]);
                        string result = objBlogOperations.AddBlog(objBlogs);
                        TempData["Message"] = result;
                    }
                    else
                    {
                        TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                        return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                    }
                }

                else
                {
                    TempData["ErrorMessage"] = ModelState.Values;

                }
            }
            catch (Exception Ex)
            {
                Common.errorlog(Convert.ToString(Ex.Message) + " Inner Msg:" + Convert.ToString(Ex.StackTrace), "AddBlog", "BlogController");
            }
            return View();
        }

        #endregion

        #region ViewBlog

        public ActionResult ViewBlogs()
        {
            List<Blogs> objListBlogsDetails = new List<Blogs>();
            try
            {
                if (Session["AdminUser"] != null)
                {
                    objListBlogsDetails = objBlogOperations.ViewBlogs(0, "Admin").ToList();
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception Ex)
            {
                Common.errorlog(Convert.ToString(Ex.Message) + " Inner Msg:" + Convert.ToString(Ex.StackTrace), "ViewBlogs", "BlogController");
            }

            return View(objListBlogsDetails);
        }

        #endregion

        #region EditBlog

        public ActionResult EditBlog(int id)
        {
            Blogs objBlogs = new Blogs();
            try
            {
                if (Session["AdminUser"] != null)
                {
                    objBlogs = objBlogOperations.ViewBlogs(id, "Admin").FirstOrDefault();
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception Ex)
            {
                Common.errorlog(Convert.ToString(Ex.Message) + " Inner Msg:" + Convert.ToString(Ex.StackTrace), "EditBlog Get", "BlogController");
            }

            return View(objBlogs);
        }

        [HttpPost]
        public ActionResult EditBlog(Blogs objBlogs)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Session["AdminUser"] != null)
                    {
                        objBlogs.CreatedBy = Convert.ToString(Session["AdminUserId"]);
                        string result = objBlogOperations.EditBlog(objBlogs);
                        TempData["Message"] = result;
                    }
                    else
                    {
                        TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                        return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = ModelState.Values;

                }
            }
            catch (Exception Ex)
            {
                Common.errorlog(Convert.ToString(Ex.Message) + " Inner Msg:" + Convert.ToString(Ex.StackTrace), "EditBlog Post", "BlogController");
            }

            return RedirectToAction("EditBlog", new { id = objBlogs.Id });
        }
        #endregion

        #region DeleteBlog
        public ActionResult DeleteBlog(int Id)
        {
            List<Blogs> objListBlogsDetails = new List<Blogs>();
            try
            {
                if (Session["AdminUser"] != null)
                {
                    string result = objBlogOperations.DeleteBlog(Id);
                    TempData["Message"] = result;

                    objListBlogsDetails = objBlogOperations.ViewBlogs(0, "Admin").ToList();
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception Ex)
            {
                Common.errorlog(Convert.ToString(Ex.Message) + " Inner Msg:" + Convert.ToString(Ex.StackTrace), "DeleteBlog", "BlogController");
            }

            return RedirectToAction("ViewBlogs");
        }

        #endregion
    }
}