using HospitalSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalSystem.Patients
{
    public partial class PatientHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadGridPatientHistory_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (!int.TryParse(Request.QueryString["PatientID"], out int patientID))
                {
                    lblErrorMessage.Text = "Invalid patient ID.";
                    lblErrorMessage.Visible = true;
                    return;
                }

                using (var db = DbService.Instance.GetDbContext())
                {
                    var historyData = db.PatientHistories
                        .Where(h => h.PatientID == patientID)
                        .Select(h => new
                        {
                            h.PatientID,
                            h.FullName,
                            h.BillingID,
                            h.TotalAmount,
                            h.AmountPaid,
                            h.BillingDate,
                            h.PaymentStatus,
                            h.AppointmentID,
                            h.AppointmentDate,
                            h.DoctorID,
                            h.DoctorName,
                            h.DepartmentName
                        })
                        .ToList();

                    RadGridPatientHistory.DataSource = historyData;
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Error loading history: " + ex.Message;
                lblErrorMessage.Visible = true;
            }
        }

    }
}