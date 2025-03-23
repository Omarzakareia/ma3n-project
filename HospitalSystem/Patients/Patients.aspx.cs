using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HospitalSystem.Services;
using Telerik.Web.UI;
using Telerik.Web.UI.Skins;

namespace HospitalSystem.Patients
{
    public partial class Patients : System.Web.UI.Page
    {
        private int? userId = null; 
        private int? staffId = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                SetUserAndStaffID();
            }
        }
        private void SetUserAndStaffID()
        {
            try
            {
                HttpCookie myCookie = Request.Cookies["cooklogin"];
                if (myCookie != null && int.TryParse(myCookie["userId"], out int parsedUserId))
                {
                    userId = parsedUserId;
                }
                else
                {
                    Response.Write("<script>alert('Error: Unable to retrieve user ID!'); window.location.href='" + ResolveUrl("~/Login.aspx") + "';</script>");
                    Response.End();
                    return;
                }

                using (var db = DbService.Instance.GetDbContext())
                {
                    var staff = db.Staffs.FirstOrDefault(s => s.UserID == userId);
                    if (staff != null)
                    {
                        staffId = staff.StaffID;
                    }
                    else
                    {
                        Response.Write("<script>alert('Error: No staff record found for this user!'); window.location.href='" + ResolveUrl("~/Default.aspx") + "';</script>");
                        Response.End();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
                Response.End();
            }
        }

        #region Buttons Clicks
        protected void btnResetSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            RadGridActive.Rebind();
        }
        protected void btnAddPatient_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = DbService.Instance.GetDbContext())
                {
                    // Get the staff ID for the logged-in user
                    var staff = db.Staffs.FirstOrDefault(s => s.UserID == userId);
                    
                    var newPatient = new Patient
                    {
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        Gender = txtGender.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        StaffID = staff.StaffID,
                        Address = txtAddress.Text.Trim(),
                        CreatedAt = DateTime.Now,
                        IsDeleted = false
                    };

                    db.Patients.Add(newPatient);
                    db.SaveChanges();

                    Response.Write("<script>alert('Patient added successfully!');</script>");
                    RadGridActive.Rebind();
                }
            }
            catch (Exception ex)
            {
                // Handle error
                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            using (var db = DbService.Instance.GetDbContext())
            {
                var patientData = db.PatientInfoes
                    .Where(p => p.FullName.ToLower().Contains(searchText))
                    .ToList();

                RadGridActive.DataSource = patientData;
                RadGridActive.DataBind();
            }
        }
        protected void btnToggleView_Click(object sender, EventArgs e)
        {
            // Toggle visibility of panels
            pnlActivePatients.Visible = !pnlActivePatients.Visible;
            pnlDeletedPatients.Visible = !pnlDeletedPatients.Visible;

            if (pnlDeletedPatients.Visible)
            {
                RadGridDeleted.Rebind();
            }
            else
            {
                RadGridActive.Rebind();
            }

            // Change button text
            btnToggleView.Text = pnlActivePatients.Visible ? "Show Deleted Patients" : "Show Active Patients";
        }
        #endregion

        #region CRUD
        protected void RadGridActive_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item != null)
            {
                int patientId = Convert.ToInt32(item.GetDataKeyValue("PatientID"));
                Response.Write("<script>alert('Patient ID: " + patientId + "');</script>"); // 🔹 Debugging step

                using (var db = DbService.Instance.GetDbContext())
                {
                    var patient = db.Patients.FirstOrDefault(p => p.PatientID == patientId);
                    if (patient != null)
                    {
                        patient.IsDeleted = true;
                        patient.DeletedBy = userId;
                        patient.DeletedAt = DateTime.Now;
                        db.SaveChanges();

                        Response.Write("<script>alert('Marked as deleted in DB!');</script>"); // 🔹 Debugging step

                    }
                    else
                    {
                        Response.Write("<script>alert('Error: Patient not found!');</script>");
                    }
                }
            }
        }

        protected void RadGridActive_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem != null)
            {
                // Get PatientID (Primary Key)
                int patientId = Convert.ToInt32(editedItem.GetDataKeyValue("PatientID"));

                // Retrieve updated values from the edit form
                string fullName = (editedItem["FullNameColumn"].FindControl("txtFullName") as RadTextBox)?.Text.Trim();
                string phone = (editedItem["Phone"].Controls[0] as TextBox).Text.Trim();

                // Split full name into first and last name
                string firstName = "";
                string lastName = "";

                if (!string.IsNullOrEmpty(fullName))
                {
                    string[] nameParts = fullName.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    firstName = nameParts.Length > 0 ? nameParts[0] : "";
                    lastName = nameParts.Length > 1 ? nameParts[1] : "";
                }

                using (var db = DbService.Instance.GetDbContext())
                {
                    var patient = db.Patients.FirstOrDefault(p => p.PatientID == patientId);
                    if (patient != null)
                    {

                        patient.FirstName = firstName;
                        patient.LastName = lastName;
                        patient.Phone = phone;

                        db.SaveChanges();
                    }
                }
            }
        }

        protected void RadGridDeleted_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "RestorePatient")
            {
                int patientId = Convert.ToInt32(e.CommandArgument);

                using (var db = DbService.Instance.GetDbContext())
                {
                    var patient = db.Patients.FirstOrDefault(p => p.PatientID == patientId);
                    if (patient != null)
                    {
                        patient.IsDeleted = false;
                        patient.DeletedBy = null;
                        patient.DeletedAt = null;
                        db.SaveChanges();

                        // Refresh grids
                        RadGridDeleted.Rebind();
                        RadGridActive.Rebind();

                        Response.Write("<script>alert('Patient restored successfully!');</script>");
                    }
                }
            }
        }
        #endregion

        #region DataSource
        protected void RadGridDeleted_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                using (var db = DbService.Instance.GetDbContext())
                {
                    var deletedPatients = db.DeletedPatientViews.ToList(); // Fetch from View
                    RadGridDeleted.DataSource = deletedPatients;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
            }
        }
        protected void RadGridActive_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                using (var db = DbService.Instance.GetDbContext())
                {
                    var patientData = db.PatientInfoes.ToList(); // Fetch from view
                    RadGridActive.DataSource = patientData;
                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
            }
        }
        #endregion

    }
}