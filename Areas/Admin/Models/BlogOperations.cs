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
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CoursesHub.Areas.Admin.Models
{
    public class Blogs
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Blog Title field is required")]
        public string BlogTitle { get; set; }

        public string OnelinerDiscription { get; set; }

        public string Quote { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "The Full Discription field is required")]
        public string FullDiscription { get; set; }
        public string SubTitle { get; set; }
        public string SubTitleDiscription { get; set; }
        public HttpPostedFileBase BlogThumbnail { get; set; }
        public HttpPostedFileBase BlogSmallThumbnail1 { get; set; }
        public HttpPostedFileBase BlogSmallThumbnail2 { get; set; }
        public HttpPostedFileBase BlogSmallThumbnail3 { get; set; }
        public string BlogThumbnailImagePath { get; set; }
        public string BlogSmallThumbnail1ImagePath { get; set; }
        public string BlogSmallThumbnail2ImagePath { get; set; }
        public string BlogSmallThumbnail3ImagePath { get; set; }
        public int BlogVerified { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string slug { get; set; }

        public string SEOTitle { get; set; }
        public string SEOMetaDiscription { get; set; }
        public string SEOMetaKeywords { get; set; }
        public string WebsiteLink { get; set; }
    }
    public class BlogOperations
    {
        string con = ConfigurationManager.ConnectionStrings["DBCon"].ToString();

        public string AddBlog(Blogs objBlogs)
        {
            string Result = string.Empty;
            try
            {
                string BlogThumbnail = Convert.ToString(Guid.NewGuid());
                string BlogSmallThumbnail1 = Convert.ToString(Guid.NewGuid());
                string BlogSmallThumbnail2 = Convert.ToString(Guid.NewGuid());
                string BlogSmallThumbnail3 = Convert.ToString(Guid.NewGuid());
                string BlogThumbnail_image_path = string.Empty;
                string BlogThumbnail_image_webpath = string.Empty;
                string BlogSmallThumbnail1_path = string.Empty;
                string BlogSmallThumbnail1_webpath = string.Empty;
                string BlogSmallThumbnail2_path = string.Empty;
                string BlogSmallThumbnail2_webpath = string.Empty;
                string BlogSmallThumbnail3_path = string.Empty;
                string BlogSmallThumbnail3_webpath = string.Empty;

                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString())))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString()));
                }

                if (objBlogs.BlogThumbnail != null && objBlogs.BlogThumbnail.ContentLength > 0)
                {
                    BlogThumbnail_image_path = HttpContext.Current.Server.MapPath("~/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString() + BlogThumbnail + Path.GetExtension(objBlogs.BlogThumbnail.FileName));
                    BlogThumbnail_image_webpath = "/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString() + BlogThumbnail + Path.GetExtension(objBlogs.BlogThumbnail.FileName);
                }
                if (objBlogs.BlogSmallThumbnail1 != null && objBlogs.BlogSmallThumbnail1.ContentLength > 0)
                {
                    BlogSmallThumbnail1_path = HttpContext.Current.Server.MapPath("~/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString() + BlogSmallThumbnail1 + Path.GetExtension(objBlogs.BlogSmallThumbnail1.FileName));
                    BlogSmallThumbnail1_webpath = "/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString() + BlogSmallThumbnail1 + Path.GetExtension(objBlogs.BlogSmallThumbnail1.FileName);
                }
                if (objBlogs.BlogSmallThumbnail2 != null && objBlogs.BlogSmallThumbnail2.ContentLength > 0)
                {
                    BlogSmallThumbnail2_path = HttpContext.Current.Server.MapPath("~/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString() + BlogSmallThumbnail2 + Path.GetExtension(objBlogs.BlogSmallThumbnail2.FileName));
                    BlogSmallThumbnail2_webpath = "/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString() + BlogSmallThumbnail2 + Path.GetExtension(objBlogs.BlogSmallThumbnail2.FileName);
                }
                if (objBlogs.BlogSmallThumbnail3 != null && objBlogs.BlogSmallThumbnail3.ContentLength > 0)
                {
                    BlogSmallThumbnail3_path = HttpContext.Current.Server.MapPath("~/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString() + BlogSmallThumbnail3 + Path.GetExtension(objBlogs.BlogSmallThumbnail3.FileName));
                    BlogSmallThumbnail3_webpath = "/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString() + BlogSmallThumbnail3 + Path.GetExtension(objBlogs.BlogSmallThumbnail3.FileName);
                }

                using (SqlConnection connection = new SqlConnection(con))
                {
                    using (SqlCommand command = new SqlCommand("usp_AddBlogs", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BlogTitle", objBlogs.BlogTitle);
                        command.Parameters.AddWithValue("@OnelinerDiscription", objBlogs.OnelinerDiscription);
                        command.Parameters.AddWithValue("@Quote", objBlogs.Quote);
                        command.Parameters.AddWithValue("@FullDiscription", objBlogs.FullDiscription);
                        command.Parameters.AddWithValue("@SubTitle", objBlogs.SubTitle);
                        command.Parameters.AddWithValue("@SubTitleDiscription", objBlogs.SubTitleDiscription);
                        command.Parameters.AddWithValue("@BlogThumbnail", BlogThumbnail_image_webpath);
                        command.Parameters.AddWithValue("@BlogSmallThumbnail1", BlogSmallThumbnail1_webpath);
                        command.Parameters.AddWithValue("@BlogSmallThumbnail2", BlogSmallThumbnail2_webpath);
                        command.Parameters.AddWithValue("@BlogSmallThumbnail3", BlogSmallThumbnail3_webpath);
                        command.Parameters.AddWithValue("@CreatedBy", objBlogs.CreatedBy);
                        command.Parameters.AddWithValue("@SEOTitle", objBlogs.SEOTitle);
                        command.Parameters.AddWithValue("@SEOMetaDiscription", objBlogs.SEOMetaDiscription);
                        command.Parameters.AddWithValue("@SEOMetaKeywords", objBlogs.SEOMetaKeywords);
                        command.Parameters.AddWithValue("@WebsiteLink", objBlogs.WebsiteLink);
                        Result = (string)command.ExecuteScalar();
                        connection.Close();

                        if (objBlogs.BlogThumbnail != null && objBlogs.BlogThumbnail.ContentLength > 0)
                        {
                            objBlogs.BlogThumbnail.SaveAs(BlogThumbnail_image_path);
                        }
                        if (objBlogs.BlogSmallThumbnail1 != null && objBlogs.BlogSmallThumbnail1.ContentLength > 0)
                        {
                            objBlogs.BlogSmallThumbnail1.SaveAs(BlogSmallThumbnail1_path);
                        }
                        if (objBlogs.BlogSmallThumbnail2 != null && objBlogs.BlogSmallThumbnail2.ContentLength > 0)
                        {
                            objBlogs.BlogSmallThumbnail2.SaveAs(BlogSmallThumbnail2_path);
                        }
                        if (objBlogs.BlogSmallThumbnail3 != null && objBlogs.BlogSmallThumbnail3.ContentLength > 0)
                        {
                            objBlogs.BlogSmallThumbnail3.SaveAs(BlogSmallThumbnail3_path);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Common.errorlog(Convert.ToString(Ex.Message) + " Inner Msg:" + Convert.ToString(Ex.StackTrace), "AddBlog", "BlogOperations");
            }
            return Result;
        }

        public List<Blogs> ViewBlogs(int id, string Type,string slug=null)
        {
            List<Blogs> lstBlogs = new List<Blogs>();
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection connection = new SqlConnection(con))
                {
                    using (SqlCommand command = new SqlCommand("usp_getBlogs", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id == 0 ? (object)DBNull.Value : id);
                        command.Parameters.AddWithValue("@Type", Type);
                        command.Parameters.AddWithValue("@slug", slug);
                        
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            connection.Open();
                            adapter.Fill(dt);
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    lstBlogs = dt.AsEnumerable().Select(row =>
                 new Blogs
                 {
                     Id = Convert.ToInt32(row["Id"]),
                     BlogTitle = Convert.ToString(row["BlogTitle"]),
                     OnelinerDiscription = Convert.ToString(row["OnelinerDiscription"]),
                     Quote = Convert.ToString(row["Quote"]),
                     FullDiscription = Convert.ToString(row["FullDiscription"]),
                     SubTitle = Convert.ToString(row["SubTitle"]),
                     SubTitleDiscription = Convert.ToString(row["SubTitleDiscription"]),
                     BlogThumbnailImagePath = Convert.ToString(row["BlogThumbnail"]),
                     BlogSmallThumbnail1ImagePath = Convert.ToString(row["BlogSmallThumbnail1"]),
                     BlogSmallThumbnail2ImagePath = Convert.ToString(row["BlogSmallThumbnail2"]),
                     BlogSmallThumbnail3ImagePath = Convert.ToString(row["BlogSmallThumbnail3"]),
                     BlogVerified = Convert.ToInt32(row["BlogVerified"]),
                     CreatedBy = Convert.ToString(row["CreatedBy"]),
                     CreatedDate = Convert.ToString(row["CreatedDate"]),
                     SEOTitle = Convert.ToString(row["SEOTitle"]),
                     SEOMetaDiscription = Convert.ToString(row["SEOMetaDiscription"]),
                     SEOMetaKeywords = Convert.ToString(row["SEOMetaKeywords"]),
                     WebsiteLink = Convert.ToString(row["WebsiteLink"]),
                 }).ToList();
                }
            }
            catch (Exception Ex)
            {
                Common.errorlog(Convert.ToString(Ex.Message) + " Inner Msg:" + Convert.ToString(Ex.StackTrace), "ViewBlogs", "BlogOperations");
            }
            return lstBlogs;
        }

        public string DeleteBlog(int id)
        {
            string Result = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(con))
                {
                    using (SqlCommand command = new SqlCommand("usp_DeleteBlog", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);
                        Result = (string)command.ExecuteScalar();
                        connection.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                Common.errorlog(Convert.ToString(Ex.Message) + " Inner Msg:" + Convert.ToString(Ex.StackTrace), "DeleteBlog", "BlogOperations");
            }
            return Result;
        }

        public string EditBlog(Blogs objBlogs)
        {
            string Result = string.Empty;
            try
            {
                string BlogThumbnail = Convert.ToString(Guid.NewGuid());
                string BlogSmallThumbnail1 = Convert.ToString(Guid.NewGuid());
                string BlogSmallThumbnail2 = Convert.ToString(Guid.NewGuid());
                string BlogSmallThumbnail3 = Convert.ToString(Guid.NewGuid());
                string BlogThumbnail_image_path = string.Empty;
                string BlogThumbnail_image_webpath = string.Empty;
                string BlogSmallThumbnail1_path = string.Empty;
                string BlogSmallThumbnail1_webpath = string.Empty;
                string BlogSmallThumbnail2_path = string.Empty;
                string BlogSmallThumbnail2_webpath = string.Empty;
                string BlogSmallThumbnail3_path = string.Empty;
                string BlogSmallThumbnail3_webpath = string.Empty;

                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString())))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString()));
                }

                if (objBlogs.BlogThumbnail != null && objBlogs.BlogThumbnail.ContentLength > 0)
                {
                    BlogThumbnail_image_path = HttpContext.Current.Server.MapPath("~/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString() + BlogThumbnail + Path.GetExtension(objBlogs.BlogThumbnail.FileName));
                    BlogThumbnail_image_webpath = "/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString() + BlogThumbnail + Path.GetExtension(objBlogs.BlogThumbnail.FileName);
                }
                else
                {
                    BlogThumbnail_image_webpath = objBlogs.BlogThumbnailImagePath;
                }
                if (objBlogs.BlogSmallThumbnail1 != null && objBlogs.BlogSmallThumbnail1.ContentLength > 0)
                {
                    BlogSmallThumbnail1_path = HttpContext.Current.Server.MapPath("~/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString() + BlogSmallThumbnail1 + Path.GetExtension(objBlogs.BlogSmallThumbnail1.FileName));
                    BlogSmallThumbnail1_webpath = "/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString() + BlogSmallThumbnail1 + Path.GetExtension(objBlogs.BlogSmallThumbnail1.FileName);
                }
                else
                {
                    BlogSmallThumbnail1_webpath = objBlogs.BlogSmallThumbnail1ImagePath;
                }
                if (objBlogs.BlogSmallThumbnail2 != null && objBlogs.BlogSmallThumbnail2.ContentLength > 0)
                {
                    BlogSmallThumbnail2_path = HttpContext.Current.Server.MapPath("~/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString() + BlogSmallThumbnail2 + Path.GetExtension(objBlogs.BlogSmallThumbnail2.FileName));
                    BlogSmallThumbnail2_webpath = "/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString() + BlogSmallThumbnail2 + Path.GetExtension(objBlogs.BlogSmallThumbnail2.FileName);
                }
                else
                {
                    BlogSmallThumbnail2_webpath = objBlogs.BlogSmallThumbnail2ImagePath;
                }
                if (objBlogs.BlogSmallThumbnail3 != null && objBlogs.BlogSmallThumbnail3.ContentLength > 0)
                {
                    BlogSmallThumbnail3_path = HttpContext.Current.Server.MapPath("~/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString() + BlogSmallThumbnail3 + Path.GetExtension(objBlogs.BlogSmallThumbnail3.FileName));
                    BlogSmallThumbnail3_webpath = "/Areas/Admin/BlogImages/" + HttpContext.Current.Session["AdminUserId"].ToString() + BlogSmallThumbnail3 + Path.GetExtension(objBlogs.BlogSmallThumbnail3.FileName);
                }
                else
                {
                    BlogSmallThumbnail3_webpath = objBlogs.BlogSmallThumbnail3ImagePath;
                }

                using (SqlConnection connection = new SqlConnection(con))
                {
                    using (SqlCommand command = new SqlCommand("usp_EditBlogs", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BlogTitle", objBlogs.BlogTitle);
                        command.Parameters.AddWithValue("@OnelinerDiscription", objBlogs.OnelinerDiscription);
                        command.Parameters.AddWithValue("@Quote", objBlogs.Quote);
                        command.Parameters.AddWithValue("@FullDiscription", objBlogs.FullDiscription);
                        command.Parameters.AddWithValue("@SubTitle", objBlogs.SubTitle);
                        command.Parameters.AddWithValue("@SubTitleDiscription", objBlogs.SubTitleDiscription);
                        command.Parameters.AddWithValue("@BlogThumbnail", BlogThumbnail_image_webpath);
                        command.Parameters.AddWithValue("@BlogSmallThumbnail1", BlogSmallThumbnail1_webpath);
                        command.Parameters.AddWithValue("@BlogSmallThumbnail2", BlogSmallThumbnail2_webpath);
                        command.Parameters.AddWithValue("@BlogSmallThumbnail3", BlogSmallThumbnail3_webpath);
                        command.Parameters.AddWithValue("@ModifyBy", objBlogs.CreatedBy);
                        command.Parameters.AddWithValue("@Id", objBlogs.Id);
                        command.Parameters.AddWithValue("@SEOTitle", objBlogs.SEOTitle);
                        command.Parameters.AddWithValue("@SEOMetaDiscription", objBlogs.SEOMetaDiscription);
                        command.Parameters.AddWithValue("@SEOMetaKeywords", objBlogs.SEOMetaKeywords);
                        command.Parameters.AddWithValue("@WebsiteLink", objBlogs.WebsiteLink);

                        Result = (string)command.ExecuteScalar();
                        connection.Close();

                        if (objBlogs.BlogThumbnail != null && objBlogs.BlogThumbnail.ContentLength > 0)
                        {
                            objBlogs.BlogThumbnail.SaveAs(BlogThumbnail_image_path);
                        }
                        if (objBlogs.BlogSmallThumbnail1 != null && objBlogs.BlogSmallThumbnail1.ContentLength > 0)
                        {
                            objBlogs.BlogSmallThumbnail1.SaveAs(BlogSmallThumbnail1_path);
                        }
                        if (objBlogs.BlogSmallThumbnail2 != null && objBlogs.BlogSmallThumbnail2.ContentLength > 0)
                        {
                            objBlogs.BlogSmallThumbnail2.SaveAs(BlogSmallThumbnail2_path);
                        }
                        if (objBlogs.BlogSmallThumbnail3 != null && objBlogs.BlogSmallThumbnail3.ContentLength > 0)
                        {
                            objBlogs.BlogSmallThumbnail3.SaveAs(BlogSmallThumbnail3_path);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Common.errorlog(Convert.ToString(Ex.Message) + " Inner Msg:" + Convert.ToString(Ex.StackTrace), "EditBlog", "BlogOperations");
            }
            return Result;
        }

        public List<Blogs> GetBlogs()
        {
            List<Blogs> lstBlogs = new List<Blogs>();
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection connection = new SqlConnection(con))
                {
                    using (SqlCommand command = new SqlCommand("usp_GetBlogDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            connection.Open();
                            adapter.Fill(dt);
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    lstBlogs = dt.AsEnumerable().Select(row =>
                 new Blogs
                 {
                     Id = Convert.ToInt32(row["Id"]),
                     BlogTitle = Convert.ToString(row["BlogTitle"]),
                     OnelinerDiscription = Convert.ToString(row["OnelinerDiscription"]),
                     Quote = Convert.ToString(row["Quote"]),
                     FullDiscription = Convert.ToString(row["FullDiscription"]),
                     SubTitle = Convert.ToString(row["SubTitle"]),
                     SubTitleDiscription = Convert.ToString(row["SubTitleDiscription"]),
                     BlogThumbnailImagePath = Convert.ToString(row["BlogThumbnail"]),
                     BlogSmallThumbnail1ImagePath = Convert.ToString(row["BlogSmallThumbnail1"]),
                     BlogSmallThumbnail2ImagePath = Convert.ToString(row["BlogSmallThumbnail2"]),
                     BlogSmallThumbnail3ImagePath = Convert.ToString(row["BlogSmallThumbnail3"]),
                     BlogVerified = Convert.ToInt32(row["BlogVerified"]),
                     CreatedBy = Convert.ToString(row["CreatedBy"]),
                     CreatedDate = Convert.ToString(row["CreatedDate"]),
                     slug= Convert.ToString(row["WebsiteLink"]).Replace(" ","-"),
                     SEOTitle = Convert.ToString(row["SEOTitle"]),
                     SEOMetaDiscription = Convert.ToString(row["SEOMetaDiscription"]),
                     SEOMetaKeywords = Convert.ToString(row["SEOMetaKeywords"]),
                     WebsiteLink = Convert.ToString(row["WebsiteLink"]),
                 }).ToList();
                }
            }
            catch (Exception Ex)
            {
                Common.errorlog(Convert.ToString(Ex.Message) + " Inner Msg:" + Convert.ToString(Ex.StackTrace), "GetBlogs", "BlogOperations");
            }
            return lstBlogs;
        }

    }
}