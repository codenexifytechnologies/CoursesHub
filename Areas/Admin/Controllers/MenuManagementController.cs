using CoursesHub.Areas.Admin.Filter;
using CoursesHub.Areas.Admin.Models;
using CoursesHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CoursesHub.Areas.Admin.Controllers
{
    [CheckLogin]
    public class MenuManagementController : Controller
    {
        MenuManagementOperations objMenuManagementOperations = new MenuManagementOperations();
        DashboardOperation objDashboardOperation = new DashboardOperation();
        public ActionResult MenuManagement()
        {
            try
            {
                if (Session["AdminUser"] != null)
                {
                    MenuManagement oMenuManagement = new MenuManagement();
                    oMenuManagement.lstEmp = objMenuManagementOperations.EmpData();
                    ViewBag.UserData = new SelectList(oMenuManagement.lstEmp, "id", "Name");
                    oMenuManagement.lstMenu = objMenuManagementOperations.MenuManagement();
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "MenuManagement", "MenuManagementController");
            }
            return View();
        }

        public ActionResult BindEmpMenu(int id)
        {
            MenuManagement oMenuManagement = new MenuManagement();
            try
            {
                if (Session["AdminUser"] != null)
                {
                    oMenuManagement.lstEmp = objMenuManagementOperations.EmpData();
                    ViewBag.UserData = new SelectList(oMenuManagement.lstEmp, "id", "Name");
                    oMenuManagement.lstMenu = objMenuManagementOperations.MenuManagement();
                    oMenuManagement.lstUserMenuMapping = objMenuManagementOperations.UserMenuMapping(id);
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "BindEmpMenu", "MenuManagementController");
            }
            return Json(oMenuManagement, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMenuRole(List<CheckboxData> lst, int EmpId)
        {

            try
            {
                if (Session["AdminUser"] != null)
                {
                    MenuManagement oMenuManagement = new MenuManagement();
                    oMenuManagement.lstEmp = objMenuManagementOperations.EmpData();
                    ViewBag.UserData = new SelectList(oMenuManagement.lstEmp, "id", "Name");
                    oMenuManagement.lstUserMenuMapping = objMenuManagementOperations.UserMenuMapping(EmpId);
                    var List = oMenuManagement.lstUserMenuMapping.Select(x => x.MenuId).ToList();
                    List<string> selectedIds = lst.Where(x => x.Checked == true).Select(x => x.ID).ToList();
                    string commaSeparatedIds = string.Join(",", selectedIds);

                    TempData["AddMenuSuccessMsg"] = objMenuManagementOperations.SaveUserMenuRole(commaSeparatedIds, EmpId);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "SaveMenuRole", "MenuManagementController");
            }
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult GetEmpMenuRole()
        {
            MenuManagement oMenuManagement = new MenuManagement();
            try
            {
                if (Session["AdminUser"] != null)
                {

                    TempData["controller"] = Request.RequestContext.RouteData.Values["Controller"].ToString();
                    TempData["action"] = Request.RequestContext.RouteData.Values["action"].ToString();
                    if (Session["lstUserMenuMapping"] != null)
                    {
                        oMenuManagement.lstUserMenuMapping = (List<CheckedMenu>)Session["lstUserMenuMapping"];
                    }
                    else
                    {
                        oMenuManagement.lstUserMenuMapping = objMenuManagementOperations.UserMenuMapping(Convert.ToInt32(Session["AdminUserId"]));
                        Session["lstUserMenuMapping"] = oMenuManagement.lstUserMenuMapping;
                    }
                    ViewBag.CourseCount = objDashboardOperation.PendingCoursesCount();
                    ViewBag.UniversityCount = objDashboardOperation.PendingUniversityCount();
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetEmpMenuRole", "MenuManagementController");
            }
            return PartialView(oMenuManagement);
        }
    }
}