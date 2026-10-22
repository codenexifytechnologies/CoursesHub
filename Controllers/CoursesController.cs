using CoursesHub.Areas.Admin.Models;
using CoursesHub.Areas.Panel.Models;
using CoursesHub.Filter;
using CoursesHub.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static CoursesHub.Models.Common;

namespace CoursesHub.Controllers
{
    public class CoursesController : BaseController
    {
        CourseDetailsOperation objCourseDetailsOperation = new CourseDetailsOperation();
        LeadOperations objLeadOperations = new LeadOperations();

        // All training providers A-Z (same data as the home page "Popular Training Providers" section)
        [Route("TrainingProviders")]
        public ActionResult TrainingProviders()
        {
            var providers = new List<ProviderSummary>();
            try
            {
                List<CourseDetails> courses = objCourseDetailsOperation.GetCourseDetails(new CourseDetails(), "");
                providers = courses
                    .Where(x => !string.IsNullOrWhiteSpace(x.UniversityName))
                    .GroupBy(x => x.UniversityName.Trim(), StringComparer.OrdinalIgnoreCase)
                    .Select(g => new ProviderSummary
                    {
                        Name = g.First().UniversityName.Trim(),
                        Logo = g.Select(x => x.UniversityLogo).FirstOrDefault(l => !string.IsNullOrWhiteSpace(l)),
                        CourseCount = g.Count(),
                        StudyAreas = g.Select(x => x.CategoryName).Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(s => s).ToList(),
                        Locations = g.SelectMany(x => CourseFilterHelper.Values(x.State)).Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(s => s).ToList()
                    })
                    .OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "TrainingProviders", "CoursesController");
            }
            return View(providers);
        }

        // All student reviews (the same verified testimonials the home page slider shows)
        [Route("Reviews")]
        public ActionResult Reviews()
        {
            var reviews = new List<Testimonial>();
            try
            {
                reviews = objCourseDetailsOperation.GetTestimonials(0, 0, 0, "All") ?? new List<Testimonial>();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Reviews", "CoursesController");
            }
            return View(reviews);
        }

