using CoursesHub.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace CoursesHub.Operation
{
    public class LoginOperation
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

        #region Login
        public Registration Login(Login objLogin)
        {
            Registration objRegistration = new Registration();
            try
            {
                var para = new DynamicParameters();
                para.Add("@EmailId", objLogin.EmailId);
                para.Add("@Password", objLogin.Password);
                para.Add("@IsStudent", objLogin.LoginType == "Student" ? 1 : 0);
                if (objLogin.LoginType == "Student")
                    objRegistration.objStudentRegistration = con.Query<StudentRegistration>("usp_UserLogin", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                else
                    objRegistration.objUniversityRegistration = con.Query<UniversityRegistration>("usp_UserLogin", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Login", "LoginOperation");
            }
            return objRegistration;
        }
        #endregion

        public Registration GetProfile(string LoginType, int Id)
        {
            Registration objRegistration = new Registration();
            try
            {
                var para = new DynamicParameters();
                para.Add("@Id", Id);
                para.Add("@LoginType", LoginType);
                if (LoginType == "Student")
                    objRegistration.objStudentRegistration = con.Query<StudentRegistration>("usp_GetProfile", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                else
                    objRegistration.objUniversityRegistration = con.Query<UniversityRegistration>("usp_GetProfile", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetProfile", "LoginOperation");
            }
            return objRegistration;
        }

        //#region Change Passsword
        //public string ChangePassword(ChangePassword objChangePassword, int UserId, string EmailId)
        //{
        //    string Result = string.Empty;
        //    try
        //    {
        //        using (connection = new SqlConnection(connectionString))
        //        {
        //            using (SqlCommand command = new SqlCommand("usp_ChangePassword", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.AddWithValue("@LoginId", UserId);
        //                command.Parameters.AddWithValue("@EmailId", EmailId);
        //                command.Parameters.AddWithValue("@OldPassword", objChangePassword.oldPassword);
        //                command.Parameters.AddWithValue("@Password", objChangePassword.RenterNewPassword);
        //                command.Connection.Open();
        //                Result = (string)command.ExecuteScalar();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //        Result = "Failed to change pasword.";
        //    }
        //    finally
        //    {
        //        if (connection.State == ConnectionState.Open)
        //        {
        //            connection.Close();
        //        }
        //    }
        //    return Result;
        //}
        //#endregion


        #region Lost Password

        public string LostPassword(Login objLogin)
        {
            string strResult = "";
            string strPassword = Convert.ToString(Guid.NewGuid()).ToUpper().Substring(0, 6);
            try
            {
                var para = new DynamicParameters();
                para.Add("@EmailId", objLogin.EmailId);
                para.Add("@Password", strPassword);
                strResult = con.Query<string>("usp_LostPassword", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                if(strResult.Contains("Success"))
                {
                    string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Emails/PasswordRecovered.html"));
                    body = body.Replace("{{user_id}}", objLogin.EmailId);
                    body = body.Replace("{{password}}", strPassword);
                    body = body.Replace("{{login_link}}", Convert.ToString(ConfigurationManager.AppSettings["LoginUrl"].ToString()));
                    Common.SendEmail(objLogin.EmailId, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"].ToString()), ConfigurationManager.AppSettings["PasswordRecovered"].ToString(), body);

                }

            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "LostPassword", "LoginOperation");
            }
            return strResult;
        }
        #endregion
    }
}