using CoursesHub.Areas.Panel.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

namespace CoursesHub.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryValue { get; set; }
        public string CategoryType { get; set; }
        public string ImagePath { get; set; }
        public int Count { get; set; }
        public int IsActive { get; set; }
    }

    public class TrendingCourses
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }

    public class CourseLevels
    {
        public int Id { get; set; }
        public string CourseLevel { get; set; }
        public int IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }

    public class Common
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
        public static bool SendEmail(string EmailTo, string EmailFrom, string EmailSubject, string EmailBody, string EmailCC = null)
        {
            bool Result = false;
            try
            {
                if (Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["IsMailSendFlag"])) == "true")
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(EmailFrom, ConfigurationManager.AppSettings["EmailDisplayName"]);
                    // mail.From = new MailAddress(EmailFrom);
                    mail.To.Add(EmailTo);
                    if (!string.IsNullOrEmpty(EmailCC))
                    {
                        mail.CC.Add(EmailCC);
                    }
                    mail.Subject = EmailSubject;
                    mail.IsBodyHtml = true;
                    mail.Body = EmailBody;
                    SmtpClient Smtpobj = new SmtpClient(Convert.ToString(ConfigurationManager.AppSettings["Host"]));
                    Smtpobj.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                    Smtpobj.Credentials = new System.Net.NetworkCredential(Convert.ToString(ConfigurationManager.AppSettings["EmailUsername"]), Convert.ToString(ConfigurationManager.AppSettings["EmailPassword"]));
                    Smtpobj.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EmailSSL"]);
                    Smtpobj.Send(mail);
                    Result = true;
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "SendEmail", "Common");
            }
            return Result;
        }

        public static void errorlog(string error, string method, string controller)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["isErrorLogON"]))
            {
                string Path = HttpContext.Current.Server.MapPath("~/Error.txt");
                string Contents = "\n DateTime: " + DateTime.Now + "\n===========================\n Method Name: " + method + "\n Error: \n" + error + "\n controller: \n" + controller;
                File.AppendAllText(Path, Contents);
            }
        }

        public static List<SelectListItem> getcategory(string UniversityId = null)
        {
            List<SelectListItem> obj = null;
            try
            {
                var para = new DynamicParameters();
                para.Add("@UniversityId", UniversityId);
                var categories = con.Query<Category>("usp_get_category", para, null, true, 0, CommandType.StoredProcedure).ToList();
                obj = categories.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.CategoryValue,

                }).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "getcategory", "Common");
            }
            return obj;
        }

        public static List<SelectListItem> getcategorytype()
        {
            List<SelectListItem> obj = null;
            try
            {
                var categories = con.Query<Category>("usp_get_categorytype", null, null, true, 0, CommandType.StoredProcedure).ToList();
                obj = categories.Select(x => new SelectListItem
                {
                    Value = x.CategoryType,
                    Text = x.CategoryType,

                }).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "getcategorytype", "Common");
            }
            return obj;
        }

        public static List<Category> getcategoryData()
        {
            List<Category> obj = null;
            try
            {
                var categories = con.Query<Category>("usp_GetCategoriesCount", null, null, true, 0, CommandType.StoredProcedure).ToList();
                obj = categories.Select(x => new Category
                {
                    Id = x.Id,
                    CategoryValue = x.CategoryValue,
                    CategoryType = x.CategoryType,
                    ImagePath = x.ImagePath,
                    Count = x.Count,

                }).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "getcategorytype", "Common");
            }
            return obj;
        }

        public static string ChangePassword(ChangePassword objChangePassword, string LoginType)
        {
            string Result = string.Empty;
            try
            {
                var para = new DynamicParameters();
                para.Add("@Id", objChangePassword.Id);
                para.Add("@OldPassword", objChangePassword.oldPassword);
                para.Add("@Password", objChangePassword.RenterNewPassword);
                para.Add("@LoginType", LoginType);
                Result = con.Query<string>("usp_ChangePassword", para, null, true, 0, CommandType.StoredProcedure).First();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Result = "Failed to change pasword.";
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Result;
        }

        public static string RemoveHtmlTags(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        public static void SaveRequestResponse(string MethodName, string Request, string Response)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["SaveRequest"]))
            {
                string Path = HttpContext.Current.Server.MapPath("~/RequestResponse.txt");
                string Contents = "\n DateTime: " + DateTime.Now + "\n===========================\n Method Name: " + MethodName + "\n Request: \n" + Request + "\n Response: \n" + Response;
                File.AppendAllText(Path, Contents);
            }
        }

        public static List<SelectListItem> getTrendingCourses()
        {
            List<SelectListItem> obj = null;
            try
            {
                var TrendingCourses = con.Query<TrendingCourses>("usp_get_trendingcourses", null, null, true, 0, CommandType.StoredProcedure).ToList();
                obj = TrendingCourses.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.CourseName,

                }).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "getTrendingCourses", "Common");
            }
            return obj;
        }


        public static List<SelectListItem> getCourseLevel()
        {
            List<SelectListItem> obj = null;
            try
            {
                var CourseLevels = con.Query<CourseLevels>("usp_get_courselevel", null, null, true, 0, CommandType.StoredProcedure).ToList();
                obj = CourseLevels.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.CourseLevel,

                }).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "getCourseLevel", "Common");
            }
            return obj;
        }

        //    public static master getmasters()
        //    {
        //        master obj = new master();
        //        try
        //        {
        //            con.open();
        //            var objmasters = con.querymultiple("usp_get_masters", commandtype: commandtype.storedprocedure);
        //            con.close();
        //            obj.objstudymethod = objmasters.read<studymethod>().tolist();
        //            obj.objstate = objmasters.read<state>().tolist();
        //            obj.objzipcode = objmasters.read<zipcode>().tolist();
        //            obj.objcourselevel = objmasters.read<courselevel>().tolist();
        //            obj.objqualificationlevel = objmasters.read<qualificationlevel>().tolist();
        //        }
        //        catch (exception ex)
        //        {
        //            common.errorlog(convert.tostring(ex.message) + " inner msg:" + convert.tostring(ex.stacktrace), "getmasters", "common");
        //        }
        //        finally
        //        {
        //            if (connectionstate.open == con.state)
        //            {
        //                con.close();
        //            }
        //        }
        //        return obj;
        //    }
        //}


        public static Master getMasters()
        {
            Master obj = new Master();
            try
            {
                using (var multi = con.QueryMultiple(
                    "usp_get_Masters",
                    commandType: CommandType.StoredProcedure))
                {
                    obj.objStudyMethod = multi.Read<StudyMethod>().ToList();
                    obj.objState = multi.Read<State>().ToList();
                    obj.objZipCode = multi.Read<ZipCode>().ToList();
                    obj.objCourseLevel = multi.Read<CourseLevel>().ToList();
                    obj.objQualificationLevel = multi.Read<QualificationLevel>().ToList();
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(
                    $"{ex.Message} Inner Msg: {ex.StackTrace}",
                    "getMasters",
                    "Common"
                );
            }
            return obj;
        }
    }
    public class SendToUniversity
    {
        public Registration objRegistration { get; set; }
        public CourseDetails objCourseDetails { get; set; }
    }

    public class StudyMethod
    {
        public string objStudymethod { get; set; }
    }
    public class State
    {
        public string objState { get; set; }
    }
    public class ZipCode
    {
        public string objZipCode { get; set; }
    }
    public class CourseLevel
    {
        public int objCourseLevelId { get; set; }
        public string objCourseLevel { get; set; }
    }
    public class QualificationLevel
    {
        public int objQualificationLevelId { get; set; }
        public string objQualificationLevel { get; set; }
    }

    public class Master
    {
        public List<StudyMethod> objStudyMethod { get; set; }
        public List<State> objState { get; set; }
        public List<ZipCode> objZipCode { get; set; }
        public List<CourseLevel> objCourseLevel { get; set; }
        public List<QualificationLevel> objQualificationLevel { get; set; }
    }

    public class Career
    {
        public string CourseCategory { get; set; }
        public string CourseCategoryName { get; set; }
        public string CourseLevel { get; set; }
        public string StudyMode { get; set; }
        public string EducationLevel { get; set; }
    }

    public class CareerPage
    {
        public Career objCareer { get; set; }
        public StudentRegistration objStudentRegistration { get; set; }
    }
}