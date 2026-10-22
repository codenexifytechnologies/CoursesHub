using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CoursesHub.Areas.Admin.Models;
using CoursesHub.Models;
using CoursesHub.Areas.Admin.SessionVariable;
using Newtonsoft.Json;

namespace CoursesHub.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: Admin/AdminLogin
        AdminLoginOperation objAdminLoginOperation = new AdminLoginOperation();

        #region UserLogin

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminLogin objAdminLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User objUser = objAdminLoginOperation.Login(objAdminLogin);
                    Session["AdminUser"] = objUser;
                    Session["AdminUserId"] = objUser.userId;

                    if (objUser != null && objUser.Message == null)
                    {
                        if (objUser.RoleName.ToLower() == "admin")
                        {
                            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                        }
                        else
                        {
                            TempData["Message"] = "Enter Valid Credentials.";
                        }
                    }
                    else
                    {
                        TempData["Message"] = "User not available in system, Please contact admin and create your account.";
                    }
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Post Login", "AdminLoginController");
            }
            return View(objAdminLogin);
        }

        #endregion

        #region User Logout

        public ActionResult LogOff()
        {
            if (TempData["Message"] != null)
            {
                TempData["Message"] = TempData["Message"];
            }

            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login", "AdminLogin", new { area = "Admin" });
        }

        #endregion

        #region UserRegistration

        public ActionResult UserRegistration()
        {
            ViewBag.Role = new SelectList(objAdminLoginOperation.getRole(), "RoleId", "RoleName");
            return View();
        }

        [HttpPost]
        public ActionResult UserRegistration(User objUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["Message"] = objAdminLoginOperation.InsertUpdateClient(objUser, "I");
                    ViewBag.Role = new SelectList(objAdminLoginOperation.getRole(), "RoleId", "RoleName");
                    ModelState.Clear();
                }
                else
                {
                    ModelState.AddModelError("Error", "Please fiil all fileds");
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "UserRegistration Post", "AdminLoginController");
            }
            return View();
        }

        public ActionResult UserRegistrationList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetUserRegistrationDetails()
        {
            List<User> objUser = new List<User>();
            try
            {
                objUser = objAdminLoginOperation.GetUserRegistrationDetails();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Get GetUserRegistrationDetails", "AdminLoginController");
            }
            return Json(new { datas = objUser }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditLoginDetails(int UserId)
        {
            User objUser = new User();

            try
            {
                objUser = objAdminLoginOperation.GetUserRegistrationDetailsByID(UserId).FirstOrDefault();
                ViewBag.Role = new SelectList(objAdminLoginOperation.getRole(), "RoleId", "RoleName", objUser.RoleID);
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Get EditLoginDetails", "AdminLoginController");
            }
            return View("EditLoginDetails", objUser);
        }

        [HttpPost]
        public ActionResult EditLoginDetails(User objUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["Message"] = objAdminLoginOperation.InsertUpdateClient(objUser, "U");
                }
                else
                {
                    ModelState.AddModelError("Error", "Please fiil all fileds");
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "EditLoginDetails Post", "AdminLoginController");
            }
            return RedirectToAction("EditLoginDetails", new { @UserId = objUser.userId });
        }

        [HttpGet]
        public ActionResult ActionLoginDetails(int Id, string Operation)
        {
            try
            {
                TempData["Message"] = objAdminLoginOperation.UserActionRegistartion(Id, Operation);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Get DeleteLoginDetails", "AdminLoginController");
            }

            return RedirectToAction("UserRegistrationList");
        }

        #endregion

        #region ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(AdminLogin objLogin)
        {
            try
            {
                string strName = string.Empty;
                if (!string.IsNullOrEmpty(objLogin.EmailId))
                {
                    string fpwd = Guid.NewGuid().ToString().Substring(0, 6);
                    objLogin.Password = fpwd;
                    var user_email = objAdminLoginOperation.CheckUser(objLogin);

                    if (!string.IsNullOrEmpty(user_email))
                    {
                        if (user_email.Contains(","))
                        {
                            var parts = user_email.Split(',');
                            if (parts.Length > 1)
                            {
                                user_email = parts[0];
                                strName = parts[1];
                            }
                        }

                        if (user_email.ToLower() == "valid")
                        {
                            string EmailFrom = Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]);
                            string EmailSubject = Convert.ToString(ConfigurationManager.AppSettings["Forgot_Password"]);
                            string Body = System.IO.File.ReadAllText(Server.MapPath("~/Emails/Forgot_Password.html"));
                            Body = Body.Replace("{{admin_name}}", strName);
                            Body = Body.Replace("{{Password}}", fpwd);
                            Body = Body.Replace("{{reset_link}}", Convert.ToString(ConfigurationManager.AppSettings["AdminLoginUrl"]));
                            Common.SendEmail(objLogin.EmailId, EmailFrom, EmailSubject, Body);
                            TempData["ForgotSuccessMessage"] = "Password has been sent to your registered email address  " + objLogin.EmailId;
                            ModelState.Clear();
                            return View("Login");
                        }
                        else
                        {
                            TempData["ForgotErrorMessage"] = "Invalid EmailId";
                        }
                    }
                    else
                    {
                        TempData["ForgotErrorMessage"] = "Error Occured";
                    }
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Post ForgotPassword", "AdminLoginController");
            }
            return View("ForgotPassword");
        }

        #endregion

        #region Change Password

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword objChangePassword)
        {
            try
            {
                if (Session["AdminUser"] != null)
                {

                    User objUser = (User)Session["AdminUser"];

                    if (ModelState.IsValid)
                    {
                        TempData["ChangePasswordSuccessMessage"] = objAdminLoginOperation.ChangePassword(objChangePassword, Convert.ToInt32(Session["AdminUserId"]), objUser.EmailId);
                        ModelState.Clear();
                        return View("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Please fiil all fileds");
                    }

                }
                else
                {
                    TempData["Message"] = "Your session has expired for security reasons. Please log in again to continue.";
                    return RedirectToAction("LogOff", "AdminLogin", new { area = "Admin" });
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Post ChangePassword", "AdminLoginController");
            }
            return View();
        }

        #endregion
    }
}