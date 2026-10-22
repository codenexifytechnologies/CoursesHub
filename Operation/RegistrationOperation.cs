using CoursesHub.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;

namespace CoursesHub.Operation
{
    public class RegistrationOperation
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

        public string AddStudent(StudentRegistration objStudentRegistration)
        {
            string msg = string.Empty;
            string Zipcode = string.Empty;
            string State = string.Empty;
            if (objStudentRegistration.Zipcode != null && objStudentRegistration.Zipcode.Contains('-'))
            {
                Zipcode = objStudentRegistration.Zipcode.Split('-')[0];
                State = objStudentRegistration.Zipcode.Split('-')[1];
            }
            try
            {
                var para = new DynamicParameters();
                para.Add("@FirstName", objStudentRegistration.FirstName);
                para.Add("@MiddleName", objStudentRegistration.MiddleName);
                para.Add("@LastName", objStudentRegistration.LastName);
                para.Add("@EmailId", objStudentRegistration.EmailId);
                para.Add("@Mobile", objStudentRegistration.Mobile);
                para.Add("@Password", objStudentRegistration.Password);
                para.Add("@isAustraliaResident", objStudentRegistration.isAustraliaResident);
                para.Add("@Zipcode", objStudentRegistration.isAustraliaResident ? Zipcode : "");
                para.Add("@Area", objStudentRegistration.isAustraliaResident ? State : "");
                para.Add("@State", objStudentRegistration.isAustraliaResident ? objStudentRegistration.State : "");
                para.Add("@Country", !objStudentRegistration.isAustraliaResident ? objStudentRegistration.Country : "");

                msg = con.Query<string>("usp_add_student", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                if (msg.Contains("Success"))
                {
                    string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Emails/Registration_Verification.html"));
                    body = body.Replace("{{student_name}}", objStudentRegistration.FirstName);
                    body = body.Replace("{{verification_link}}", Convert.ToString(ConfigurationManager.AppSettings["BaseURL"].ToString() + "StudentVerify/Verify?link=" + msg.Split(',')[1] + "&emailid=" + objStudentRegistration.EmailId));
                    Common.SendEmail(objStudentRegistration.EmailId, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"].ToString()), ConfigurationManager.AppSettings["Registration_Verification"].ToString(), body);
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "AddStudent", "RegistrationOperation");
                throw ex;
            }
            return msg.Split(',')[0];
        }

        public string EditStudent(StudentRegistration objStudentRegistration)
        {
            string msg = string.Empty;
            try
            {
                var para = new DynamicParameters();
                para.Add("@Id", objStudentRegistration.Id);
                para.Add("@FirstName", objStudentRegistration.FirstName);
                para.Add("@MiddleName", objStudentRegistration.MiddleName);
                para.Add("@LastName", objStudentRegistration.LastName);
                para.Add("@EmailId", objStudentRegistration.EmailId);
                para.Add("@Mobile", objStudentRegistration.Mobile);
                para.Add("@Password", objStudentRegistration.Password);
                para.Add("@isAustraliaResident", objStudentRegistration.isAustraliaResident);
                para.Add("@Zipcode", objStudentRegistration.isAustraliaResident ? objStudentRegistration.Zipcode.Split('-')[0] : "");
                para.Add("@State", objStudentRegistration.isAustraliaResident ? objStudentRegistration.Zipcode.Split('-')[1] : "");
                para.Add("@Country", !objStudentRegistration.isAustraliaResident ? objStudentRegistration.Country : "");
                msg = con.Query<string>("usp_edit_student", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "EditStudent", "RegistrationOperation");
                throw ex;
            }
            return msg;
        }

        public List<StudentRegistration> GetStudent(StudentRegistration objStudentRegistration)
        {
            List<StudentRegistration> objListStudentRegistration = new List<StudentRegistration>();
            try
            {
                var para = new DynamicParameters();
                para.Add("@Id", objStudentRegistration.Id);
                objListStudentRegistration = con.Query<StudentRegistration>("usp_get_student", para, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetStudent", "RegistrationOperation");
                throw ex;
            }
            return objListStudentRegistration;
        }

        public string ChangeStatusStudent(StudentRegistration objStudentRegistration)
        {
            string msg = string.Empty;
            string strName = string.Empty;
            string strEmail = string.Empty;
            try
            {
                var para = new DynamicParameters();
                para.Add("@Id", objStudentRegistration.Id);
                para.Add("@IsActive", objStudentRegistration.IsActive);
                msg = con.Query<string>("usp_change_status_student", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();

                if (msg.Contains(","))
                {
                    var parts = msg.Split(',');
                    if (parts.Length > 1)
                    {
                        msg = parts[0];
                        strName = parts[1];
                        strEmail = parts[2];
                    }
                }

                if (msg == "Actived")
                {
                    string subject = Convert.ToString(ConfigurationManager.AppSettings["Student_RegistrationApproval"]);
                    subject = subject.Replace("{{user_name}}", strName);
                    string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Emails/StudentAccountApproval.html"));
                    body = body.Replace("{{user_name}}", strName);
                    body = body.Replace("{{portal_login_link}}", Convert.ToString(ConfigurationManager.AppSettings["LoginUrl"]));
                    Common.SendEmail(strEmail, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]), subject, body);
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ChangeStatusStudent", "RegistrationOperation");
                throw ex;
            }
            return msg;
        }

        public string AddUniversity(UniversityRegistration objUniversityRegistration, HttpPostedFileBase Sign, HttpPostedFileBase Logo)
        {
            string msg = string.Empty;
            string Sign_guid = Convert.ToString(Guid.NewGuid());
            string Logo_guid = Convert.ToString(Guid.NewGuid());
            string SServerMapPath = string.Empty;
            string LServerMapPath = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(objUniversityRegistration.RTO_ID))
                {
                    string rtoMsg = CheckRTOID(objUniversityRegistration.RTO_ID);
                    if ((rtoMsg.ToLower()) == "already exist")
                    {
                        return "This RTO ID has already been registered with our system. Please register with a new RTO ID.";
                    }
                }

                if (Sign != null && Sign.ContentLength > 0)
                {
                    string SignExtension = Path.GetExtension(Sign.FileName);
                    string SignPath = "Uploads/Sign/" + objUniversityRegistration.RTO_ID;
                    SServerMapPath = HttpContext.Current.Server.MapPath("~/" + SignPath);
                    if (!Directory.Exists(SServerMapPath))
                    {
                        Directory.CreateDirectory(SServerMapPath);
                    }
                    Sign_guid = "/" + Sign_guid + SignExtension;
                    SignPath = SignPath + Sign_guid;
                    objUniversityRegistration.Signature = ConfigurationManager.AppSettings["BaseURL"].ToString() + SignPath;
                    SServerMapPath = SServerMapPath + Sign_guid;
                }

                if (Logo != null && Logo.ContentLength > 0)
                {
                    string LogoExtension = Path.GetExtension(Logo.FileName);
                    string LogoPath = "Uploads/Logo/" + objUniversityRegistration.RTO_ID;
                    LServerMapPath = HttpContext.Current.Server.MapPath("~/" + LogoPath);
                    if (!Directory.Exists(LServerMapPath))
                    {
                        Directory.CreateDirectory(LServerMapPath);
                    }
                    Logo_guid = "/" + Logo_guid + LogoExtension;
                    LogoPath = LogoPath + Logo_guid;
                    objUniversityRegistration.Logo = ConfigurationManager.AppSettings["BaseURL"].ToString() + LogoPath;
                    LServerMapPath = LServerMapPath + Logo_guid;
                }
                var para = new DynamicParameters();
                para.Add("@RTO_ID", objUniversityRegistration.RTO_ID);
                para.Add("@RTO_Status", objUniversityRegistration.RTO_Status);
                para.Add("@CRICOS_Code", objUniversityRegistration.CRICOS_Code);
                para.Add("@Legal_Name_Organisation", objUniversityRegistration.Legal_Name_Organisation);
                para.Add("@Trading_Name", objUniversityRegistration.Trading_Name);
                para.Add("@ABN_ACN", objUniversityRegistration.ABN_ACN);
                para.Add("@Business_Address", objUniversityRegistration.Business_Address);
                para.Add("@Postal_Address", objUniversityRegistration.Postal_Address);
                para.Add("@Website", objUniversityRegistration.Website);
                para.Add("@Contact_Number", objUniversityRegistration.Contact_Number);
                para.Add("@General_Contact_Email", objUniversityRegistration.General_Contact_Email);
                para.Add("@Full_Name", objUniversityRegistration.Full_Name);
                para.Add("@Position", objUniversityRegistration.Position);
                para.Add("@Direct_Phone", objUniversityRegistration.Direct_Phone);
                para.Add("@Email", objUniversityRegistration.Email);
                para.Add("@Relevant_Courses", string.Join(",", objUniversityRegistration.Relevant_Courses));
                para.Add("@Signature", objUniversityRegistration.Signature);
                para.Add("@Logo", objUniversityRegistration.Logo);
                para.Add("@Password", objUniversityRegistration.Password);
                para.Add("@ZipCode", objUniversityRegistration.Zipcode);
                para.Add("@State", objUniversityRegistration.State);
                msg = con.Query<string>("usp_add_University", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                if (msg.Contains("Added"))
                {
                    if (!string.IsNullOrEmpty(SServerMapPath))
                        Sign.SaveAs(SServerMapPath);
                    if (!string.IsNullOrEmpty(LServerMapPath))
                        Logo.SaveAs(LServerMapPath);

                    string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Emails/University_Registration.html"));
                    body = body.Replace("{{university_name}}", objUniversityRegistration.Legal_Name_Organisation);
                    body = body.Replace("{{portal_login_link}}", Convert.ToString(ConfigurationManager.AppSettings["LoginUrl"].ToString()));
                    Common.SendEmail(objUniversityRegistration.General_Contact_Email, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"].ToString()), ConfigurationManager.AppSettings["University_Registration"].ToString(), body);
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "AddUniversity", "RegistrationOperation");
                throw ex;
            }
            return msg;
        }

        public string EditUniversity(UniversityRegistration objUniversityRegistration, HttpPostedFileBase Sign, HttpPostedFileBase Logo)
        {
            string msg = string.Empty;
            string SServerMapPath = string.Empty;
            string LServerMapPath = string.Empty;
            string Sign_guid = Convert.ToString(Guid.NewGuid());
            string Logo_guid = Convert.ToString(Guid.NewGuid());

            try
            {
                if (Sign != null && Sign.ContentLength > 0)
                {
                    string SignExtension = Path.GetExtension(Sign.FileName);
                    string SignPath =  "Uploads/Sign/" + objUniversityRegistration.RTO_ID;
                    SServerMapPath = HttpContext.Current.Server.MapPath("~/" + SignPath);
                    if (!Directory.Exists(SServerMapPath))
                    {
                        Directory.CreateDirectory(SServerMapPath);
                    }
                    Sign_guid = "/" + Sign_guid + SignExtension;
                    SignPath = SignPath + Sign_guid;
                    objUniversityRegistration.Signature = ConfigurationManager.AppSettings["BaseURL"].ToString() + SignPath;
                    SServerMapPath = SServerMapPath + Sign_guid;
                }

                if (Logo != null && Logo.ContentLength > 0)
                {
                    string LogoExtension = Path.GetExtension(Logo.FileName);
                    string LogoPath = "Uploads/Logo/" + objUniversityRegistration.RTO_ID;
                    LServerMapPath = HttpContext.Current.Server.MapPath("~/" + LogoPath);
                    if (!Directory.Exists(LServerMapPath))
                    {
                        Directory.CreateDirectory(LServerMapPath);
                    }
                    Logo_guid = "/" + Logo_guid + LogoExtension;
                    LogoPath = LogoPath + Logo_guid;
                    objUniversityRegistration.Logo = ConfigurationManager.AppSettings["BaseURL"].ToString() + LogoPath;
                    LServerMapPath = LServerMapPath + Logo_guid;
                }
                var para = new DynamicParameters();
                para.Add("@Id", objUniversityRegistration.Id);
                para.Add("@RTO_ID", objUniversityRegistration.RTO_ID);
                para.Add("@RTO_Status", objUniversityRegistration.RTO_Status);
                para.Add("@CRICOS_Code", objUniversityRegistration.CRICOS_Code);
                para.Add("@Legal_Name_Organisation", objUniversityRegistration.Legal_Name_Organisation);
                para.Add("@Trading_Name", objUniversityRegistration.Trading_Name);
                para.Add("@ABN_ACN", objUniversityRegistration.ABN_ACN);
                para.Add("@Business_Address", objUniversityRegistration.Business_Address);
                para.Add("@Postal_Address", objUniversityRegistration.Postal_Address);
                para.Add("@Website", objUniversityRegistration.Website);
                para.Add("@Contact_Number", objUniversityRegistration.Contact_Number);
                para.Add("@General_Contact_Email", objUniversityRegistration.General_Contact_Email);
                para.Add("@Full_Name", objUniversityRegistration.Full_Name);
                para.Add("@Position", objUniversityRegistration.Position);
                para.Add("@Direct_Phone", objUniversityRegistration.Direct_Phone);
                para.Add("@Email", objUniversityRegistration.Email);
                para.Add("@Relevant_Courses", objUniversityRegistration.Relevant_Courses);
                para.Add("@Signature", objUniversityRegistration.Signature);
                para.Add("@Logo", objUniversityRegistration.Logo);
                para.Add("@Password", objUniversityRegistration.Password);
                msg = con.Query<string>("usp_edit_University", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                if (msg.Contains("Updated"))
                {
                    if (!string.IsNullOrEmpty(SServerMapPath))
                        Sign.SaveAs(SServerMapPath);
                    if (!string.IsNullOrEmpty(LServerMapPath))
                        Logo.SaveAs(LServerMapPath);
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "EditUniversity", "RegistrationOperation");
                throw ex;
            }
            return msg;
        }

        public string ChangeStatusUniversity(UniversityRegistration objUniversityRegistration, int IsDelete)
        {
            string msg = string.Empty;
            string strName = string.Empty;
            string strEmail = string.Empty;
            string strPwd = string.Empty;
            string isReject = string.Empty;
            try
            {
                var para = new DynamicParameters();
                para.Add("@Id", objUniversityRegistration.Id);
                para.Add("@IsActive", objUniversityRegistration.IsActive);
                para.Add("@IsReject", objUniversityRegistration.IsReject);
                para.Add("@isUniversityActive", objUniversityRegistration.isUniversityActive);
                para.Add("@IsDelete", IsDelete);
                para.Add("@RejectionReason", objUniversityRegistration.RejectionReason);
                msg = con.Query<string>("usp_change_status_University_ver1", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();

                if (!string.IsNullOrEmpty(msg) && msg.Contains(","))
                {
                    var parts = msg.Split(',');
                    if (parts.Length > 1)
                    {
                        strName = parts[0];
                        strEmail = parts[1];
                        strPwd = parts[2];
                    }
                }
                if (IsDelete == 1)
                {
                    msg = "University Archived";
                    string subject = Convert.ToString(ConfigurationManager.AppSettings["University_Terminate.html"]);
                    string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Emails/University_Terminate.html"));
                    body = body.Replace("{{university_name}}", strName);
                    Common.SendEmail(strEmail, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]), subject, body);
                }
                else if (objUniversityRegistration.isUniversityActive != null)
                {
                    if (objUniversityRegistration.isUniversityActive == true)
                    {
                        msg = "University Acivated";
                    }
                    else if (objUniversityRegistration.isUniversityActive == false)
                    {
                        msg = "University De-Acivated";
                    }
                }
                else if (objUniversityRegistration.IsReject == true)
                {
                    msg = "University Rejected";
                    string subject = Convert.ToString(ConfigurationManager.AppSettings["University_Registration_Rejection"]);
                    subject = subject.Replace("{{university_name}}", strName);
                    string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Emails/University_Registration_Rejection.html"));
                    body = body.Replace("{{university_name}}", strName);
                    body = body.Replace("{{reason}}", objUniversityRegistration.RejectionReason);
                    Common.SendEmail(strEmail, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]), subject, body);
                }
                else if (objUniversityRegistration.IsActive)
                {
                    msg = "University Varified";
                    string subject = Convert.ToString(ConfigurationManager.AppSettings["University_Registration_Approval"]);
                    subject = subject.Replace("{{university_name}}", strName);
                    string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Emails/University_Registration_Approval.html"));
                    body = body.Replace("{{dashboard_link}}", Convert.ToString(ConfigurationManager.AppSettings["LoginUrl"]));
                    body = body.Replace("{{Username}}", strEmail);
                    body = body.Replace("{{Password}}", strPwd);
                    body = body.Replace("{{university_name}}", strName);
                    Common.SendEmail(strEmail, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]), subject, body);
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "ChangeStatusUniversity", "RegistrationOperation");
                throw ex;
            }
            return msg;
        }

        public string DeleteUniversity(int UniversityId)
        {
            string msg = string.Empty;
            try
            {
                var para = new DynamicParameters();
                para.Add("@Id", UniversityId);
                msg = con.Query<string>("usp_delete_university", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "DeleteUniversity", "RegistrationOperation");
                throw ex;
            }
            return msg;
        }

        public string DeleteStudent(int StudentId)
        {
            string msg = string.Empty;
            try
            {
                var para = new DynamicParameters();
                para.Add("@Id", StudentId);
                msg = con.Query<string>("usp_delete_student", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "DeleteStudent", "RegistrationOperation");
                throw ex;
            }
            return msg;
        }

        public string VerifyStudent(string link, string emailid)
        {
            string Result = string.Empty;
            try
            {
                var para = new DynamicParameters();
                para.Add("@link", link);
                para.Add("@emailid", emailid);
                Result = con.Query<string>("usp_verify_student_email", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "VerifyStudent", "RegistrationOperation");
                return "Error";
            }
            return Result;
        }


        public string CheckRTOID(string RTOID)
        {
            string msg = string.Empty;
            try
            {
                var para = new DynamicParameters();
                para.Add("@RTOID", RTOID);
                msg = con.Query<string>("usp_CheckRTOID", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "CheckRTOID", "RegistrationOperation");
                throw ex;
            }
            return msg;
        }
    }
}