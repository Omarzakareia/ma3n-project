using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalSystem
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string currentPage = Path.GetFileName(Request.Url.AbsolutePath).ToLower();

                // Hide navbar items if on the Login page
                if (currentPage == "login.aspx")
                {
                    userManagementNav.Visible = false;
                    doctorsNav.Visible = false;
                    patientsNav.Visible = false;
                    reportsNav.Visible = false;
                    bookingNav.Visible = false;
                    lstWelcome.Visible = false;
                    return;
                }


                HttpCookie myCookie = Request.Cookies["cooklogin"];
                if (myCookie == null)
                {
                    lstWelcome.Visible = false;
                    lstLog.Visible = true;
                    return;
                }

                // Get user role from cookie
                string userRole = myCookie["Role"]?.ToUpper();
                if (string.IsNullOrEmpty(userRole))
                {
                    Response.Redirect("~/Unauthorized.aspx");
                    return;
                }

                // Set username in navbar
                lstWelcome.Visible = true;
                lstLog.Visible = false;
                btnUser.Text = HttpUtility.UrlDecode(myCookie["name"].ToUpper());

                // Control navbar access
                ControlNavbarAccess(userRole);

                // Restrict access to unauthorized pages
                RestrictPageAccess(userRole);
            }
        }

        private void ControlNavbarAccess(string userRole)
        {
            // Control navbar items based on role
            userManagementNav.Visible = (userRole == "ADMIN");
            doctorsNav.Visible = (userRole == "ADMIN" || userRole == "DOCTOR");
            patientsNav.Visible = (userRole == "ADMIN" || userRole == "STAFF");
            reportsNav.Visible = (userRole == "ADMIN"); // Adjust roles as needed
            bookingNav.Visible = (userRole == "ADMIN" || userRole == "STAFF"); // Adjust roles as needed


        }

        private void RestrictPageAccess(string userRole)
        {
            string currentPage = Path.GetFileName(Request.Url.AbsolutePath).ToLower();

            Dictionary<string, string[]> rolePageMapping = new Dictionary<string, string[]>
            {

                { "ADMIN", new string[] {
                    "default.aspx",
                    "usermanagement.aspx",
                    "departments.aspx",
                    "myappointments.aspx",
                    "patients.aspx",
                    "patientsreport.aspx",
                    "booking.aspx",
                    "unauthorized.aspx",
                    "billingsreport.aspx",
                    "patientsreport.aspx",
                    "appointmentsreport.aspx",
                    "billinghistory.aspx",
                    "invoice.aspx"
                } },
                { "DOCTOR", new string[] {
                    "default.aspx",
                    "departments.aspx",
                    "myappointments.aspx",
                    "unauthorized.aspx",
                    "appointmentsreport.aspx" 
                } },
                { "STAFF", new string[] {
                    "default.aspx",
                    "patients.aspx",
                    "patienthistory.aspx",
                    "patientsreport.aspx",
                    "booking.aspx",
                    "unauthorized.aspx",
                    "billingsreport.aspx",
                    "patientsreport.aspx",
                    "billinghistory.aspx",
                    "invoice.aspx"
                } }
            };

            if (!rolePageMapping.ContainsKey(userRole) || !rolePageMapping[userRole].Contains(currentPage))
            {
                Response.Redirect(ResolveUrl("~/Unauthorized.aspx"));
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["cooklogin"] != null)
            {
                HttpCookie myCookie = new HttpCookie("cooklogin");
                myCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(myCookie);
            }
            Response.Redirect(ResolveUrl("~/Default.aspx"));
        }
    }
}
