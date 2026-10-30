using CoursesHub.Areas.Admin.Models;
using CoursesHub.Models;
using Newtonsoft.Json;
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
            Common.SaveRequestResponse("BlogDetail", slug, "start");
            Blogs objBlogs = null;
            try
            {
                // Match on the same slug the blog list builds its links from (WebsiteLink),
                // not BlogTitle, otherwise the lookup never finds the post.
                objBlogs = objBlogOperations.GetBlogs()
                    .FirstOrDefault(b => string.Equals(b.slug, slug, StringComparison.OrdinalIgnoreCase));
                Common.SaveRequestResponse("BlogDetail", slug,JsonConvert.SerializeObject(objBlogs));

            }
            catch (Exception Ex)
            {
                Common.errorlog(Convert.ToString(Ex.Message) + " Inner Msg:" + Convert.ToString(Ex.StackTrace), "BlogDetail", "BlogOperations");
            }
            if (objBlogs == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogDetails = objBlogs;
            return View(objBlogs);
        }

    }
}