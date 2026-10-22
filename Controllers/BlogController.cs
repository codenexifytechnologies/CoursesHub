using CoursesHub.Areas.Admin.Models;
using CoursesHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Controllers
{
    public class BlogController : BaseController
    {
        BlogOperations objBlogOperations = new BlogOperations();
        // GET: Blog
        public ActionResult Blog()
        {
            List<Blogs> objListBlogs = new List<Blogs>();
            objListBlogs = objBlogOperations.GetBlogs();
            ViewBag.Blog = objListBlogs;
            return View();
        }

        //public ActionResult BlogDetail(int Id)
        //{
        //    Blogs objBlogs = new Blogs();
        //    try
        //    {
        //        objBlogs = objBlogOperations.ViewBlogs(Id, "User").FirstOrDefault();
        //    }
        //    catch (Exception Ex)
        //    {
        //        Common.errorlog(Convert.ToString(Ex.Message) + " Inner Msg:" + Convert.ToString(Ex.StackTrace), "BlogDetail", "BlogOperations");
        //    }
        //    ViewBag.BlogDetails = objBlogs;
        //    return View(objBlogs);
        //}

        public ActionResult BlogDetail(string slug)
        {
            Blogs objBlogs = new Blogs();
            try
            {
                slug = slug.Replace("-", " ");
                objBlogs = objBlogOperations.ViewBlogs(0, "User", slug).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                Common.errorlog(Convert.ToString(Ex.Message) + " Inner Msg:" + Convert.ToString(Ex.StackTrace), "BlogDetail", "BlogOperations");
            }
            ViewBag.BlogDetails = objBlogs;
            return View(objBlogs);
        }

    }
}