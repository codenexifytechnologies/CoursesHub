using CoursesHub.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace CoursesHub.Areas.Admin.Models
{
    public class UniversityOperation
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
        public List<UniversityRegistration> GetAllUniversity(UniversityRegistration objUniversityRegistration)
        {
            List<UniversityRegistration> objListUniversityRegistration = new List<UniversityRegistration>();
            try
            {
                var para = new DynamicParameters();
                para.Add("@Id", objUniversityRegistration.Id);
                objListUniversityRegistration = con.Query<UniversityRegistration>("usp_get_University", para, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetAllUniversity", "UniversityOperations");
                throw ex;
            }
            return objListUniversityRegistration;
        }

        public List<UniversityRegistration> GetArchiveUniversity(int? UniversityId)
        {
            List<UniversityRegistration> objListUniversityRegistration = new List<UniversityRegistration>();
            try
            {
                var para = new DynamicParameters();
                para.Add("@UniversityId", UniversityId);
                objListUniversityRegistration = con.Query<UniversityRegistration>("usp_get_ArchiveUniversity", para, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetArchiveUniversity", "UniversityOperations");
                throw ex;
            }
            return objListUniversityRegistration;
        }

        public string RetriveArchiveUniversity(int UniversityId)
        {
            string Result = string.Empty; ;
            try
            {
                var para = new DynamicParameters();
                para.Add("@UniversityId", UniversityId);
                Result = con.Query<string>("usp_retrive_ArchiveUniversity", CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetArchiveUniversity", "UniversityOperations");
                throw ex;
            }
            return Result;
        }

    }
}