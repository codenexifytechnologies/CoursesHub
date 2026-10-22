using CoursesHub.Areas.Admin.Filter;
using CoursesHub.Models;
using CoursesHub.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CoursesHub.Areas.Admin.Controllers
{
    /// <summary>Admin: list of newsletter subscribers (footer "Newsletter" form) with CSV export.</summary>
    [CheckLogin]
    public class NewsletterSubscribersController : Controller
    {
        readonly NewsletterOperation objNewsletterOperation = new NewsletterOperation();

        public ActionResult Index()
        {
            if (Session["AdminUser"] == null)
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
            }

            var subscribers = new List<NewsletterSubscriber>();
            try
            {
                subscribers = objNewsletterOperation.GetSubscribers();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Index", "NewsletterSubscribersController");
                ViewBag.LoadError = "Subscribers could not be loaded. Please make sure SQL/newsletter.sql has been run on the database.";
            }
            return View(subscribers);
        }

        // CSV of active subscribers (or all with ?all=true) for mailing tools
        public ActionResult ExportCsv(bool all = false)
        {
            if (Session["AdminUser"] == null)
                return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });

            var rows = objNewsletterOperation.GetSubscribers().Where(s => all || s.IsActive);
            var csv = new StringBuilder("Email,Status,Subscribed,Unsubscribed,Source\r\n");
            foreach (var s in rows)
            {
                csv.Append(Csv(s.Email)).Append(',')
                   .Append(s.IsActive ? "Subscribed" : "Unsubscribed").Append(',')
                   .Append(s.SubscribedDate.ToString("yyyy-MM-dd HH:mm")).Append(',')
                   .Append(s.UnsubscribedDate.HasValue ? s.UnsubscribedDate.Value.ToString("yyyy-MM-dd HH:mm") : "").Append(',')
                   .Append(Csv(s.Source)).Append("\r\n");
            }
            var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(csv.ToString())).ToArray();
            return File(bytes, "text/csv", "newsletter-subscribers-" + DateTime.Now.ToString("yyyyMMdd") + ".csv");
        }

        static string Csv(string value)
        {
            value = value ?? "";
            // stop spreadsheet formulas and quote the value
            if (value.Length > 0 && "=+-@".IndexOf(value[0]) >= 0) value = "'" + value;
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
    }
}
