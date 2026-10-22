using CoursesHub.Areas.Admin.Filter;
using CoursesHub.Models;
using CoursesHub.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Areas.Admin.Controllers
{
    [CheckLogin]
    public class StudentController : Controller
    {
        LoginOperation objLoginOperation = new LoginOperation();
        RegistrationOperation objRegistrationOperation = new RegistrationOperation();
        // GET: Admin/Student
        public ActionResult ViewStudent()
        {
            Registration objRegistration = new Registration();
            objRegistration.objStudentRegistration = new StudentRegistration();
            List<StudentRegistration> objListStudentRegistration = new List<StudentRegistration>();
            try
            {
                if (Session["AdminUser"] != null)
                {
                    objListStudentRegistration = objRegistrationOperation.GetStudent(objRegistration.objStudentRegistration);
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ViewStudent", "StudentController");
            }
            return View(objListStudentRegistration);
        }

        public ActionResult ViewStudentDetails(int Id)
        {
            Registration objRegistration = new Registration();
            try
            {
                if (Session["AdminUser"] != null)
                {
                    objRegistration = objLoginOperation.GetProfile("Student", Id);
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ViewStudentDetails Get", "StudentController");
            }
            return View(objRegistration.objStudentRegistration);
        }

        [HttpPost]
        public ActionResult ViewStudentDetails(StudentRegistration objStudentRegistration)
        {
            try
            {
                if (Session["AdminUser"] != null)
                {
                    TempData["EditStudentDetailsMsg"] = objRegistrationOperation.ChangeStatusStudent(objStudentRegistration);
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ViewStudentDetails Post", "StudentController");
            }
            return RedirectToAction("ViewStudentDetails", new { Id = objStudentRegistration.Id });
        }

        public ActionResult DeleteStudent(int Id)
        {
            try
            {
                if (Session["AdminUser"] != null)
                {
                    TempData["EditStudentDetailsMsg"] = objRegistrationOperation.DeleteStudent(Id);
                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "DeleteStudent", "StudentController");
            }
            return RedirectToAction("ViewStudent");
        }
    }
}