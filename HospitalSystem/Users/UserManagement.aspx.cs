using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace HospitalSystem.Users
{
    public partial class UserManagement : System.Web.UI.Page
    {
        private readonly HospitalEntities _context = new HospitalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDeletedUsers();
                LoadMaxAttempts();
            }
        }

        private void LoadUserData()
        {
            var users = _context.UserInfoViews.ToList();
            RadGrid1.DataSource = users.Count > 0 ? users : new List<UserInfoView>();
        }

        private void LoadDeletedUsers()
        {
            var deletedUsers = _context.DeletedUsersViews.ToList();
            RadGridDeletedUsers.DataSource = deletedUsers.Any() ? deletedUsers : new List<DeletedUsersView>();
            RadGridDeletedUsers.DataBind();
        }

        private void LoadMaxAttempts()
        {
            var setting = _context.Settings.FirstOrDefault();
            txtMaxAttempts.Text = setting?.MaxFailedAttempts.ToString() ?? "5";
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
                Application["MaxFailedAttempts"] = maxAttempts;
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
                    if (user != null)
                    {
                        user.IsDeleted = true;
                        user.DeletedBy = GetCurrentUserID();
                        user.DeletedAt = DateTime.Now;
                        _context.SaveChanges();

                        LoadUserData();
                        LoadDeletedUsers();
                    }
                }
            }
        }


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

        protected void btnActiveUsers_Click(object sender, EventArgs e)
        {
            RadMultiPage1.SelectedIndex = 0;
            LoadUserData();
        }

        protected void btnDeletedUsers_Click(object sender, EventArgs e)
        {
            RadMultiPage1.SelectedIndex = 1;
            LoadDeletedUsers();
        }

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            LoadUserData();
        }

        protected void RadGrid1_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is Telerik.Web.UI.GridEditableItem editedItem)
            {
                // Get the UserID from the DataKey
                object dataKeyValue = editedItem.GetDataKeyValue("UserID");

                if (dataKeyValue != null && int.TryParse(dataKeyValue.ToString(), out int userId))
                {
                    // Fetch user from database
                    var user = _context.Users
                        .Include("Role") // Use string version if needed
                        .FirstOrDefault(u => u.UserID == userId);

                    if (user != null)
                    {
                        // Extract new values from edited item
                        user.Username = (editedItem["Username"].Controls[0] as TextBox)?.Text?.Trim();
                        user.FullName = (editedItem["FullName"].Controls[0] as TextBox)?.Text?.Trim();
                        user.Email = (editedItem["Email"].Controls[0] as TextBox)?.Text?.Trim();
                        user.Phone = (editedItem["Phone"].Controls[0] as TextBox)?.Text?.Trim();

                        // Handle RoleName separately (ensure role exists)
                        string newRoleName = (editedItem["RoleName"].Controls[0] as TextBox)?.Text?.Trim();
                        if (!string.IsNullOrEmpty(newRoleName))
                        {
                            var role = _context.Roles.FirstOrDefault(r => r.RoleName == newRoleName);
                            if (role != null)
                            {
                                user.RoleID = role.RoleID; // Assign the existing Role ID
                            }
                        }

                        // Save changes to database
                        _context.SaveChanges();

                        // Rebind grid to reflect changes
                        LoadUserData();
                    }
                }
            }
        }
    }
}
