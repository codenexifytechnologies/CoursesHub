using CoursesHub.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Helpers;

namespace CoursesHub.Areas.Admin.Models
{
    public class Lead
    {
        public int Id { get; set; }
        public int Course_ID { get; set; }
        public string Course_Name { get; set; }
        public string CourseDuration { get; set; }
        public string StudyMethod { get; set; }
        public string CourseCategory { get; set; }

        public string Student_Name { get; set; }
        public int Student_ID { get; set; }
        public string Student_EmailId { get; set; }
        public string Student_Mobile { get; set; }
        public bool Student_isAustraliaResident { get; set; }
        public string Student_Zipcode { get; set; }
        public string Student_State { get; set; }
        public string Student_Country { get; set; }

        public int University_ID { get; set; }
        public string University_Name { get; set; }
        public string University_Full_Name { get; set; }
        public string University_Position { get; set; }
        public string University_Email { get; set; }
        public string University_Direct_Phone { get; set; }

        public bool isEmailSent { get; set; }
        public DateTime EmailSentOn { get; set; }
        public bool isPremiumLead { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LeadType { get; set; }
    }

    public class LeadOperations
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
        public List<Lead> GetAllLeads()
        {
            List<Lead> objListLead = new List<Lead>();
            try
            {
                var para = new DynamicParameters();
                objListLead = con.Query<Lead>("usp_get_Premium_Lead", para, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetAllLeads", "LeadOperations");
                throw ex;
            }
            return objListLead;
        }

        public List<Lead> GetPremiumLeadsById(int Student_ID)
        {
            List<Lead> objListLead = new List<Lead>();
            try
            {
                var para = new DynamicParameters();
                para.Add("@Student_ID", Student_ID);
                objListLead = con.Query<Lead>("usp_get_Premium_Lead", para, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetPremiumLeadsById", "LeadOperations");
                throw ex;
            }
            return objListLead;
        }

        public List<Lead> GetAllNormalLeadsById(int Student_ID)
        {
            List<Lead> objListLead = new List<Lead>();
            try
            {
                var para = new DynamicParameters();
                para.Add("@Student_ID", Student_ID);
                objListLead = con.Query<Lead>("usp_get_NormalLead", para, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetAllNormalLeadsById", "LeadOperations");
                throw ex;
            }
            return objListLead;
        }

        public List<Lead> GetAllDetails(int id, string LeadType)
        {
            List<Lead> objListLead = new List<Lead>();
            try
            {
                var para = new DynamicParameters();
                para.Add("@Id", id);
                para.Add("@LeadType", LeadType);
                objListLead = con.Query<Lead>("usp_get_Lead_Details", para, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetAllDetails", "LeadOperations");
                throw ex;
            }
            return objListLead;
        }

        public bool UpdateLeadAfterEmail(int id, string LeadType)
        {
            bool Result = false;
            try
            {
                var para = new DynamicParameters();
                para.Add("@Id", id);
                para.Add("@LeadType", LeadType);
                Result = con.Query<bool>("usp_UpdateLeadAfterEmail", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "UpdateLeadAfterEmail", "LeadOperations");
                throw ex;
            }
            return Result;
        }

        public List<Lead> GetLeadForUniversity(int UniversityId)
        {
            List<Lead> objListLead = new List<Lead>();
            try
            {
                var para = new DynamicParameters();
                para.Add("@UniversityId", UniversityId);
                objListLead = con.Query<Lead>("usp_get_LeadsForUniversity", para, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetLeadForUniversity", "LeadOperations");
                throw ex;
            }
            return objListLead;
        }

        public List<Lead> GetPremiumLeadsByIdForSendingMail(int Student_ID)
        {
            List<Lead> objListLead = new List<Lead>();
            try
            {
                var para = new DynamicParameters();
                para.Add("@Student_ID", Student_ID);
                objListLead = con.Query<Lead>("usp_get_Premium_Lead_SendMail", para, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetPremiumLeadsByIdForSendingMail", "LeadOperations");
                throw ex;
            }
            return objListLead;
        }

        public List<Lead> GetAllNormalLeadsByIdForSendingMail(int Student_ID)
        {
            List<Lead> objListLead = new List<Lead>();
            try
            {
                var para = new DynamicParameters();
                para.Add("@Student_ID", Student_ID);
                objListLead = con.Query<Lead>("usp_get_NormalLead_SendMail", para, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetAllNormalLeadsByIdForSendingMail", "LeadOperations");
                throw ex;
            }
            return objListLead;
        }
    }
}