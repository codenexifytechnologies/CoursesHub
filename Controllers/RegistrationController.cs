using CoursesHub.Models;
using CoursesHub.Operation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Controllers
{
    public class RegistrationController : BaseController
    {
        RegistrationOperation objRegistrationOperation = new RegistrationOperation();
        // GET: Registration

        // Upload rules for the provider signature / logo (the form only accepts these image types).
        private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png" };
        private const int MaxImageBytes = 2 * 1024 * 1024; // 2 MB each, keeps both files under the 4 MB request limit

        // /Registration                        -> choose Student or Training Provider
        // /Registration?type=Student           -> student form
        // /Registration?type=TrainingProvider  -> training provider wizard
        [Route("Registration")]
        public ActionResult Registration(string type)
        {
            Registration objRegistration = new Registration { LoginType = LoginTypes.Normalize(type) };
            try
            {
                bind();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Registration Get", "RegistrationController");
            }
            return View(objRegistration);
        }

        [Route("Registration")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(Registration objRegistration, HttpPostedFileBase Sign, HttpPostedFileBase Logo, string submitButton, string confirmation)
        {
            objRegistration = objRegistration ?? new Registration();

            // The registration type is decided on the server: the submit button value must be a known type
            // AND the posted data must belong to that type only (student data can never be saved as a provider and vice versa).
            string registrationType = LoginTypes.Normalize(submitButton);
            objRegistration.LoginType = registrationType;
            try
            {
                bind();

                bool isStudent = registrationType == LoginTypes.Student
                    && objRegistration.objStudentRegistration != null && objRegistration.objUniversityRegistration == null;
                bool isProvider = registrationType == LoginTypes.University
                    && objRegistration.objUniversityRegistration != null && objRegistration.objStudentRegistration == null;

                if (!isStudent && !isProvider)
                {
                    objRegistration.LoginType = null;
                    ViewBag.ErrorMessage = "Invalid registration request. Please choose Student or Training Provider and try again.";
                    return View(objRegistration);
                }

                if (isStudent)
                {
                    ValidateStudent(objRegistration.objStudentRegistration);
                    if (!ModelState.IsValid)
                    {
                        ViewBag.ErrorMessage = "Please correct the highlighted fields and try again.";
                        return View(objRegistration);
                    }

                    string msg = objRegistrationOperation.AddStudent(objRegistration.objStudentRegistration);
                    if (!string.IsNullOrEmpty(msg) && msg.Contains("Success"))
                    {
                        // Post/Redirect/Get: a page refresh cannot submit the same registration again.
                        TempData["Message"] = msg;
                        TempData["RegistrationSuccess"] = LoginTypes.Student;
                        return RedirectToAction("Registration", new { type = "Student" });
                    }
                    ViewBag.ErrorMessage = string.IsNullOrEmpty(msg) ? "We could not complete your registration. Please try again." : msg;
                    return View(objRegistration);
                }
                else
                {
                    ValidateProvider(objRegistration.objUniversityRegistration, Sign, Logo, confirmation);
                    if (!ModelState.IsValid)
                    {
                        ViewBag.ErrorMessage = "Please correct the highlighted fields and try again.";
                        return View(objRegistration);
                    }

                    string msg = objRegistrationOperation.AddUniversity(objRegistration.objUniversityRegistration, Sign, Logo);
                    if (!string.IsNullOrEmpty(msg) && msg.Contains("Added"))
                    {
                        TempData["Message"] = msg;
                        TempData["RegistrationSuccess"] = LoginTypes.University;
                        return RedirectToAction("Registration", new { type = "TrainingProvider" });
                    }
                    ViewBag.ErrorMessage = string.IsNullOrEmpty(msg) ? "We could not complete your registration. Please try again." : msg;
                    return View(objRegistration);
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Registration Post", "RegistrationController");
                ViewBag.ErrorMessage = "Something went wrong while saving your registration. Please try again.";
            }
            return View(objRegistration);
        }

        #region Server-side validation (mirrors the rules enforced by the registration form)
        private void ValidateStudent(StudentRegistration s)
        {
            const string p = "objStudentRegistration.";

            // Same rule as the form's "strongPassword" check.
            if (!string.IsNullOrEmpty(s.Password) &&
                !Regex.IsMatch(s.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{6,}$"))
            {
                ModelState.AddModelError(p + "Password", "Password must contain at least 1 uppercase, 1 lowercase, 1 number, and 1 special character.");
            }

            if (s.isAustraliaResident)
            {
                if (string.IsNullOrWhiteSpace(s.Zipcode))
                    ModelState.AddModelError(p + "Zipcode", "Post Code is required for Australian residents");
                if (string.IsNullOrWhiteSpace(s.State))
                    ModelState.AddModelError(p + "State", "Please Select State");
            }
            else if (string.IsNullOrWhiteSpace(s.Country))
            {
                ModelState.AddModelError(p + "Country", "Country is required for non-Australian residents");
            }
        }

        private void ValidateProvider(UniversityRegistration u, HttpPostedFileBase Sign, HttpPostedFileBase Logo, string confirmation)
        {
            const string p = "objUniversityRegistration.";

            // The form has always required a 5-digit RTO ID; the model attribute (6 digits) was never enforced,
            // so keep the rule users actually register with. The ID is also used in the upload folder path.
            ModelState.Remove(p + "RTO_ID");
            if (string.IsNullOrWhiteSpace(u.RTO_ID))
                ModelState.AddModelError(p + "RTO_ID", "Please enter RTO ID");
            else if (!Regex.IsMatch(u.RTO_ID, @"^\d{5}$"))
                ModelState.AddModelError(p + "RTO_ID", "Please enter exactly 5 numeric digits (e.g. 12345)");

            if (!string.IsNullOrEmpty(u.General_Contact_Email) && !IsEmail(u.General_Contact_Email))
                ModelState.AddModelError(p + "General_Contact_Email", "Please enter a valid General Contact Email");
            if (!string.IsNullOrEmpty(u.Email) && !IsEmail(u.Email))
                ModelState.AddModelError(p + "Email", "Please enter a valid Email");
            if (!string.IsNullOrEmpty(u.Contact_Number) && !Regex.IsMatch(u.Contact_Number, @"^\d+$"))
                ModelState.AddModelError(p + "Contact_Number", "Please enter a valid Contact Number (digits only)");
            if (!string.IsNullOrEmpty(u.Direct_Phone) && !Regex.IsMatch(u.Direct_Phone, @"^\d+$"))
                ModelState.AddModelError(p + "Direct_Phone", "Please enter a valid Direct Phone (digits only)");
            if (!string.IsNullOrEmpty(u.Website) &&
                !Regex.IsMatch(u.Website, @"^(https?:\/\/)?(www\.)?([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}(\/.*)?$", RegexOptions.IgnoreCase))
                ModelState.AddModelError(p + "Website", "Please enter a valid Website URL (e.g. example.com or https://example.com)");

            if (string.IsNullOrWhiteSpace(u.Zipcode))
                ModelState.AddModelError(p + "Zipcode", "Please select Post Code");
            if (string.IsNullOrWhiteSpace(u.State))
                ModelState.AddModelError(p + "State", "Please select State");
            if (u.Relevant_Courses == null || !u.Relevant_Courses.Any(c => !string.IsNullOrWhiteSpace(c)))
                ModelState.AddModelError(p + "Relevant_Courses", "Please select at least one course");
            if (string.IsNullOrEmpty(confirmation))
                ModelState.AddModelError("confirmation", "Please confirm the compliance statements");

            ValidateImage(Sign, "Sign", "Authorised Signature");
            ValidateImage(Logo, "Logo", "Logo");
        }

        private void ValidateImage(HttpPostedFileBase file, string key, string label)
        {
            if (file == null || file.ContentLength == 0)
                return; // uploads are optional, as before

            string ext = (Path.GetExtension(file.FileName) ?? string.Empty).ToLowerInvariant();
            if (!AllowedImageExtensions.Contains(ext) || !(file.ContentType ?? string.Empty).StartsWith("image/", StringComparison.OrdinalIgnoreCase))
                ModelState.AddModelError(key, label + " must be a JPG or PNG image.");
            else if (file.ContentLength > MaxImageBytes)
                ModelState.AddModelError(key, label + " must be 2 MB or smaller.");
        }

        private static bool IsEmail(string value)
        {
            return Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
        #endregion

        public void bind()
        {
            //ViewBag.CoursesList = new List<SelectListItem>
            //{
            //    new SelectListItem { Value = "AgedCare", Text = "Aged Care" },
            //    new SelectListItem { Value = "Disability", Text = "Disability" },
            //    new SelectListItem { Value = "Nursing", Text = "Nursing" },
            //    new SelectListItem { Value = "CommunityServices", Text = "Community Services" },
            //    new SelectListItem { Value = "BusinessIT", Text = "Business / IT" },
            //    new SelectListItem { Value = "Other", Text = "Other" }
            //};

            ViewBag.CoursesList = Common.getcategory();

            // Country and state lists come from external APIs and rarely change: cache them for a day
            // instead of calling both services on every page load. Failed calls are never cached.
            ViewBag.CountryList = GetCachedLookup("Registration:CountryList", () =>
            {
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
                    return list.OrderBy(x => x.GetType().GetProperty("Text").GetValue(x, null)).ToList();
                }
            });

            ViewBag.StateList = GetCachedLookup("Registration:StateList", () =>
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0"); // Required

                    var jsonString = client.GetStringAsync("https://v0.postcodeapi.com.au/states/").Result;

                    JArray states = JArray.Parse(jsonString);

                    return states.Select(s => new
                    {
                        Value = s["name"].ToString(),
                        Text = s["name"].ToString()
                    }).OrderBy(x => x.Text).ToList<object>();
                }
            });
        }

        private static List<object> GetCachedLookup(string cacheKey, Func<List<object>> load)
        {
            var cached = HttpRuntime.Cache[cacheKey] as List<object>;
            if (cached != null)
            {
                return cached;
            }

            List<object> list = load(); // throws as before if the external service fails, so nothing is cached
            HttpRuntime.Cache.Insert(cacheKey, list, null, DateTime.UtcNow.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
            return list;
        }

        public async Task<ActionResult> SearchAusZip(string query)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = $"http://v0.postcodeapi.com.au/suburbs/{query}.json";
                    var response = await client.GetStringAsync(url);

                    var json = JArray.Parse(response);

                    var list = json.Select(item => new
                    {
                        Postcode = item["postcode"].ToString(),
                        Suburb = item["name"].ToString(),
                        State = item["state"].ToString()
                    }).ToList();

                    return Json(list, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new List<object>(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}