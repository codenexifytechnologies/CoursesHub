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
    public class DashboardController : Controller
    {
        DashboardOperation objDashboardOperation = new DashboardOperation();
        UniversityOperation objUniversityOperation = new UniversityOperation();

        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            if (Session["AdminUser"] != null)
            {
                Registration objRegistration = new Registration();
                objRegistration.objUniversityRegistration = new UniversityRegistration();
                TempData["controller"] = Request.RequestContext.RouteData.Values["Controller"].ToString();
                TempData["action"] = Request.RequestContext.RouteData.Values["action"].ToString();
                DashboardCount objDashboardCount = objDashboardOperation.GetCount();
                List<UniversityRegistration> objListUniversityRegistration = new List<UniversityRegistration>();
                objListUniversityRegistration = objUniversityOperation.GetAllUniversity(objRegistration.objUniversityRegistration);

                ViewBag.UniversityList = objListUniversityRegistration.Where(y => y.IsActive == true)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Legal_Name_Organisation
                    }).ToList();

                ViewBag.LeadTypeList = objListUniversityRegistration.Where(y => y.IsActive == true)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Legal_Name_Organisation
                    }).ToList();
                return View(objDashboardCount);
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
            }
        }


        public ActionResult GetBarChartData(int? UniversityId)
        {
            if (Session["AdminUser"] != null)
            {
                var data = objDashboardOperation.Monthwise_Lead(UniversityId);

                return Json(new
                {
                    Months = data.Select(x => x.MonthName).ToList(),
                    PremiumLead = data.Select(x => x.PremiumLead).ToList(),
                    NormalLead = data.Select(x => x.NormalLead).ToList()
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
            }
        }

        public ActionResult GetDonutData()
        {
            if (Session["AdminUser"] != null)
            {
                var data = objDashboardOperation.DonutData();

                return Json(new
                {
                    country = data.Where(x => x.isCountry == 1).Select(x => x.country).ToArray(),
                    countrystudentcount = data.Where(x => x.isCountry == 1).Select(x => x.count).ToArray(),
                    CColors = data.Where(x => x.isCountry == 1).Select(x => x.ColorCode).ToArray(),
                    state = data.Where(x => x.isCountry == 0).Select(x => x.country).ToArray(),
                    statestudentcount = data.Where(x => x.isCountry == 0).Select(x => x.count).ToArray(),
                    SColors = data.Where(x => x.isCountry == 0).Select(x => x.ColorCode).ToArray(),
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
            }
        }

        public ActionResult GetPieData()
        {
            if (Session["AdminUser"] != null)
            {
                var data = objDashboardOperation.PieData();

                return Json(new
                {
                    CategoryType = data.Select(x => x.CategoryType).ToArray(),
                    CategoryTypeCount = data.Select(x => x.count).ToArray(),
                    CColors = data.Select(x => x.ColorCode).ToArray(),
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
            }
        }

    }
}