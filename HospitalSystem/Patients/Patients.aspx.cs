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
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void txtSearch_TextChanged(object sender, EventArgs e)
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

        protected void btnToggleView_Click(object sender, EventArgs e)
        {
            // Toggle visibility of panels
            pnlActivePatients.Visible = !pnlActivePatients.Visible;
            pnlDeletedPatients.Visible = !pnlDeletedPatients.Visible;

            // Change button text
            btnToggleView.Text = pnlActivePatients.Visible ? "Show Deleted Patients" : "Show Active Patients";
        }
        protected void RadGridActive_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem != null)
            {
                // Get PatientID (Primary Key)
                int patientId = Convert.ToInt32(editedItem.GetDataKeyValue("PatientID"));

                // Retrieve updated values from the edit form
                string fullName = (editedItem["FullName"].Controls[0] as TextBox).Text.Trim();
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
                        // Update fields
                        patient.FirstName = firstName;
                        patient.LastName = lastName;
                        patient.Phone = phone;

                        db.SaveChanges(); // Save changes to DB
                    }
                }
            }
        }


        protected void RadGridDeleted_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                using (var db = DbService.Instance.GetDbContext())
                {
                    var deletedPatients = db.DeletedPatientViews.ToList(); // Fetch from View
                    RadGridDeleted.DataSource = deletedPatients;
                    RadGridDeleted.DataBind();
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
        protected void btnAddPatient_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = DbService.Instance.GetDbContext())
                {
                    int defaultStaffId = 1;
                    // Create a new Patient object
                    var newPatient = new Patient
                    {
                        PatientID = 29,
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        Gender = txtGender.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        StaffID = defaultStaffId,
                        Address = txtAddress.Text.Trim(),
                        CreatedAt = DateTime.Now,  // Set CreatedAt to current time
                        IsDeleted = false         // Mark as not deleted
                    };

                    // Add to database and save
                    db.Patients.Add(newPatient);
                    db.SaveChanges();

                    // Show success message
                    Response.Write("<script>alert('Patient added successfully!');</script>");

                    // Refresh Grid
                    RadGridActive.Rebind();
                }
            }
            catch (Exception ex)
            {
                // Handle error
                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
            }
        }

    }
}