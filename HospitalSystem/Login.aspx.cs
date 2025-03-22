using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using HospitalSystem.Services;

namespace HospitalSystem
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly AuthenticationService _authService = new AuthenticationService();
        private int MaxFailedAttempts;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadMaxFailedAttempts();
        }

        private void LoadMaxFailedAttempts()
        {
            HospitalEntities _context = new HospitalEntities();
            MaxFailedAttempts = Application["MaxFailedAttempts"] as int?
                                ?? _context.Settings.Select(s => (int?)s.MaxFailedAttempts).FirstOrDefault()
                                ?? 5;

            Application["MaxFailedAttempts"] = MaxFailedAttempts;
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();

                lblError.Visible = false;

                if (_authService.IsAccountLocked(email))
                {
                    lblError.Text = "Your account is locked due to multiple failed login attempts.";
                    lblError.Visible = true;
                    return;
                }

                if (_authService.AuthenticateUser(email, password, out int userId, out string role))
                {
                    _authService.ResetFailedLogins(email);
                    _authService.SetAuthCookie(email, role, userId, chkRememberMe.Checked);
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    _authService.IncrementFailedLogins(email, MaxFailedAttempts);
                    lblError.Text = "Invalid email or password.";
                    lblError.Visible = true;
                }
            }
        }
    }
}
