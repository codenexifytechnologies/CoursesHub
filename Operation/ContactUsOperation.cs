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
    public class ContactUsOperation
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
        public string AddConatctus(ContactUs objContactUs)
        {
            string msg = string.Empty;
            try
            {
                var para = new DynamicParameters();
                para.Add("@Name", objContactUs.Name);
                para.Add("@Email", objContactUs.Email);
                para.Add("@Subject", objContactUs.Subject);
                para.Add("@Message", objContactUs.Message);
                msg = con.Query<string>("usp_add_contactus", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();

                if(msg == "Thank you for contacting us. Our team will connect with you shortly.")
                {
                    string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Emails/ContactUs.html"));
                    body = body.Replace("{{name}}", objContactUs.Name);
                    body = body.Replace("{{subject}}", objContactUs.Subject);
                    body = body.Replace("{{message}}", objContactUs.Message);
                    Common.SendEmail(objContactUs.Email, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"].ToString()), ConfigurationManager.AppSettings["ContactUs"].ToString(), body);

                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "AddConatctus", "ContactUsOperation");
                throw ex;
            }
            return msg;
        }


    }
}