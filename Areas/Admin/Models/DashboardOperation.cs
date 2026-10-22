using CoursesHub.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CoursesHub.Areas.Admin.Models
{
    public class DashboardCount
    {
        public UnivresityCount ObjUnivresityCount { get; set; }
        public CoursesCount ObjCoursesCount { get; set; }
        public FeedbackCount ObjFeedbackCount { get; set; }
        public LeadCount ObjLeadCount { get; set; }
    }
    public class UnivresityCount
    {
        public int UTotal { get; set; }
        public int UNew { get; set; }
        public int UVerified { get; set; }
    }

    public class CoursesCount
    {
        public int CTotal { get; set; }
        public int CNew { get; set; }
        public int CVerified { get; set; }
    }
    public class FeedbackCount
    {
        public int TTotal { get; set; }
        public int TNew { get; set; }
        public int TVerified { get; set; }
    }
    public class LeadCount
    {
        public int LTotal { get; set; }
        public int TNew { get; set; }
        public int TVerified { get; set; }
    }

    public class MonthwiseLead
    {
        public string MonthName { get; set; }
        public int PremiumLead { get; set; }
        public int NormalLead { get; set; }
    }

    public class DonutData
    {
        public int isCountry { get; set; }
        public string country { get; set; }
        public int count { get; set; }
        public string ColorCode { get; set; }

    }
    public class PieData
    {
        public string CategoryType { get; set; }
        public int count { get; set; }
        public string ColorCode { get; set; }

    }
    
    public class DashboardOperation
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
        public DashboardCount GetCount()
        {
            DashboardCount objDashboardCount = new DashboardCount();
            try
            {
                UnivresityCount oUnivresityCount = new UnivresityCount();
                CoursesCount oCoursesCount = new CoursesCount();
                FeedbackCount oFeedbackCount = new FeedbackCount();
                LeadCount oLeadCount = new LeadCount();
                var para = new DynamicParameters();

                using (var multi = con.QueryMultiple("usp_get_dashboard_count", commandType: CommandType.StoredProcedure))
                {
                    oUnivresityCount = multi.Read<UnivresityCount>().FirstOrDefault();
                    oCoursesCount = multi.Read<CoursesCount>().FirstOrDefault();
                    oFeedbackCount = multi.Read<FeedbackCount>().FirstOrDefault();
                    oLeadCount = multi.Read<LeadCount>().FirstOrDefault();
                }
                objDashboardCount.ObjUnivresityCount = oUnivresityCount;
                objDashboardCount.ObjCoursesCount = oCoursesCount;
                objDashboardCount.ObjFeedbackCount = oFeedbackCount;
                objDashboardCount.ObjLeadCount = oLeadCount;
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetCount", "BlogOperations");
            }
            return objDashboardCount;
        }

        public List<MonthwiseLead> Monthwise_Lead(int? universityId)
        {
            List<MonthwiseLead> objListLead = new List<MonthwiseLead>();
            try
            {
                var para = new DynamicParameters();
                para.Add("@UniversityId", universityId);
                objListLead = con.Query<MonthwiseLead>("usp_monthwise_lead", para, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Monthwise_Lead", "DashboardOperation");
                throw ex;
            }
            return objListLead;
        }

        public List<DonutData> DonutData()
        {
            List<DonutData> objListLead = new List<DonutData>();
            try
            {
                var para = new DynamicParameters();
                objListLead = con.Query<DonutData>("usp_GetDonutData", para, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "DonutData", "DashboardOperation");
                throw ex;
            }
            return objListLead;
        }

        public List<PieData> PieData()
        {
            List<PieData> objListLead = new List<PieData>();
            try
            {
                var para = new DynamicParameters();
                objListLead = con.Query<PieData>("usp_GetPieData", para, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "PieData", "DashboardOperation");
                throw ex;
            }
            return objListLead;
        }


        public int PendingCoursesCount()
        {
            int intCount = 0;
            try
            {
                var para = new DynamicParameters();
                intCount = con.Query<int>("usp_GetCourseCount", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "PendingCoursesCount", "DashboardOperation");
                throw ex;
            }
            return intCount;
        }

        public int PendingUniversityCount()
        {
            int intCount = 0;
            try
            {
                var para = new DynamicParameters();
                intCount = con.Query<int>("usp_GetUniversityCount", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "PendingUniversityCount", "DashboardOperation");
                throw ex;
            }
            return intCount;
        }


    }
}