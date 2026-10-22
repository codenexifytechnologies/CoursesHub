using CoursesHub.Areas.Admin.Models;
using CoursesHub.Areas.Panel.Models;
using CoursesHub.Models;
using CoursesHub.Operation;
using Newtonsoft.Json.Linq;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Controllers
{
    public class IndexController : BaseController
    {
        CourseDetailsOperation objCourseDetailsOperation = new CourseDetailsOperation();
        BlogOperations objBlogOperations = new BlogOperations();
        // GET: Index
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                TempData["Message"] = TempData["Message"];
            }

            CourseDetails objCourseDetails = new CourseDetails();
            List<CourseDetails> objListCourseDetails = new List<CourseDetails>();
            List<Category> objListCategory = new List<Category>();
            List<Blogs> objListBlogs = new List<Blogs>();
            List<Category> lstCategory = new List<Category>();
            try
            {
                objListCourseDetails = objCourseDetailsOperation.GetCourseDetails(objCourseDetails, "");
                objListCategory = Common.getcategory().Select(x => new Category
                {
                    Id = int.Parse(x.Value),
                    CategoryValue = x.Text
                }).ToList();
                objListBlogs = objBlogOperations.GetBlogs();
                lstCategory = Common.getcategoryData();


            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Index", "IndexController");
            }
            ViewBag.All = objListCourseDetails;
            ViewBag.Provider = objListCourseDetails.GroupBy(x => x.UniversityName).Select(g => new CourseDetails
            {
                UniversityName = g.Key,
                UniversityLogo = g.FirstOrDefault().UniversityLogo,
                Count = g.Count()
            }).ToList();
            ViewBag.Category = objListCategory;
            ViewBag.Blog = objListBlogs;
            ViewBag.lstCategory = lstCategory;
            ViewBag.Testimonials = objCourseDetailsOperation.GetTestimonials(0, 0, 0, "All");
            return View();
        }

        public ActionResult CareerChange()
        {
            ViewBag.CoursesList = Common.getcategory();

            using (HttpClient client = new HttpClient())
            {
                var jsonString = client.GetStringAsync("https://api.first.org/data/v1/countries").Result;

                JObject json = JObject.Parse(jsonString);
                JObject data = (JObject)json["data"];  // get "data" object

                var list = new List<object>();

                foreach (var item in data)
                {
                    list.Add(new
                    {
                        Value = item.Value["country"].ToString(),
                        Text = item.Value["country"].ToString()
                    });
                }
                ViewBag.CountryList = list.OrderBy(x => x.GetType().GetProperty("Text").GetValue(x, null)).ToList();
            }


            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0"); // Required

                var jsonString = client.GetStringAsync("https://v0.postcodeapi.com.au/states/").Result;

                JArray states = JArray.Parse(jsonString);

                var list = states.Select(s => new
                {
                    Value = s["name"].ToString(),
                    Text = s["name"].ToString()
                }).ToList().OrderBy(x => x.Text);

                ViewBag.StateList = list;
            }

            ViewBag.CourseCategory = Common.getcategory(null);
            ViewBag.CourseLevel = Common.getCourseLevel();
            return View();
        }

        [HttpPost]
        public ActionResult CareerChange(CareerPage objCareerPage)
        {
            try
            {
                string body = System.IO.File.ReadAllText(Server.MapPath("~/Emails/StudentIntrest.html"));
                body = body.Replace("{{student_name}}", objCareerPage.objStudentRegistration.FirstName + " " + objCareerPage.objStudentRegistration.LastName);
                body = body.Replace("{{student_email}}", objCareerPage.objStudentRegistration.EmailId);
                body = body.Replace("{{student_contact}}", objCareerPage.objStudentRegistration.Mobile);
                body = body.Replace("{{category_name}}", objCareerPage.objCareer.CourseCategoryName);
                body = body.Replace("{{study_method}}", objCareerPage.objCareer.StudyMode);
                body = body.Replace("{{course_level}}", objCareerPage.objCareer.CourseLevel);
                body = body.Replace("{{education}}", objCareerPage.objCareer.EducationLevel);
                Common.SendEmail(Convert.ToString(ConfigurationManager.AppSettings["CcEmail"].ToString()), Convert.ToString(ConfigurationManager.AppSettings["FromEmail"].ToString()), ConfigurationManager.AppSettings["Student_Intrest"].ToString(), body);
                TempData["CareerPage"] = objCareerPage;
                return RedirectToAction("AllCourses", "Courses", new { area = "" });
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Login", "CareerChange");
                return View("Index");
            }
        }

        [Route("FAQ")]
        public ActionResult FAQ()
        {
            return View("~/Views/Shared/_Faq.cshtml");
        }

        [Route("Terms")]
        public ActionResult Terms()
        {
            return View("~/Views/Shared/_Terms.cshtml");
        }

        [Route("Error")]
        public ActionResult Error()
        {
            return View("~/Views/Shared/_Error.cshtml");
        }

        public ActionResult SearchResults(string keyword)
        {
            CourseDetails objCourseDetails = new CourseDetails();
            List<CourseDetails> objListCourseDetails = new List<CourseDetails>();
            ViewBag.List = objCourseDetailsOperation.GetSearchCourseDetails(keyword);
            return PartialView("_SearchResults", ViewBag.List);
        }

        public ActionResult Master()
        {
            ViewBag.GetMaster = Common.getMasters();
            // Qualification Level options come from the courses (same as the All Courses sidebar)
            try { ViewBag.MenuQualifications = CourseFilterHelper.MenuQualificationOptions(); }
            catch (Exception ex) { Common.errorlog(Convert.ToString(ex.Message), "Master qualifications", "IndexController"); }
            return View();
        }
        [HttpPost]
        public ActionResult SetSessionValues(string EmailId, string Password, string LoginType)
        {
            LoginType = LoginTypes.Normalize(LoginType);
            if (Session["Registration"] == null && LoginType != null)
            {
                LoginOperation objLoginOperation = new LoginOperation();
                Login objLogin = new Login
                {
                    EmailId = EmailId,
                    Password = Encoding.UTF8.GetString(Convert.FromBase64String(Password)),
                    LoginType = LoginType
                };
                Registration objRegistration = objLoginOperation.Login(objLogin);
                objRegistration.LoginType = objLogin.LoginType;

                if (objRegistration.objStudentRegistration != null || objRegistration.objUniversityRegistration != null)
                {
                    if (objRegistration.objStudentRegistration != null)
                    {
                        if (string.IsNullOrEmpty(objRegistration.objStudentRegistration.SqlMsg))
                        {
                            objRegistration.objStudentRegistration.Password =
                                   Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(
                                       objRegistration.objStudentRegistration.Password));
                            Session["Registration"] = objRegistration;
                        }
                        else
                        {
                            TempData["ErrMessage"] = objRegistration.objStudentRegistration.SqlMsg;
                        }
                    }
                    else if (objRegistration.objUniversityRegistration != null)
                    {
                        if (string.IsNullOrEmpty(objRegistration.objUniversityRegistration.SqlMsg))
                        {
                            objRegistration.objUniversityRegistration.Password =
                                   Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(
                                       objRegistration.objUniversityRegistration.Password));
                            Session["Registration"] = objRegistration;
                        }
                        else
                        {
                            TempData["ErrMessage"] = objRegistration.objUniversityRegistration.SqlMsg;
                        }
                    }
                    else
                    {
                        TempData["ErrMessage"] = "Enter Valid Credentials.";
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetSessionValues()
        {
            Session["Registration"] = Session["Registration"];
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}