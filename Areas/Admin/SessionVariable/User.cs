using CoursesHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesHub.Areas.Admin.SessionVariable
{
    public sealed class Users
    {
        #region Singleton

        private const string SESSION_SINGLETON_NAME =
            "Singleton_502E69E5-668B-E011-951F-00155DF26207";

        private Users()
        {
        }

        public static void ClearIndividualSession()
        {
            HttpContext.Current.Session["Singleton_502E69E5-668B-E011-951F-00155DF26207"] = null;
        }

        public static Users Current
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_SINGLETON_NAME] == null)
                {
                    HttpContext.Current.Session[SESSION_SINGLETON_NAME] = new Users();
                }

                return HttpContext.Current.Session[SESSION_SINGLETON_NAME] as Users;
            }
        }

        public bool IsAlreadyRegisterd()
        {
            if (Current.AdminUser != null && Current.AdminUser.userId != null)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        #endregion

        public User AdminUser { get; set; }
        public int AdminUserId { get; set; }

    }

}