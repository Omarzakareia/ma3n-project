using DevExpress.XtraReports.Web;
using HospitalSystem.Services;
using System;
using System.Web.UI;

namespace HospitalSystem.Reports
{
    public partial class PatientsReport : System.Web.UI.Page
    {
        private InternSmallHospitalConnectionString _context = DbService.Instance.GetDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadReport("Daily"); // Load Daily report by default
            }
        }

        protected void btnDaily_Click(object sender, EventArgs e)
        {
            LoadReport("Daily");
        }

        protected void btnMonthly_Click(object sender, EventArgs e)
        {
            LoadReport("Monthly");
        }

        private void LoadReport(string type)
        {
            // Create Report Instance
            PatientRegisterationReport report = new PatientRegisterationReport();

            // Validate Parameters Exist Before Assigning
            if (report.Parameters["StartDate"] != null && report.Parameters["EndDate"] != null)
            {
                DateTime startDate, endDate;

                if (type == "Daily")
                {
                    startDate = DateTime.Today; // Start of the day (00:00:00)
                    endDate = DateTime.Today.AddDays(1).AddTicks(-1); // End of the day (23:59:59.9999999)
                }

                else // Monthly
                {
                    startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    endDate = startDate.AddMonths(1).AddDays(-1);
                }

                // Assign Parameter Values
                report.Parameters["StartDate"].Value = startDate;
                report.Parameters["EndDate"].Value = endDate;
                report.Parameters["StartDate"].Visible = false;
                report.Parameters["EndDate"].Visible = false;
            }
            else
            {
                throw new Exception("Report parameters are missing. Ensure they are defined in the report.");
            }

            // Ensure the viewer is not null before assigning
            if (ASPxWebDocumentViewer1 != null)
            {
                ASPxWebDocumentViewer1.OpenReport(report);
                ASPxWebDocumentViewer1.DataBind();
            }
            else
            {
                throw new Exception("ASPxWebDocumentViewer1 is not initialized. Ensure the control is available in the page.");
            }
        }
    }
}
