using CoursesHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoursesHub.Areas.Panel.PanelSessionVariable
{
    public class PanelSessionvar
    {
        #region Singleton

        private const string SESSION_SINGLETON_NAME =
            "Singleton_502E69E5-668B-E011-951F-00155DF26207";

        private PanelSessionvar()
        {
        }

        public static void ClearIndividualSession()
        {
            HttpContext.Current.Session["Singleton_502E69E5-668B-E011-951F-00155DF26207"] = null;
        }

        public static PanelSessionvar Current
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_SINGLETON_NAME] == null)
                {
                    HttpContext.Current.Session[SESSION_SINGLETON_NAME] = new PanelSessionvar();
                }

                return HttpContext.Current.Session[SESSION_SINGLETON_NAME] as PanelSessionvar;
            }
        }

        public bool IsAlreadyRegisterd()
        {
            if (Current.SessionRegistration != null)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

        #endregion

        public Registration SessionRegistration { get; set; }
    }
}