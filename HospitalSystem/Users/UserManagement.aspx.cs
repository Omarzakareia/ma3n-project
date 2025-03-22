using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace HospitalSystem.Users
{
    public partial class UserManagement : System.Web.UI.Page
    {
        private readonly HospitalEntities _context = new HospitalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie myCookie = Request.Cookies["cooklogin"];
                if (myCookie == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    string userRole = myCookie["Role"].ToUpper(); 

                    if (string.IsNullOrEmpty(userRole) || userRole != "ADMIN")
                    {
                        Response.Redirect("~/Unauthorized.aspx"); 
                    }                   
                    LoadMaxAttempts();
                }
            }
        }




        private void LoadMaxAttempts() => txtMaxAttempts.Text = _context.Settings.FirstOrDefault()?.MaxFailedAttempts.ToString() ?? "5";
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            using (_context)
            {
                var query = _context.UserInfoViews.AsQueryable();

                if (!string.IsNullOrEmpty(searchText))
                {
                    query = query.Where(u => u.FullName.StartsWith(searchText));
                }

                RadGrid1.DataSource = query.ToList();
                RadGrid1.DataBind();
            }
        }
        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            RadGrid1.Rebind();
        }
        protected void btnSaveAttempts_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtMaxAttempts.Text, out int maxAttempts) && maxAttempts > 0)
            {
                var setting = _context.Settings.FirstOrDefault() ?? _context.Settings.Add(new Setting());
                setting.MaxFailedAttempts = maxAttempts;
                _context.SaveChanges();
                lblSaveStatus.Text = "Max failed attempts updated!";
                lblSaveStatus.Visible = true;
                Application["MaxFailedAttempts"] = maxAttempts;
            }
        }
        protected void btnActiveUsers_Click(object sender, EventArgs e) { RadMultiPage1.SelectedIndex = 0; RadGrid1.Rebind(); }
        protected void btnDeletedUsers_Click(object sender, EventArgs e) { RadMultiPage1.SelectedIndex = 1;RadGridDeletedUsers.Rebind(); }
        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            RadGrid1.MasterTableView.IsItemInserted = true;
            RadGrid1.Rebind();
        }
        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e) {
            RadGrid1.DataSource = _context.UserInfoViews.ToList();
            var specialityColumn = RadGrid1.MasterTableView.GetColumn("Speciality");
            specialityColumn.Visible = false; // Ensure it's hidden by default

        }
        protected void RadGridDeletedUsers_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGridDeletedUsers.DataSource = _context.DeletedUsersViews.ToList();
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem item && int.TryParse(item.GetDataKeyValue("UserID")?.ToString(), out int userId))
            {
                var user = _context.Users.FirstOrDefault(u => u.UserID == userId);
                if (user != null)
                {
                    user.IsDeleted = true;
                    user.DeletedBy = GetCurrentUserID();
                    user.DeletedAt = DateTime.Now;
                    _context.SaveChanges();
                    RadGrid1.Rebind();
                }
            }
        }
        protected void RadGrid1_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridEditableItem editedItem && int.TryParse(editedItem.GetDataKeyValue("UserID")?.ToString(), out int userId))
            {
                var user = _context.Users.Include("Role").FirstOrDefault(u => u.UserID == userId);
                if (user != null)
                {
                    user.Username = (editedItem["Username"].Controls[0] as TextBox)?.Text?.Trim();
                    user.PasswordHash = (editedItem["Password"].Controls[0] as TextBox)?.Text?.Trim();
                    user.FullName = (editedItem["FullName"].Controls[0] as TextBox)?.Text?.Trim();
                    user.Email = (editedItem["Email"].Controls[0] as TextBox)?.Text?.Trim();
                    user.Phone = (editedItem["Phone"].Controls[0] as TextBox)?.Text?.Trim();

                    // Handle Role dropdown
                    var ddlRole = editedItem.FindControl("ddlRole") as RadDropDownList;
                    string selectedRole = ddlRole?.SelectedValue;

                    if (!string.IsNullOrEmpty(selectedRole))
                    {
                        var role = _context.Roles.FirstOrDefault(r => r.RoleName == selectedRole);
                        if (role != null) user.RoleID = role.RoleID;
                    }

                    // Handle IsLocked dropdown
                    var ddlIsLocked = editedItem.FindControl("ddlIsLocked") as RadDropDownList;
                    if (ddlIsLocked != null)
                    {
                        user.IsLocked = bool.Parse(ddlIsLocked.SelectedValue);
                        // If the user is unlocked, reset FailedLogins to 0
                        if (!(bool)user.IsLocked)
                        {
                            user.FailedLogins = 0;
                        }
                    }

                    _context.SaveChanges();

                    // **Handle Specialty if the user is a Doctor**
                    if (selectedRole == "Doctor")
                    {
                        var ddlSpecialty = editedItem.FindControl("ddlSpecialty") as RadComboBox;
                        if (ddlSpecialty != null && !string.IsNullOrEmpty(ddlSpecialty.SelectedValue))
                        {
                            if (int.TryParse(ddlSpecialty.SelectedValue, out int specialtyId))
                            {
                                System.Diagnostics.Debug.WriteLine("Updating Speciality ID: " + specialtyId);

                                var existingDoctor = _context.Doctors.FirstOrDefault(d => d.UserID == userId);
                                if (existingDoctor != null)
                                {
                                    // Update existing specialty
                                    existingDoctor.SpecialityID = specialtyId;
                                }
                                else
                                {
                                    // If the doctor doesn't exist yet, insert a new record
                                    var newDoctor = new Doctor
                                    {
                                        UserID = user.UserID,
                                        SpecialityID = specialtyId
                                    };
                                    _context.Doctors.Add(newDoctor);
                                }

                                _context.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        // If the role is changed from Doctor to something else, remove the doctor entry
                        var existingDoctor = _context.Doctors.FirstOrDefault(d => d.UserID == userId);
                        if (existingDoctor != null)
                        {
                            _context.Doctors.Remove(existingDoctor);
                            _context.SaveChanges();
                        }
                    }

                    RadGrid1.Rebind();
                }
            }
        }

        protected void RadGrid1_InsertCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridEditableItem item)
            {
                // Get values from input fields
                string username = (item["Username"].Controls[0] as TextBox)?.Text.Trim();
                string fullName = (item["FullName"].Controls[0] as TextBox)?.Text.Trim();
                string email = (item["Email"].Controls[0] as TextBox)?.Text.Trim();
                string phone = (item["Phone"].Controls[0] as TextBox)?.Text.Trim();
                string password = (item["Password"].Controls[0] as TextBox)?.Text.Trim();

                // Get Role dropdown
                var ddlRole = item.FindControl("ddlRole") as RadDropDownList;
                int? roleId = null;
                string selectedRole = ddlRole?.SelectedValue;

                if (!string.IsNullOrEmpty(selectedRole))
                {
                    var roleEntity = _context.Roles.FirstOrDefault(r => r.RoleName == selectedRole);
                    if (roleEntity != null)
                    {
                        roleId = roleEntity.RoleID;
                    }
                }

                // Validate required fields
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(fullName) ||
                    string.IsNullOrEmpty(email) || roleId == null)
                {
                    return; // Stop insert if required fields are missing
                }

                // Get IsLocked dropdown
                var ddlIsLocked = item.FindControl("ddlIsLocked") as RadDropDownList;
                bool isLocked = ddlIsLocked != null && bool.TryParse(ddlIsLocked.SelectedValue, out bool locked) && locked;

                // Insert User
                var newUser = new User
                {
                    Username = username,
                    PasswordHash = password,
                    FullName = fullName,
                    Email = email,
                    Phone = phone,
                    RoleID = roleId.Value,
                    FailedLogins = 0,
                    IsLocked = isLocked,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                // **Handle Specialty if the user is a Doctor**
                if (ddlRole.SelectedValue == "Doctor")
                {
                    var ddlSpecialty = item.FindControl("ddlSpecialty") as RadComboBox;
                    if (ddlSpecialty != null && !string.IsNullOrEmpty(ddlSpecialty.SelectedValue))
                    {
                        // Parse SpecialityID directly from SelectedValue
                        if (int.TryParse(ddlSpecialty.SelectedValue, out int specialtyId))
                        {
                            System.Diagnostics.Debug.WriteLine("Parsed Speciality ID: " + specialtyId);

                            var newDoctor = new Doctor
                            {
                                UserID = newUser.UserID,
                                SpecialityID = specialtyId
                            };

                            _context.Doctors.Add(newDoctor);
                            _context.SaveChanges();
                        }
                    }
                }

                RadGrid1.Rebind();
            }
        }

        protected void ddlRole_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            var ddlRole = sender as RadDropDownList;
            var gridItem = ddlRole.NamingContainer as GridEditableItem;

            var ddlSpecialty = gridItem.FindControl("ddlSpecialty") as RadComboBox;
            if (ddlRole.SelectedValue == "Doctor")
            {
                ddlSpecialty.Visible = true;
            }
            else
            {
                ddlSpecialty.Visible = false;
            }
        }
        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem editItem && e.Item.IsInEditMode)
            {
                var ddlRole = editItem.FindControl("ddlRole") as RadDropDownList;
                var ddlSpecialty = editItem.FindControl("ddlSpecialty") as RadComboBox;

                if (ddlSpecialty != null)
                {
                    ddlSpecialty.DataSource = _context.Specialities.ToList();
                    ddlSpecialty.DataTextField = "SpecialityName";
                    ddlSpecialty.DataValueField = "SpecialityID";
                    ddlSpecialty.DataBind();
                    ddlSpecialty.Items.Insert(0, new RadComboBoxItem("-- Select Specialty --", ""));
                }

                // **Prevent crash when inserting a new user**
                if (editItem.OwnerTableView.IsItemInserted)
                {
                    ddlSpecialty.Visible = false; // Hide specialty by default for new users
                    return;
                }

                // **Check if editing an existing user**
                if (editItem.OwnerTableView.DataKeyNames.Contains("UserID") &&
                    editItem.GetDataKeyValue("UserID") != null)
                {
                    int userId = System.Convert.ToInt32(editItem.GetDataKeyValue("UserID"));
                    string selectedRole = ddlRole.SelectedValue;
                    bool isDoctor = selectedRole == "Doctor";

                    if (isDoctor)
                    {
                        ddlSpecialty.Visible = true;
                        var doctor = _context.Doctors.FirstOrDefault(d => d.UserID == userId);
                        if (doctor != null)
                        {
                            ddlSpecialty.SelectedValue = doctor.SpecialityID.ToString();
                        }
                    }
                    else
                    {
                        ddlSpecialty.Visible = false;
                    }

                    var specialityColumn = RadGrid1.MasterTableView.GetColumn("Speciality");
                    specialityColumn.Visible = isDoctor;
                }
            }
        }





        protected void RadGridDeletedUsers_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem item && int.TryParse(item.GetDataKeyValue("UserID")?.ToString(), out int userId))
            {
                var user = _context.Users.FirstOrDefault(u => u.UserID == userId);
                if (user != null)
                {
                    user.IsDeleted = false;
                    user.DeletedBy = null;
                    user.DeletedAt = null;
                    _context.SaveChanges();
                    RadGrid1.Rebind();
                    RadGridDeletedUsers.Rebind();
                }
            }
        }


    }
}
