using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using HospitalSystem; // Ensure this namespace includes your EF context

namespace HospitalSystem.Users
{
    public partial class UserManagement : System.Web.UI.Page
    {
        private readonly HospitalEntities _context = new HospitalEntities(); // Your EF DbContext

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMaxAttempts();
            }
        }

        private void LoadMaxAttempts()
        {
            var setting = _context.Settings.FirstOrDefault();
            txtMaxAttempts.Text = setting != null ? setting.MaxFailedAttempts.ToString() : "5";
        }

        protected void btnSaveAttempts_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtMaxAttempts.Text, out int maxAttempts) && maxAttempts > 0)
            {
                var setting = _context.Settings.FirstOrDefault();
                if (setting != null)
                {
                    setting.MaxFailedAttempts = maxAttempts;
                }
                else
                {
                    _context.Settings.Add(new Setting { MaxFailedAttempts = maxAttempts });
                }

                _context.SaveChanges();

                lblSaveStatus.Text = "Max failed attempts updated!";
                lblSaveStatus.Visible = true;
                Application["MaxFailedAttempts"] = maxAttempts; // Store globally
            }
            else
            {
                lblSaveStatus.Text = "Invalid input. Please enter a positive number.";
                lblSaveStatus.ForeColor = System.Drawing.Color.Red;
                lblSaveStatus.Visible = true;
            }
        }

        protected void RadGrid1_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is Telerik.Web.UI.GridDataItem item)
            {
                object dataKeyValue = item.GetDataKeyValue("UserID");

                if (dataKeyValue != null && int.TryParse(dataKeyValue.ToString(), out int userId))
                {
                    var user = _context.Users.FirstOrDefault(u => u.UserID == userId);
                    int currentUserId = GetCurrentUserID();

                    // Ensure the current user ID exists in the User table
                    bool isValidUser = _context.Users.Any(u => u.UserID == currentUserId);

                    if (user != null && isValidUser) // Only update if the current user exists
                    {
                        user.IsDeleted = true;
                        user.DeletedBy = currentUserId; // Ensure this user ID exists in DB
                        user.DeletedAt = DateTime.Now;

                        _context.SaveChanges();

                        e.Canceled = true; // Prevent Telerik from attempting a second delete
                    }
                }
            }

            // Force a refresh of the grid
            RadGrid1.Rebind();
        }

        // Get the currently logged-in user ID from cookies
        private int GetCurrentUserID()
        {
            int userId = 0;
            HttpCookie authCookie = Request.Cookies["cooklogin"];

            if (authCookie != null && authCookie["userId"] != null)
            {
                int.TryParse(authCookie["userId"], out userId);
            }

            return userId;
        }


    }
}