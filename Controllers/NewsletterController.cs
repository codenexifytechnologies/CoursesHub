using CoursesHub.Models;
using CoursesHub.Operation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace CoursesHub.Controllers
{
    /// <summary>Footer newsletter: subscribe (AJAX) and unsubscribe (link in the welcome email).</summary>
    public class NewsletterController : BaseController
    {
        readonly NewsletterOperation objNewsletterOperation = new NewsletterOperation();

        const int MaxAttemptsPerSession = 5;
        static readonly TimeSpan AttemptWindow = TimeSpan.FromMinutes(10);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subscribe(string email, string website = null, string source = null)
        {
            JsonResult result = SubscribeCore(email, website, source);
            if (Request.IsAjaxRequest()) return result;

            // Without JavaScript the form posts normally: show the message as a toast and go back
            var messageProperty = result.Data.GetType().GetProperty("message");
            TempData["Message"] = messageProperty == null ? null : messageProperty.GetValue(result.Data);
            string back = Request.UrlReferrer != null && Url.IsLocalUrl(Request.UrlReferrer.PathAndQuery) ? Request.UrlReferrer.PathAndQuery : Url.Action("Index", "Index", new { area = "" });
            return Redirect(back);
        }

        JsonResult SubscribeCore(string email, string website, string source)
        {
            // "website" is a hidden field people never fill in; bots usually do
            if (!string.IsNullOrEmpty(website))
                return Json(new { ok = true, message = "Thank you for subscribing!" });

            if (TooManyAttempts())
                return Json(new { ok = false, message = "Too many attempts. Please try again in a few minutes." });

            string cleanEmail = NewsletterOperation.CleanEmail(email);
            if (cleanEmail == null)
                return Json(new { ok = false, message = "Please enter a valid email address." });

            try
            {
                var result = objNewsletterOperation.Subscribe(cleanEmail, string.IsNullOrWhiteSpace(source) ? "Footer" : source.Trim());
                if (result == null)
                    return Json(new { ok = false, message = "Something went wrong. Please try again." });

                if (result.Result == "AlreadySubscribed")
                    return Json(new { ok = true, already = true, message = "You're already subscribed. Thanks for staying with us!" });

                // Only the subscriber gets an email (no admin notification)
                SendWelcomeEmail(cleanEmail, result.Token);
                return Json(new { ok = true, message = "Thank you for subscribing! Please check your inbox." });
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Subscribe", "NewsletterController");
                return Json(new { ok = false, message = "We couldn't subscribe you right now. Please try again later." });
            }
        }

        // Link from the welcome email: /Newsletter/Unsubscribe?token=...
        [HttpGet]
        public ActionResult Unsubscribe(string token)
        {
            ViewBag.Status = "Invalid";
            Guid parsed;
            if (Guid.TryParse(token ?? "", out parsed))
            {
                try
                {
                    var result = objNewsletterOperation.Unsubscribe(parsed);
                    if (result != null)
                    {
                        ViewBag.Status = result.Result;
                        ViewBag.Email = result.Email;
                    }
                }
                catch (Exception ex)
                {
                    Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Unsubscribe", "NewsletterController");
                    ViewBag.Status = "Error";
                }
            }
            return View();
        }

        bool TooManyAttempts()
        {
            var attempts = (Session["NewsletterAttempts"] as List<DateTime>) ?? new List<DateTime>();
            attempts = attempts.Where(t => DateTime.UtcNow - t < AttemptWindow).ToList();
            attempts.Add(DateTime.UtcNow);
            Session["NewsletterAttempts"] = attempts;
            return attempts.Count > MaxAttemptsPerSession;
        }

        void SendWelcomeEmail(string email, Guid token)
        {
            try
            {
                string unsubscribeUrl = Url.Action("Unsubscribe", "Newsletter", new { area = "", token = token }, Request.Url.Scheme);
                string siteUrl = Url.Action("Index", "Index", new { area = "" }, Request.Url.Scheme);
                string scheme = Request.Url.Scheme;
                string body = System.IO.File.ReadAllText(Server.MapPath("~/Emails/Newsletter_Welcome.html"))
                    .Replace("{{unsubscribe_link}}", unsubscribeUrl)
                    .Replace("{{site_link}}", siteUrl)
                    .Replace("{{contact_link}}", Url.Action("ContactUs", "ContactUs", new { area = "" }, scheme))
                    .Replace("{{year}}", DateTime.Now.Year.ToString());
                Common.SendEmail(email, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]), "Welcome to Courses Hub – you're subscribed!", body);
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "SendWelcomeEmail", "NewsletterController");
            }
        }
    }
}
