using System;
using System.Collections.Generic;
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
            HttpCookie myCookie = new HttpCookie("cooklogin");
            myCookie = Request.Cookies["cooklogin"];
            if (myCookie == null)
            {
                lstWelcome.Visible = false;
                lstLog.Visible = true;
            }
            else
            {
                lstWelcome.Visible = true;
                lstLog.Visible = false;
                btnUser.Text = HttpUtility.UrlDecode(myCookie["name"].ToUpper());
            }

        }
        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["cooklogin"] != null)
            {
                // Create a new cookie with the same name and set it to expire
                HttpCookie myCookie = new HttpCookie("cooklogin");
                myCookie.Expires = DateTime.Now.AddDays(-1); // Expire it in the past
                Response.Cookies.Add(myCookie);
                Response.Redirect("Default.aspx");
            }

            Response.Redirect("Default.aspx");
        }
    }
}