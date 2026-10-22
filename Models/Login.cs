using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoursesHub.Models
{
    
    public class Login
    {
        [Required(ErrorMessage = "The Email ID field is required")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "The Password field is required")]
        public string Password { get; set; }
        public string LoginType { get; set; }
    }

    #region Login Types
    /// <summary>
    /// Login types accepted by usp_UserLogin. "Student" and "University" are the values stored in
    /// Session["Registration"].LoginType, so they must not change.
    /// </summary>
    public static class LoginTypes
    {
        public const string Student = "Student";
        public const string University = "University";

        /// <summary>
        /// Maps a requested login type (route/query value or posted hidden field) to a supported type.
        /// Returns null when the value is not recognised.
        /// </summary>
        public static string Normalize(string loginType)
        {
            switch ((loginType ?? string.Empty).Trim().ToLowerInvariant())
            {
                case "student":
                    return Student;
                case "university":
                case "trainingprovider":
                case "provider":
                    return University;
                default:
                    return null;
            }
        }

        /// <summary>Value used in login URLs, e.g. /Login?type=TrainingProvider.</summary>
        public static string ToUrlType(string loginType)
        {
            return Normalize(loginType) == University ? "TrainingProvider" : "Student";
        }
    }
    #endregion

    #region Change Passsword
    public class ChangePassword
    {
        public int Id { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The Old password field is required")]
        public string oldPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The New password field is required")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The Re-enter new password field is required")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Re-enter new password doesn't match.Try again!!")]
        public string RenterNewPassword { get; set; }
    }
    #endregion

    public class User
    {
        public int userId { get; set; }

        [Required(ErrorMessage = "The Name field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The EmailId field is required")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "The Mobile No field is required")]
        public string mobileNo { get; set; }

        public string isAdmin { get; set; }

        public string Message { get; set; }

        [Required(ErrorMessage = "The Password field is required for login")]
        [RegularExpression(@"^(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Password must contain minimum 8 character atleat 1 uppercase,1 lower case,1 no and 1 special symbol.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string isActive { get; set; }

        public HttpPostedFileBase ProfileImage { get; set; }

        public string Image { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}