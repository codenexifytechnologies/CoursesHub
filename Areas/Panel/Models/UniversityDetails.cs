using CoursesHub.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CoursesHub.Areas.Panel.Models
{
    public class UniversityDetails
    {
        public int EnrollCourses { get; set; }
        public int ActiveCourses { get; set; }
        public int NormalLead { get; set; }
        public int PremiumLead { get; set; }
    }

    public class UniversityOperations
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
        public UniversityDetails GetDashBoardCount(int UniversityId)
        {
            UniversityDetails objUniversityDetails = new UniversityDetails();
            try
            {
                var para = new DynamicParameters();
                para.Add("@UniversityId", UniversityId);
                objUniversityDetails = con.Query<UniversityDetails>("usp_get_UniversistyDashboardCounts", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetDashBoardCount", "UniversityOperations");
                throw ex;
            }
            return objUniversityDetails;
        }
    }
}