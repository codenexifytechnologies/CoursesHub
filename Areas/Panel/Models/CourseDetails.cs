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
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace CoursesHub.Areas.Panel.Models
{
    public class CourseDetails
    {
        public int Id { get; set; }
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public string UniversityEmail { get; set; }
        public string RTO_ID { get; set; }
        public string CourseName { get; set; }
        public string CourseProvider { get; set; }
        [AllowHtml]
        public string AboutCourse { get; set; }
        public string StudyMethod { get; set; }
        public string CourseCategory { get; set; }
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
        public string CoursePhoto { get; set; }
        public string CourseVideo { get; set; }
        public string CourseVideo2 { get; set; }
        public string CourseVideo3 { get; set; }
        public string CourseStartDate { get; set; }
        public string CourseDuration { get; set; }
        [AllowHtml]
        public string CourseOverview { get; set; }
        [AllowHtml]
        public string CourseOutcomes { get; set; }
        [AllowHtml]
        public string CourseLearn { get; set; }
        [AllowHtml]
        public string CourseRequirements { get; set; }
        [AllowHtml]
        public string WorkPlacement { get; set; }
        [AllowHtml]
        public string RPL { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public string CourseGuide { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool IsReject { get; set; }
        [AllowHtml]
        public string RejectionReason { get; set; }
        // TrendingCourse (varchar): set by the admin only, "1" = trending, "0" = not trending (see CourseFilterHelper.IsTrending)
        public string TrendingCourses { get; set; }
        // FeaturedCourse (varchar): set by the admin only, "High", "Medium" or "Low"; null = not featured
        public string FeaturedCourse { get; set; }
        public string CourseLevel { get; set; }

        public string TrendingCoursesName { get; set; }
        public string CourseLevelName { get; set; }
        public string UniversityLogo { get; set; }
        public int Count { get; set; }
        public string State { get; set; }
        // GovernmentFunding (varchar): saved as "Yes" (government funded) or "No" (fee for service).
        // Reading also accepts older values such as 1/0, true/false, "Government Funded" (see CourseFilterHelper.FundingFlag).
        public string GovernmentFunding { get; set; }
        // StudentType (varchar): saved as "Domestic" or "International" (see CourseFilterHelper.StudentTypeValue)
        public string StudentType { get; set; }

    }

    public class Testimonial
    {
        public int Id { get; set; }
        public int Course_Id { get; set; }
        public int Student_Id { get; set; }
        public string Person_Name { get; set; }
        public string Designation { get; set; }
        public string Comment { get; set; }
        public bool IsActive { get; set; }
        public string CourseName { get; set; }
        public string Trading_Name { get; set; }
    }

    public class CourseDetailsOperation
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

        public List<CourseDetails> GetCourseDetails(CourseDetails objCourseDetails, string logintype, string FilterType = null, string FilterValue = null, string CategoryId = null, string UniversityId = null)
        {
            List<CourseDetails> objListCourseDetails = new List<CourseDetails>();
            try
            {
                var para = new DynamicParameters();
                para.Add("@Id", objCourseDetails.Id);
                para.Add("@LoginType", logintype);
                para.Add("@CategoryType", FilterType == "CategoryType" ? FilterValue : null);
                para.Add("@CategoryId", CategoryId);
                para.Add("@UniversityId", UniversityId);
                para.Add("@StudeyType", FilterType == "StudeyType" ? FilterValue : null);
                para.Add("@State", FilterType == "State" ? FilterValue : null);
                para.Add("@ZipCode", FilterType == "ZipCode" ? FilterValue : null);
                para.Add("@CourseLevel", FilterType == "CourseLevel" ? FilterValue : null);
                para.Add("@QualificationType", FilterType == "QualificationType" ? FilterValue : null);

                objListCourseDetails = con.Query<CourseDetails>("usp_get_course", para, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetCourseDetails", "CourseDetailsOperation");
                throw ex;
            }
            return objListCourseDetails;
        }

        //public List<CourseDetails> GetCourseDetails(CourseDetails objCourseDetails, string logintype, string CategoryType = null, string CategoryId = null, string UniversityId = null)
        //{
        //    List<CourseDetails> objListCourseDetails = new List<CourseDetails>();
        //    try
        //    {
        //        var para = new DynamicParameters();
        //        para.Add("@Id", objCourseDetails.Id);
        //        para.Add("@LoginType", logintype);
        //        para.Add("@CategoryType", CategoryType);
        //        para.Add("@CategoryId", CategoryId);
        //        para.Add("@UniversityId", UniversityId);
        //        objListCourseDetails = con.Query<CourseDetails>("usp_get_course", para, null, true, 0, CommandType.StoredProcedure).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetCourseDetails", "CourseDetailsOperation");
        //        throw ex;
        //    }
        //    return objListCourseDetails;
        //}
        public string AddCourseDetails(CourseDetails objCourseDetails, HttpPostedFileBase Photo, HttpPostedFileBase Guide)
        {
            string msg = string.Empty;
            string GServerMapPath = string.Empty;
            string PServerMapPath = string.Empty;
            string massage = string.Empty;
            Registration objSessionRegistrationr = (Registration)HttpContext.Current.Session["Registration"];
            objCourseDetails.UniversityId = Convert.ToInt32(objSessionRegistrationr.objUniversityRegistration.Id);
            try
            {
                string Photo_guid = Convert.ToString(Guid.NewGuid());
                string Guide_guid = Convert.ToString(Guid.NewGuid());

                if (Photo != null && Photo.ContentLength > 0)
                {
                    string PhotoExtension = Path.GetExtension(Photo.FileName);
                    string PhotoPath = "Uploads/Photo/" + objCourseDetails.UniversityId;
                    PServerMapPath = HttpContext.Current.Server.MapPath("~/" + PhotoPath);
                    if (!Directory.Exists(PServerMapPath))
                    {
                        Directory.CreateDirectory(PServerMapPath);
                    }
                    Photo_guid = "/" + Photo_guid + PhotoExtension;
                    PhotoPath = PhotoPath + Photo_guid;
                    objCourseDetails.CoursePhoto = ConfigurationManager.AppSettings["BaseURL"].ToString() + PhotoPath;
                    PServerMapPath = PServerMapPath + Photo_guid;
                }

                if (Guide != null && Guide.ContentLength > 0)
                {
                    string GuideExtension = Path.GetExtension(Guide.FileName);
                    string GuidePath = "Uploads/Guide/" + objCourseDetails.UniversityId;
                    GServerMapPath = HttpContext.Current.Server.MapPath("~/" + GuidePath);
                    if (!Directory.Exists(GServerMapPath))
                    {
                        Directory.CreateDirectory(GServerMapPath);
                    }
                    Guide_guid = "/" + Guide_guid + GuideExtension;
                    GuidePath = GuidePath + Guide_guid;
                    objCourseDetails.CourseGuide = ConfigurationManager.AppSettings["BaseURL"].ToString() + GuidePath;
                    GServerMapPath = GServerMapPath + Guide_guid;

                }

                var para = new DynamicParameters();
                para.Add("@UniversityId", objCourseDetails.UniversityId);
                para.Add("@RTO_ID", objCourseDetails.RTO_ID);
                para.Add("@CourseName", objCourseDetails.CourseName);
                para.Add("@CourseProvider", objCourseDetails.CourseProvider);
                para.Add("@AboutCourse", objCourseDetails.AboutCourse);
                para.Add("@StudyMethod", objCourseDetails.StudyMethod);
                para.Add("@CourseCategory", objCourseDetails.CourseCategory);
                para.Add("@CoursePhoto", objCourseDetails.CoursePhoto);
                para.Add("@CourseVideo", objCourseDetails.CourseVideo);
                para.Add("@CourseVideo2", objCourseDetails.CourseVideo2);
                para.Add("@CourseVideo3", objCourseDetails.CourseVideo3);
                para.Add("@CourseStartDate", objCourseDetails.CourseStartDate);
                para.Add("@CourseDuration", objCourseDetails.CourseDuration);
                para.Add("@CourseOverview", objCourseDetails.CourseOverview);
                para.Add("@CourseOutcomes", objCourseDetails.CourseOutcomes);
                para.Add("@CourseLearn", objCourseDetails.CourseLearn);
                para.Add("@CourseRequirements", objCourseDetails.CourseRequirements);
                para.Add("@WorkPlacement", objCourseDetails.WorkPlacement);
                para.Add("@RPL", objCourseDetails.RPL);
                para.Add("@CourseGuide", objCourseDetails.CourseGuide);
                // Trending is decided by the admin; a new course starts as not trending
                para.Add("@TrendingCourse", "0", DbType.String);
                para.Add("@CourseLevel", objCourseDetails.CourseLevel);
                para.Add("@GovernmentFunding", CoursesHub.Models.CourseFilterHelper.FundingToStore(objCourseDetails.GovernmentFunding), DbType.String);
                para.Add("@StudentType", CoursesHub.Models.CourseFilterHelper.StudentTypeValue(objCourseDetails.StudentType), DbType.String);
                msg = con.Query<string>("usp_add_course", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                if (msg.Contains("Added"))
                {
                    string strName = string.Empty;
                    string strEmail = string.Empty;
                    string[] values = msg.Split(',');
                    massage = values[0];
                    strName = values[2];
                    strEmail = values[3];

                    string subject = Convert.ToString(ConfigurationManager.AppSettings["University_Add_Course"]);
                    subject = subject.Replace("{{course_name}}", objCourseDetails.CourseName);
                    string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Emails/University_Add_Course.html"));
                    body = body.Replace("{{university_name}}", strName);
                    body = body.Replace("{{course_name}}", objCourseDetails.CourseName);
                    Common.SendEmail(strEmail, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]), subject, body);


                    if (!string.IsNullOrEmpty(PServerMapPath))
                    {
                        if (Photo != null && Photo.ContentLength > 0)
                        {
                            using (var img = Image.FromStream(Photo.InputStream))
                            {
                                using (var resized = ResizeImage(img, 400, 400))
                                {
                                    resized.Save(PServerMapPath, ImageFormat.Jpeg);
                                }
                            }
                        }
                        // Photo.SaveAs(PServerMapPath);

                    }
                    if (!string.IsNullOrEmpty(GServerMapPath))
                    {
                        Guide.SaveAs(GServerMapPath);
                    }

                    if (objCourseDetails.Testimonials != null)
                    {
                        foreach (var item in objCourseDetails.Testimonials)
                        {
                            try
                            {
                                para = new DynamicParameters();
                                para.Add("@Course_Id", Convert.ToInt32(values[1]));
                                para.Add("@Person_Name", item.Person_Name);
                                para.Add("@Designation", item.Designation);
                                para.Add("@Comment", item.Comment);
                                con.Query<string>("usp_add_testimonial", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                            }
                            catch (Exception ex)
                            {
                                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "usp_add_testimonial", "CourseDetailsOperation");
                                throw ex;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "AddCourseDetails", "CourseDetailsOperation");
                throw ex;
            }
            return massage;
        }
        public string EditCourseDetails(CourseDetails objCourseDetails, HttpPostedFileBase Photo, HttpPostedFileBase Guide)
        {
            string msg = string.Empty;
            string GServerMapPath = string.Empty;
            string PServerMapPath = string.Empty;
            Registration objSessionRegistrationr = (Registration)HttpContext.Current.Session["Registration"];
            objCourseDetails.UniversityId = Convert.ToInt32(objSessionRegistrationr.objUniversityRegistration.Id);

            try
            {
                string Photo_guid = Convert.ToString(Guid.NewGuid());
                string Guide_guid = Convert.ToString(Guid.NewGuid());

                if (Photo != null && Photo.ContentLength > 0)
                {
                    string PhotoExtension = Path.GetExtension(Photo.FileName);
                    string PhotoPath = "Uploads/Photo/" + objCourseDetails.UniversityId;
                    PServerMapPath = HttpContext.Current.Server.MapPath("~/" + PhotoPath);
                    if (!Directory.Exists(PServerMapPath))
                    {
                        Directory.CreateDirectory(PServerMapPath);
                    }
                    Photo_guid = "/" + Photo_guid + PhotoExtension;
                    PhotoPath = PhotoPath + Photo_guid;
                    objCourseDetails.CoursePhoto = ConfigurationManager.AppSettings["BaseURL"].ToString() + PhotoPath;
                    PServerMapPath = PServerMapPath + Photo_guid;
                }

                if (Guide != null && Guide.ContentLength > 0)
                {
                    string GuideExtension = Path.GetExtension(Guide.FileName);
                    string GuidePath = "Uploads/Guide/" + objCourseDetails.UniversityId;
                    GServerMapPath = HttpContext.Current.Server.MapPath("~/" + GuidePath);
                    if (!Directory.Exists(GServerMapPath))
                    {
                        Directory.CreateDirectory(GServerMapPath);
                    }
                    Guide_guid = "/" + Guide_guid + GuideExtension;
                    GuidePath = GuidePath + Guide_guid;
                    objCourseDetails.CourseGuide = ConfigurationManager.AppSettings["BaseURL"].ToString() + GuidePath;
                    GServerMapPath = GServerMapPath + Guide_guid;

                }
                var para = new DynamicParameters();
                para.Add("@Id", objCourseDetails.Id);
                para.Add("@UniversityId", objCourseDetails.UniversityId);
                para.Add("@RTO_ID", objCourseDetails.RTO_ID);
                para.Add("@CourseName", objCourseDetails.CourseName);
                para.Add("@CourseProvider", objCourseDetails.CourseProvider);
                para.Add("@AboutCourse", objCourseDetails.AboutCourse);
                para.Add("@StudyMethod", objCourseDetails.StudyMethod);
                para.Add("@CourseCategory", objCourseDetails.CourseCategory);
                para.Add("@CoursePhoto", objCourseDetails.CoursePhoto);
                para.Add("@CourseVideo", objCourseDetails.CourseVideo);
                para.Add("@CourseVideo2", objCourseDetails.CourseVideo2);
                para.Add("@CourseVideo3", objCourseDetails.CourseVideo3);
                para.Add("@CourseStartDate", objCourseDetails.CourseStartDate);
                para.Add("@CourseDuration", objCourseDetails.CourseDuration);
                para.Add("@CourseOverview", objCourseDetails.CourseOverview);
                para.Add("@CourseOutcomes", objCourseDetails.CourseOutcomes);
                para.Add("@CourseLearn", objCourseDetails.CourseLearn);
                para.Add("@CourseRequirements", objCourseDetails.CourseRequirements);
                para.Add("@WorkPlacement", objCourseDetails.WorkPlacement);
                para.Add("@RPL", objCourseDetails.RPL);
                para.Add("@CourseGuide", objCourseDetails.CourseGuide);
                // Trending is decided by the admin: a provider edit keeps the value already saved for this course
                string savedTrending = null;
                try
                {
                    var savedCourse = GetCourseDetails(new CourseDetails { Id = objCourseDetails.Id }, "Admin").FirstOrDefault();
                    if (savedCourse != null) savedTrending = savedCourse.TrendingCourses;
                }
                catch (Exception lookupEx)
                {
                    Common.errorlog(Convert.ToString(lookupEx.Message), "EditCourseDetails trending lookup", "CourseDetailsOperation");
                }
                para.Add("@TrendingCourse", savedTrending, DbType.String);
                para.Add("@CourseLevel", objCourseDetails.CourseLevel);
                para.Add("@GovernmentFunding", CoursesHub.Models.CourseFilterHelper.FundingToStore(objCourseDetails.GovernmentFunding), DbType.String);
                para.Add("@StudentType", CoursesHub.Models.CourseFilterHelper.StudentTypeValue(objCourseDetails.StudentType), DbType.String);

                msg = con.Query<string>("usp_edit_course", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
                if (msg.Contains("Updated"))
                {
                    if (!string.IsNullOrEmpty(PServerMapPath))
                        Photo.SaveAs(PServerMapPath);
                    if (!string.IsNullOrEmpty(GServerMapPath))
                        Guide.SaveAs(GServerMapPath);
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "EditCourseDetails", "CourseDetailsOperation");
                throw ex;
            }
            return msg;
        }
        public string StausChangeCourse(CourseDetails objCourseDetails)
        {
            string Result = string.Empty;
            string strCourseName = string.Empty;
            string strName = string.Empty;
            string strEmail = string.Empty;

            try
            {
                var para = new DynamicParameters();
                para.Add("@Course_Id", objCourseDetails.Id);
                para.Add("@IsActive", objCourseDetails.IsActive);
                // Admin-only promotion settings: Trending "1"/"0", Featured "High"/"Medium"/"Low" (null = not featured)
                para.Add("@TrendingCourse", CoursesHub.Models.CourseFilterHelper.TrendingToStore(objCourseDetails.TrendingCourses), DbType.String);
                para.Add("@FeaturedCourse", CoursesHub.Models.CourseFilterHelper.FeaturedPriority(objCourseDetails.FeaturedCourse), DbType.String);
                Result = con.Query<string>("usp_change_status_course", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();

                if (Result.Contains(","))
                {
                    var parts = Result.Split(',');
                    if (parts.Length > 1)
                    {
                        Result = parts[0];
                        strCourseName = parts[1];
                        strName = parts[2];
                        strEmail = parts[3];
                    }
                }
                if (Result.ToLower() == "activated")
                {
                    string subject = Convert.ToString(ConfigurationManager.AppSettings["University_Add_Course_Approval"]);
                    subject = subject.Replace("{{course_name}}", strCourseName);
                    string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Emails/University_Add_Course_Approval.html"));
                    body = body.Replace("{{university_name}}", strName);
                    body = body.Replace("{{course_name}}", strName);
                    Common.SendEmail(strEmail, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]), subject, body);
                }
                else if (Result.ToLower() == "inactivated")
                {
                    string subject = Convert.ToString(ConfigurationManager.AppSettings["CourseInactivation"]);
                    subject = subject.Replace("{{course_name}}", strCourseName);
                    string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Emails/CourseInactivation.html"));
                    body = body.Replace("{{university_name}}", strName);
                    body = body.Replace("{{course_name}}", strName);
                    Common.SendEmail(strEmail, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]), subject, body);
                }

            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "StausChangeTestimonial", "CourseDetailsOperation");
                throw ex;
            }
            return Result;
        }
        public string DeleteCourse(int CourseId)
        {
            string msg = string.Empty;
            try
            {
                var para = new DynamicParameters();
                para.Add("@Id", CourseId);
                msg = con.Query<string>("usp_delete_Course", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "DeleteCourse", "CourseDetailsOperation");
                throw ex;
            }
            return msg;
        }
        public string RejectCourse(int CourseId, string reason)
        {
            string msg = string.Empty;
            try
            {
                var para = new DynamicParameters();
                para.Add("@CourseId", CourseId);
                para.Add("@reason", reason.Replace("\n", "<br>"));
                msg = con.Query<string>("usp_reject_Course", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();

                if (msg == "Rejected")
                {
                    CourseDetails objCourseDetails = new CourseDetails();
                    objCourseDetails.Id = CourseId;
                    objCourseDetails = GetCourseDetails(objCourseDetails, "Admin").FirstOrDefault();
                    string subject = Convert.ToString(ConfigurationManager.AppSettings["University_Add_Course_Rejection"]);
                    subject = subject.Replace("{{course_name}}", objCourseDetails.CourseName);
                    string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Emails/University_Add_Course_Rejection.html"));
                    body = body.Replace("{{university_name}}", objCourseDetails.UniversityName);
                    body = body.Replace("{{course_name}}", objCourseDetails.CourseName);
                    body = body.Replace("{{RejectionReason}}", reason.Replace("\n", "<br>"));
                    Common.SendEmail(objCourseDetails.UniversityEmail, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]), subject, body);
                }
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "DeleteCourse", "CourseDetailsOperation");
                throw ex;
            }
            return msg;
        }
        public List<CourseDetails> GetSearchCourseDetails(string Keyword)
        {
            List<CourseDetails> objListCourseDetails = new List<CourseDetails>();
            try
            {
                var para = new DynamicParameters();
                para.Add("@Keyword", Keyword);
                objListCourseDetails = con.Query<CourseDetails>("usp_get_search_course_details", para, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetTestimonials", "CourseDetailsOperation");
                throw ex;
            }
            return objListCourseDetails;
        }
        public List<Testimonial> GetTestimonials(int Testimonial_Id, int Course_Id, int Student_Id, string LoginType)
        {
            List<Testimonial> objListTestimonial = new List<Testimonial>();
            try
            {
                var para = new DynamicParameters();
                para.Add("@Testimonial_Id", Testimonial_Id);
                para.Add("@Course_Id", Course_Id);
                para.Add("@Student_Id", Student_Id);
                para.Add("@LoginType", LoginType);
                objListTestimonial = con.Query<Testimonial>("usp_get_testimonial", para, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetTestimonials", "CourseDetailsOperation");
                throw ex;
            }
            return objListTestimonial;
        }
        public string SaveTestimonial(Testimonial objTestimonial)
        {
            string msg = string.Empty;
            try
            {
                var para = new DynamicParameters();
                para.Add("@Course_Id", objTestimonial.Course_Id);
                para.Add("@Student_Id", objTestimonial.Student_Id);
                para.Add("@Person_Name", objTestimonial.Person_Name);
                para.Add("@Designation", objTestimonial.Designation);
                para.Add("@Comment", objTestimonial.Comment);
                msg = con.Query<string>("usp_add_testimonial", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "SaveTestimonial", "CourseDetailsOperation");
                throw ex;
            }
            return msg;
        }
        public string EditTestimonial(Testimonial objTestimonial)
        {
            string msg = string.Empty;
            try
            {
                var para = new DynamicParameters();
                para.Add("@Id", objTestimonial.Id);
                para.Add("@Course_Id", objTestimonial.Course_Id);
                para.Add("@Student_Id", objTestimonial.Student_Id);
                para.Add("@Person_Name", objTestimonial.Person_Name);
                para.Add("@Designation", objTestimonial.Designation);
                para.Add("@Comment", objTestimonial.Comment);
                para.Add("@IsActive", objTestimonial.IsActive);
                msg = con.Query<string>("usp_edit_testimonial", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "EditTestimonial", "CourseDetailsOperation");
                throw ex;
            }
            return msg;
        }
        public string StausChangeTestimonial(Testimonial objTestimonial)
        {
            string Result = string.Empty;
            try
            {
                var para = new DynamicParameters();
                para.Add("@Testimonail_Id", objTestimonial.Id);
                para.Add("@IsActive", objTestimonial.IsActive);
                Result = con.Query<string>("usp_staus_change_testimonial", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "StausChangeTestimonial", "CourseDetailsOperation");
                throw ex;
            }
            return Result;
        }
        public string DeleteTestimonial(int TestimonialId)
        {
            string msg = string.Empty;
            try
            {
                var para = new DynamicParameters();
                para.Add("@Id", TestimonialId);
                msg = con.Query<string>("usp_delete_Testimonial", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "DeleteTestimonial", "CourseDetailsOperation");
                throw ex;
            }
            return msg;
        }
        public List<CourseDetails> GetAppliedCourseDetails(int StudentId)
        {
            List<CourseDetails> objListCourseDetails = new List<CourseDetails>();
            try
            {
                var para = new DynamicParameters();
                para.Add("@StudentId", StudentId);
                objListCourseDetails = con.Query<CourseDetails>("usp_get_applied_course", para, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetCourseDetails", "CourseDetailsOperation");
                throw ex;
            }
            return objListCourseDetails;
        }
        public string Connect(int CourseId, int UniversityId, StudentRegistration objStudentRegistration)
        {
            string msg = string.Empty;
            try
            {
                var para = new DynamicParameters();
                para.Add("@Course_Id", CourseId);
                para.Add("@Student_Id", objStudentRegistration.Id);
                para.Add("@University_ID", UniversityId);
                msg = con.Query<string>("usp_CreateLead", para, null, true, 0, CommandType.StoredProcedure).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Connect", "CourseDetailsOperation");
                throw ex;
            }
            return msg;
        }

        private static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private static Bitmap ResizeImageKeepRatio(Image image, int size)
        {
            int width, height;

            if (image.Width > image.Height)
            {
                width = size;
                height = (int)(image.Height * size / (float)image.Width);
            }
            else
            {
                height = size;
                width = (int)(image.Width * size / (float)image.Height);
            }

            return ResizeImage(image, width, height);
        }

    }
}