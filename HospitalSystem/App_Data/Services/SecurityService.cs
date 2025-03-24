using System;
using System.Linq;
using System.Web;

namespace HospitalSystem.App_Data
{
    public class SecurityService
    {

        public static void CheckUserAccess(params string[] allowedRoles)
        {
            HttpCookie myCookie = HttpContext.Current.Request.Cookies["cooklogin"];
            if (myCookie == null)
            {
                HttpContext.Current.Response.Redirect("~/Login.aspx");
                return;
            }

            string userRole = myCookie["Role"]?.ToUpper();
            if (string.IsNullOrEmpty(userRole) || !allowedRoles.Contains(userRole))
            {
                HttpContext.Current.Response.Redirect("~/Unauthorized.aspx");
            }
        }

    }
}