        // All featured courses (set by the admin); the home page shows the first 6
        [Route("FeaturedCourses")]
        public ActionResult FeaturedCourses()
        {
            var featured = new List<CourseDetails>();
            try
            {
                featured = CourseFilterHelper.FeaturedCourses(objCourseDetailsOperation.GetCourseDetails(new CourseDetails(), ""));
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "FeaturedCourses", "CoursesController");
            }
            return View(featured);
        }

        // All course categories (fields of study) with their course counts; the home page shows the first 8
        [Route("Categories")]
        public ActionResult Categories()
        {
            var categories = new List<Category>();
            try
            {
                categories = (Common.getcategoryData() ?? new List<Category>())
                    .OrderBy(c => c.CategoryValue, StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Categories", "CoursesController");
            }
            return View(categories);
        }

        // "Find a provider" results: every provider offering one course (by national code or title),
        // using the answers from the Find a provider questions (resident? postcode : country)
        [Route("FindProvider")]
        public ActionResult FindProvider(string course, string resident = null, string postcode = null, string country = null)
        {
            var model = new FindProviderResult
            {
                Course = (course ?? "").Trim(),
                IsResident = !string.Equals(resident, "no", StringComparison.OrdinalIgnoreCase),
                Postcode = (postcode ?? "").Trim(),
                Country = (country ?? "").Trim()
            };
            model.State = model.IsResident ? CourseFilterHelper.StateFromPostcode(model.Postcode) : null;
            try
            {
                if (!string.IsNullOrEmpty(model.Course))
                {
                    var key = model.Course.ToLowerInvariant();
                    var offers = objCourseDetailsOperation.GetCourseDetails(new CourseDetails(), "")
                        .Where(x => string.Equals(CourseFilterHelper.CourseCode(x.CourseName), model.Course, StringComparison.OrdinalIgnoreCase)
                                 || CourseFilterHelper.CourseTitle(x.CourseName).ToLowerInvariant() == key)
                        .ToList();

                    if (offers.Count > 0)
                    {
                        model.CourseCode = CourseFilterHelper.CourseCode(offers[0].CourseName);
                        model.CourseTitle = CourseFilterHelper.CourseTitle(offers[0].CourseName);
                    }

                    // Domestic students see domestic offers, international students see international ones
                    // (offers without a student type are open to both)
                    string wanted = model.IsResident ? "Domestic" : "International";
                    offers = offers.Where(x => string.IsNullOrEmpty(CourseFilterHelper.StudentTypeValue(x.StudentType))
                                            || CourseFilterHelper.StudentTypeValue(x.StudentType) == wanted).ToList();

                    if (!string.IsNullOrEmpty(model.State))
                    {
                        model.InArea = offers.Where(x => CourseFilterHelper.Values(x.State).Any(s => CourseFilterHelper.SameState(s, model.State))).ToList();
                        model.Elsewhere = offers.Except(model.InArea).ToList();
                    }
                    else
                    {
                        model.InArea = offers;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "FindProvider", "CoursesController");
            }
            if (string.IsNullOrEmpty(model.CourseTitle)) model.CourseTitle = CourseFilterHelper.CourseTitle(model.Course);
            return View(model);
        }

        // FilterType/FilterValue: existing filters (CategoryType, CategoryId, StudeyType, State, ...)
        // Search: keyword from the header search box (course name, category, provider or description)
        public ActionResult AllCourses(string FilterType = null, string FilterValue = null, string Search = null)
        {
            CourseDetails objCourseDetails = new CourseDetails();
            List<CourseDetails> objListCourseDetails = new List<CourseDetails>();
            CareerPage objCareerPage = null;
            try
            {
                if (TempData["CareerPage"] != null)
                {
                    objCareerPage = TempData["CareerPage"] as CareerPage;
                }

                // Qualification Level is worked out from the course name in the app (see below), so it is not sent to the procedure
                bool qualificationFilter = FilterType == "QualificationType";
                objListCourseDetails = objCourseDetailsOperation.GetCourseDetails(objCourseDetails, "", qualificationFilter ? null : FilterType, qualificationFilter ? null : FilterValue);
                if (objCareerPage != null)
                {
                    //objListCourseDetails = objListCourseDetails.Where(x => x.StudyMethod == objCareerPage.objCareer.StudyMode || x.CourseCategory == Convert.ToInt32(objCareerPage.objCareer.CourseCategory)).ToList();
                    objListCourseDetails = objListCourseDetails.Where(x => x.CourseLevelName == objCareerPage.objCareer.StudyMode || x.CourseCategory == Convert.ToString(objCareerPage.objCareer.CourseCategory) || x.StudyMethod == objCareerPage.objCareer.CourseLevel).ToList();
                }

                // Category chosen in the header search / home category links. The stored procedure only receives
                // a category id through a separate argument, so apply the selected category here.
                if (FilterType == "CategoryId" && !string.IsNullOrWhiteSpace(FilterValue))
                {
                    string categoryId = FilterValue.Trim();
                    objListCourseDetails = objListCourseDetails.Where(x => (x.CourseCategory ?? string.Empty).Trim() == categoryId).ToList();
                    ViewBag.SearchCategoryName = Common.getcategory()?.FirstOrDefault(c => c.Value == categoryId)?.Text;
                }

                // Training provider chosen from the home page "Popular Training Provider" cards
                if (FilterType == "Provider" && !string.IsNullOrWhiteSpace(FilterValue))
                {
                    string provider = FilterValue.Trim();
                    objListCourseDetails = objListCourseDetails.Where(x => string.Equals((x.UniversityName ?? string.Empty).Trim(), provider, StringComparison.OrdinalIgnoreCase)).ToList();
                    ViewBag.SearchProviderName = provider;
                }

                // Keyword from the header search box
                if (!string.IsNullOrWhiteSpace(Search))
                {
                    string term = Search.Trim();
                    objListCourseDetails = objListCourseDetails.Where(x =>
                        ContainsText(x.CourseName, term) ||
                        ContainsText(x.CategoryName, term) ||
                        ContainsText(x.CourseProvider, term) ||
                        ContainsText(StripHtml(x.AboutCourse), term)).ToList();
                    ViewBag.Search = term;
                }
                // Filters from the header "Find Your Course" drawer
                if (!string.IsNullOrWhiteSpace(FilterValue))
                {
                    if (FilterType == "GovernmentFunding")
                    {
                        // Filter on the server by the GovernmentFunding column ("Government Funded" = Yes, "Fee for Service" = No)
                        bool? wanted = CourseFilterHelper.FundingFlag(FilterValue);
                        if (wanted != null)
                        {
                            objListCourseDetails = objListCourseDetails.Where(x => CourseFilterHelper.FundingFlag(x.GovernmentFunding) == wanted).ToList();
                        }
                        ViewBag.PreselectGroup = "funding";
                        ViewBag.PreselectKey = CourseFilterHelper.Key(FilterValue);
                        ViewBag.SearchFilterLabel = FilterValue.Trim();
                    }
                    else if (FilterType == "StudentType")
                    {
                        // Filter on the server by the StudentType column ("Domestic" / "International")
                        string wantedType = CourseFilterHelper.StudentTypeValue(FilterValue);
                        if (wantedType != null)
                        {
                            objListCourseDetails = objListCourseDetails.Where(x => CourseFilterHelper.StudentTypeValue(x.StudentType) == wantedType).ToList();
                        }
                        ViewBag.PreselectGroup = "studenttype";
                        ViewBag.PreselectKey = CourseFilterHelper.Key(FilterValue);
                        ViewBag.SearchFilterLabel = FilterValue.Trim();
                    }
                    else if (FilterType == "StudeyType" || FilterType == "State")
                    {
                        ViewBag.SearchFilterLabel = FilterValue.Trim();
                    }
                    else if (FilterType == "QualificationType")
                    {
                        // FilterValue is a tbl_QualificationLevel_master Id (header menu) or the level name itself
                        string value = FilterValue.Trim();
                        var level = Common.getMasters()?.objQualificationLevel?.FirstOrDefault(q => q.objQualificationLevelId.ToString() == value);
                        string levelName = level != null ? level.objQualificationLevel : value;
                        objListCourseDetails = objListCourseDetails
                            .Where(x => CourseFilterHelper.SameQualification(CourseFilterHelper.Qualification(x), levelName))
                            .ToList();
                        ViewBag.PreselectGroup = "qualification";
                        ViewBag.PreselectKey = CourseFilterHelper.Key(levelName);
                        ViewBag.SearchFilterLabel = levelName;
                    }
                }
                ViewBag.SelectedCategoryType = FilterType == "CategoryType" ? FilterValue : null;
                TempData["SelectedCategoryId"] = FilterType == "CategoryId" ? FilterValue : null;
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ViewCourses", "CoursesController");
            }
            ViewBag.All = objListCourseDetails;
            ViewBag.CategoryType = Common.getcategorytype();
            return View();
        }

        private static bool ContainsText(string value, string term)
        {
            return !string.IsNullOrEmpty(value) && value.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static string StripHtml(string html)
        {
            return string.IsNullOrEmpty(html) ? html : System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", " ");
        }

        // GET: Courses
        //public ActionResult AllCourses(string CategoryType = null, string CategoryId = null)
        //{
        //    CourseDetails objCourseDetails = new CourseDetails();
        //    List<CourseDetails> objListCourseDetails = new List<CourseDetails>();
        //    CareerPage objCareerPage = null;
        //    try
        //    {
        //        if (TempData["CareerPage"] != null)
        //        {
        //            objCareerPage = TempData["CareerPage"] as CareerPage;
        //        }

        //        objListCourseDetails = objCourseDetailsOperation.GetCourseDetails(objCourseDetails, "", CategoryType, CategoryId);
        //        if (objCareerPage != null)
        //        {
        //            //objListCourseDetails = objListCourseDetails.Where(x => x.StudyMethod == objCareerPage.objCareer.StudyMode || x.CourseCategory == Convert.ToInt32(objCareerPage.objCareer.CourseCategory)).ToList();
        //            objListCourseDetails = objListCourseDetails.Where(x => x.CourseLevelName == objCareerPage.objCareer.StudyMode || x.CourseCategory == Convert.ToInt32(objCareerPage.objCareer.CourseCategory) || x.StudyMethod == objCareerPage.objCareer.CourseLevel).ToList();
        //        }
        //        ViewBag.SelectedCategoryType = CategoryType;
        //        TempData["SelectedCategoryId"] = CategoryId;
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ViewCourses", "CoursesController");
        //    }
        //    ViewBag.All = objListCourseDetails;
        //    ViewBag.CategoryType = Common.getcategorytype();
        //    return View();
        //}

        [Route("Courses/{Title}/{CourseId}")]
        public ActionResult CourseDetails(int CourseId, string Title)
        {

            CourseDetails objCourseDetails = new CourseDetails();
            List<CourseDetails> objListCourseDetails = new List<CourseDetails>();
            objListCourseDetails = objCourseDetailsOperation.GetCourseDetails(objCourseDetails, "");
            Registration objRegistration = new Registration();

            Session["CourseId"] = CourseId;
            objCourseDetails.Id = CourseId;
            try
            {
                if (Session["Registration"] != null)
                {
                    objRegistration = (Registration)Session["Registration"];
                }
                objCourseDetails = objListCourseDetails.Where(x => x.Id == CourseId).FirstOrDefault();
                objCourseDetails.Testimonials = objCourseDetailsOperation.GetTestimonials(0, CourseId, 0, "");

            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "CourseDetails", "CoursesController");
            }
            ViewBag.CourseDetails = objCourseDetails;
            ViewBag.SameCourses = objListCourseDetails.Where(x => x.CourseCategory == objCourseDetails.CourseCategory && x.Id != objCourseDetails.Id).ToList();
            ViewBag.SameUniversity = objListCourseDetails.Where(x => x.UniversityId == objCourseDetails.UniversityId && x.Id != objCourseDetails.Id).ToList();
            ViewBag.isStudent = objRegistration.objUniversityRegistration == null ? 1 : 0;
            return View();
        }

        public ActionResult CourseDetails(int CourseId)
        {
            CourseDetails objCourseDetails = new CourseDetails();
            List<CourseDetails> objListCourseDetails = new List<CourseDetails>();
            objListCourseDetails = objCourseDetailsOperation.GetCourseDetails(objCourseDetails, "");

            Session["CourseId"] = CourseId;
            try
            {
                objCourseDetails = objListCourseDetails.Where(x => x.Id == CourseId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "CourseDetails", "CoursesController");
            }

            return RedirectToAction("CourseDetails", new { CourseId = CourseId, Title = objCourseDetails.CourseName.Replace(" ", "-") });
        }

        public ActionResult SaveTestimonial(Testimonial objTestimonial)
        {
            try
            {
                if (Session["Registration"] != null)
                {
                    Registration objRegistration = (Registration)Session["Registration"];
                    objTestimonial.Student_Id = objRegistration.objStudentRegistration.Id;
                    objTestimonial.Person_Name = objRegistration.objStudentRegistration.FirstName;
                    objTestimonial.Designation = "Student";
                    objTestimonial.Course_Id = Session["CourseId"] == null ? 0 : Convert.ToInt32(Session["CourseId"]);
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "Login", new { area = "" });
                }

                TempData["Message"] = objCourseDetailsOperation.SaveTestimonial(objTestimonial);
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "SaveTestimonial", "CoursesController");
            }

            return RedirectToAction("CourseDetails", new { CourseId = Convert.ToInt32(Session["CourseId"]) });
        }

        [SessionCheck]
        public ActionResult ConnectToUnivarsity(int CourseId, int UniversityId)
        {
            string msg = string.Empty;
            string university_name = string.Empty;
            try
            {
                Registration objRegistration = new Registration();
                if (Session["Registration"] != null)
                {
                    objRegistration = (Registration)Session["Registration"];
                }
                //else
                //{
                //    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                //    return RedirectToAction("LogOff", "Login", new { area = "" });
                //}

                msg = objCourseDetailsOperation.Connect(CourseId, UniversityId, objRegistration.objStudentRegistration);
                if (msg.Contains(","))
                {
                    var parts = msg.Split(',');
                    if (parts.Length > 1)
                    {
                        university_name = parts[0];
                        msg = parts[1];
                    }
                }
                TempData["Message"] = msg;
                if (msg.Contains("Thank you for apply"))
                {
                    string subject = Convert.ToString(ConfigurationManager.AppSettings["Connect_University"]);
                    subject = subject.Replace("{{university_name}}", university_name);
                    string body = System.IO.File.ReadAllText(Server.MapPath("~/Emails/Connect_University.html"));
                    body = body.Replace("{{university_name}}", university_name);
                    body = body.Replace("{{student_name}}", objRegistration.objStudentRegistration.FirstName + " " + objRegistration.objStudentRegistration.MiddleName + " " + objRegistration.objStudentRegistration.LastName);
                    Common.SendEmail(objRegistration.objStudentRegistration.EmailId, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]), subject, body);

                    /*Start - Send Mail to Premium Lead*/
                    List<Lead> lstPremiumLead = objLeadOperations.GetPremiumLeadsByIdForSendingMail(objRegistration.objStudentRegistration.Id);


                    foreach (var item in lstPremiumLead)
                    {
                        Lead objLead = objLeadOperations.GetAllDetails(item.Id, "P").FirstOrDefault();
                        subject = Convert.ToString(ConfigurationManager.AppSettings["Student_Inquiry"]);
                        subject = subject.Replace("{{Course_Name}}", objLead.Course_Name);
                        subject = subject.Replace("{{Student_Name}}", objLead.Student_Name);
                        body = System.IO.File.ReadAllText(Server.MapPath("~/Emails/Student_Inquiry.html"));
                        body = body.Replace("{{Course_Name}}", objLead.Course_Name);
                        body = body.Replace("{{University_Name}}", objLead.University_Name);
                        body = body.Replace("{{Student_Name}}", objLead.Student_Name);
                        body = body.Replace("{{Student_Email}}", objLead.Student_EmailId);
                        body = body.Replace("{{Student_Phone}}", objLead.Student_Mobile);
                        body = body.Replace("{{Course_Name}}", objLead.Course_Name);
                        body = body.Replace("{{Location}}", objLead.Student_isAustraliaResident ? (objLead.Student_Zipcode + "," + objLead.Student_State) : objLead.Student_Country);
                        body = body.Replace("{{Course_Name}}", objLead.Course_Name);

                        Common.SendEmail(objLead.University_Email, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]), subject, body);
                        bool Result = objLeadOperations.UpdateLeadAfterEmail(item.Id, "P");
                    }
                    /*End -Send Mail to Premium Lead*/


                    /*Start - Send Mail to Normal Lead*/
                    List<Lead> lstNormalLead = objLeadOperations.GetAllNormalLeadsByIdForSendingMail(objRegistration.objStudentRegistration.Id);

                    foreach (var items in lstNormalLead)
                    {
                        Lead objLead = objLeadOperations.GetAllDetails(items.Id, "N").FirstOrDefault();
                        subject = Convert.ToString(ConfigurationManager.AppSettings["Student_Inquiry"]);
                        subject = subject.Replace("{{Course_Name}}", objLead.Course_Name);
                        subject = subject.Replace("{{Student_Name}}", objLead.Student_Name);
                        body = System.IO.File.ReadAllText(Server.MapPath("~/Emails/Student_Inquiry.html"));
                        body = body.Replace("{{Course_Name}}", objLead.Course_Name);
                        body = body.Replace("{{University_Name}}", objLead.University_Name);
                        body = body.Replace("{{Student_Name}}", objLead.Student_Name);
                        body = body.Replace("{{Student_Email}}", objLead.Student_EmailId);
                        body = body.Replace("{{Student_Phone}}", objLead.Student_Mobile);
                        body = body.Replace("{{Course_Name}}", objLead.Course_Name);
                        body = body.Replace("{{Location}}", objLead.Student_isAustraliaResident ? (objLead.Student_Zipcode + "," + objLead.Student_State) : objLead.Student_Country);
                        body = body.Replace("{{Course_Name}}", objLead.Course_Name);

                        Common.SendEmail(objLead.University_Email, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]), subject, body);
                        bool Result = objLeadOperations.UpdateLeadAfterEmail(items.Id, "N");
                    }

                    /*End - Send Mail to Normal Lead*/

                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ConnectToUnivarsity", "CoursesController");
            }

            return RedirectToAction("CourseDetails", new { CourseId = Convert.ToInt32(Session["CourseId"]) });
        }

        public JsonResult GetCategories()
        {
            return Json(new { message = Common.getcategory(), selected = TempData["SelectedCategoryId"] }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCategoryTypes()
        {
            return Json(new { message = Common.getcategorytype() }, JsonRequestBehavior.AllowGet);
        }

    }
}