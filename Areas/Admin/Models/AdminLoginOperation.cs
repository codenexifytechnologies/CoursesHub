using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using CoursesHub.Models;

namespace CoursesHub.Areas.Admin.Models
{
    public class AdminLogin
    {
        [Required(ErrorMessage = "The Email ID field is required")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "The Password field is required")]
        public string Password { get; set; }
    }

    public class Roles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class AdminLoginOperation
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBCon"].ToString();
        SqlConnection connection;
        public User Login(AdminLogin objLogin)
        {
            User objUser = new User();
            try
            {
                DataTable userDataTable = new DataTable();
                using (connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("usp_AdminUserLogin", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmailId", objLogin.EmailId);
                        command.Parameters.AddWithValue("@Password", objLogin.Password);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            connection.Open();
                            adapter.Fill(userDataTable);
                        }
                    }
                }
                if (userDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in userDataTable.Rows)
                    {
                        objUser = new User
                        {
                            userId = Convert.ToInt32(row["userId"]),
                            Name = row["name"].ToString(),
                            EmailId = row["EmailId"].ToString(),
                            mobileNo = row["mobileNo"].ToString(),
                            RoleID = Convert.ToInt32(row["role"]),
                            RoleName = row["rolename"].ToString(),
                            Image = row["ImagePath"].ToString(),
                        };
                    }
                }
                else
                {
                    objUser.Message = "Login Failed";
                }
            }
            catch (Exception)
            {
                objUser.Message = "Login Failed";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return objUser;
        }

        #region UserRegistration

        public string InsertUpdateClient(User objUser, string Action)
        {
            try
            {
                string path = "";
                string Result = string.Empty;
                string PServerMapPath = string.Empty;
                string ProfileImage_guid = Convert.ToString(Guid.NewGuid());

                if (objUser.ProfileImage != null && objUser.ProfileImage.ContentLength > 0)
                {
                    string ProfileImageExtension = Path.GetExtension(objUser.ProfileImage.FileName);
                    string ProfileImagePath = "Areas/Admin/Uploads/";
                    PServerMapPath = HttpContext.Current.Server.MapPath("~/" + ProfileImagePath);
                    if (!Directory.Exists(PServerMapPath))
                    {
                        Directory.CreateDirectory(PServerMapPath);
                    }
                    ProfileImage_guid = "/" + ProfileImage_guid + ProfileImageExtension;
                    ProfileImagePath = ProfileImagePath + ProfileImage_guid;
                    PServerMapPath = PServerMapPath + ProfileImage_guid;
                    objUser.ProfileImage.SaveAs(PServerMapPath);
                    path = ConfigurationManager.AppSettings["BaseURL"].ToString() + PServerMapPath;


                    var filename = Path.GetFileName(objUser.ProfileImage.FileName);
                    string ProfileImage = Convert.ToString(Guid.NewGuid());

                    path = Path.Combine(HttpContext.Current.Server.MapPath("~/Areas/Admin/Uploads/"), filename);
                    objUser.ProfileImage.SaveAs(path);
                }

                using (connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("USP_INSERTUSERREGISTER", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ACTION", Action);
                        command.Parameters.AddWithValue("@UserId", objUser.userId);
                        command.Parameters.AddWithValue("@NAME", objUser.Name);
                        command.Parameters.AddWithValue("@EMAILID", objUser.EmailId);
                        command.Parameters.AddWithValue("@PASSWORD", objUser.Password);
                        command.Parameters.AddWithValue("@MOBILE", objUser.mobileNo);
                        command.Parameters.AddWithValue("@IMAGEPATH", path);
                        command.Parameters.AddWithValue("@Role", objUser.RoleID);

                        command.Connection.Open();
                        Result = (string)command.ExecuteScalar();
                    }
                }

                if (Result == "User Added Successfully" && Action == "I")
                {
                    string subject = Convert.ToString(ConfigurationManager.AppSettings["User_Registration"]);
                    subject = subject.Replace("{{user_name}}", objUser.Name);
                    string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Emails/User_Registration.html"));
                    body = body.Replace("{{user_name}}", objUser.Name);
                    body = body.Replace("{{user_id}}", objUser.EmailId);
                    body = body.Replace("{{password}}", objUser.Password);
                    body = body.Replace("{{login_link}}", Convert.ToString(ConfigurationManager.AppSettings["AdminLoginUrl"]));
                    Common.SendEmail(objUser.EmailId, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]), subject, body);
                }
                else if (Result == "User Updated Successfully" && Action == "U")
                {
                    string subject = Convert.ToString(ConfigurationManager.AppSettings["User_Detail_Updation"]);
                    subject = subject.Replace("{{user_name}}", objUser.Name);
                    string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Emails/User_Detail_Updation.html"));
                    body = body.Replace("{{admin_name}}", objUser.Name);
                    body = body.Replace("{{profile_link}}", Convert.ToString(ConfigurationManager.AppSettings["AdminLoginUrl"]));
                    Common.SendEmail(objUser.EmailId, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]), subject, body);
                }

                return Result;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return "User creation failed due to some technical issue, please try again later.";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public List<User> GetUserRegistrationDetails()
        {
            List<User> lstUser = new List<User>();
            User objUser = new User();
            try
            {
                DataTable userDataTable = new DataTable();
                using (connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("usp_GetUserRegistrationDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            connection.Open();
                            adapter.Fill(userDataTable);
                        }
                    }
                }
                if (userDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in userDataTable.Rows)
                    {
                        objUser = new User()
                        {
                            userId = Convert.ToInt32(row["userId"].ToString()),
                            Name = row["name"].ToString(),
                            EmailId = row["EmailId"].ToString(),
                            mobileNo = row["mobileNo"].ToString(),
                            RoleID = Convert.ToInt32(row["role"]),
                            RoleName = row["rolename"].ToString(),
                            isActive = row["isActive"].ToString() == "1" ? "Active" : "InActive",
                        };
                        lstUser.Add(objUser);
                    }

                }
                else
                {
                    objUser.Message = "Login Failed";
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return lstUser;
        }

        public List<User> GetUserRegistrationDetailsByID(int UserId)
        {
            List<User> lstUser = new List<User>();
            User objUser = new User();
            try
            {
                DataTable userDataTable = new DataTable();
                using (connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetUserRegistrationDetailsByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", UserId);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            connection.Open();
                            adapter.Fill(userDataTable);
                        }
                    }
                }
                if (userDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in userDataTable.Rows)
                    {
                        objUser = new User()
                        {
                            userId = Convert.ToInt32(row["userId"].ToString()),
                            Name = row["name"].ToString(),
                            EmailId = row["EmailId"].ToString(),
                            Password = row["Password"].ToString(),
                            mobileNo = row["mobileNo"].ToString(),
                            RoleID = Convert.ToInt32(row["role"]),
                            isActive = row["isActive"].ToString() == "1" ? "Active" : "InActive",
                            Image = row["ImagePath"].ToString(),
                        };
                        lstUser.Add(objUser);
                    }

                }
                else
                {
                    objUser.Message = "Login Failed";
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return lstUser;
        }

        public string UserActionRegistartion(int Id, string Action)
        {
            try
            {
                string Result = string.Empty;
                using (connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("usp_Operation_User", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Action", Action);
                        command.Parameters.AddWithValue("@UserId", Id);
                        command.Connection.Open();
                        Result = (string)command.ExecuteScalar();
                    }
                }
                return Result;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return "User Action failed due to some technical issue, please try again later.";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public List<Roles> getRole()
        {
            List<Roles> lstRoles = new List<Roles>();
            Roles objRoles = new Roles();
            try
            {
                DataTable userDataTable = new DataTable();
                using (connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("usp_get_role", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            connection.Open();
                            adapter.Fill(userDataTable);
                        }
                    }
                }
                if (userDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in userDataTable.Rows)
                    {
                        objRoles = new Roles()
                        {
                            RoleId = Convert.ToInt32(row["id"]),
                            RoleName = row["rolename"].ToString(),
                        };
                        lstRoles.Add(objRoles);
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return lstRoles;
        }

        #endregion

        #region ForgotPassword
        public string CheckUser(AdminLogin objLogin)
        {
            string result = string.Empty;
            User objUser = new User();
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("usp_CheckUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmailId", objLogin.EmailId);
                        command.Parameters.AddWithValue("@Password", objLogin.Password);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            connection.Open();
                            result = (string)command.ExecuteScalar();
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return result;
        }
        #endregion

        #region Change Password
        public string ChangePassword(ChangePassword objChangePassword, int UserId, string EmailId)
        {
            string Result = string.Empty;
            string strName = string.Empty;
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("usp_ChangePassword", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", UserId);
                        //command.Parameters.AddWithValue("@EmailId", EmailId);
                        command.Parameters.AddWithValue("@OldPassword", objChangePassword.oldPassword);
                        command.Parameters.AddWithValue("@Password", objChangePassword.RenterNewPassword);
                        command.Parameters.AddWithValue("@LoginType", "A");
                        command.Connection.Open();
                        Result = (string)command.ExecuteScalar();

                        if (Result.Contains(","))
                        {
                            var parts = Result.Split(',');
                            if (parts.Length > 1)
                            {
                                Result = parts[0];
                                strName = parts[1];
                            }
                        }

                    }
                }

                if (Result == "Password changed successfully.")
                {
                    string subject = Convert.ToString(ConfigurationManager.AppSettings["Change_Password"]);
                    string body = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Emails/Change_Password.html"));
                    body = body.Replace("{{admin_name}}", strName);
                    body = body.Replace("{{account_link}}", Convert.ToString(ConfigurationManager.AppSettings["AdminLoginUrl"]));
                    Common.SendEmail(EmailId, Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]), subject, body);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Result = "Failed to change pasword.";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return Result;
        }
        #endregion
    }
}