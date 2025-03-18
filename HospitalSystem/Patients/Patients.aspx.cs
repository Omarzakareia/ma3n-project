using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HospitalSystem.Services;
using Telerik.Web.UI;

namespace HospitalSystem.Patients
{
    public partial class Patients : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPatientData();
                LoadDeletedPatientData();
            }
        }

        private void LoadPatientData()
        {
            try
            {
                using (var db = DbService.Instance.GetDbContext())
                {
                    var patientData = db.PatientInfoes.ToList(); // Fetch from view
                    RadGrid1.DataSource = patientData;
                    RadGrid1.DataBind(); // Bind to RadGrid
                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
            }
        }
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            using (var db = DbService.Instance.GetDbContext())
            {
                var patientData = db.PatientInfoes
                    .Where(p => p.FullName.ToLower().Contains(searchText))
                    .ToList();

                RadGrid1.DataSource = patientData;
                RadGrid1.DataBind();
            }
        }

        protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
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

                        LoadPatientData();
                    }
                    else
                    {
                        Response.Write("<script>alert('Error: Patient not found!');</script>");
                    }
                }
            }
        }
        private void LoadDeletedPatientData()
        {
            try
            {
                using (var db = DbService.Instance.GetDbContext())
                {
                    var deletedPatients = db.DeletedPatientViews.ToList(); // Fetch from DeletedPatientView
                    RadGridDeleted.DataSource = deletedPatients;
                    RadGridDeleted.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
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
    }
}