using System;
using HospitalSystem.App_Data;
namespace HospitalSystem.Reports
{
	public partial class BillingsReport : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				SecurityService.CheckUserAccess("ADMIN");
				LoadReport("Daily");
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
            IncomeReport report = new IncomeReport();
            // Validate Parameters Exist Before Assigning
            if (report.Parameters["StartDate"] != null && report.Parameters["EndDate"] != null)
            {
                DateTime startDate, endDate;
                if (type == "Daily")
                {
                    startDate = DateTime.Today;
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
            if (ASPxWebDocumentViewer1 != null)
            {
                ASPxWebDocumentViewer1.OpenReport(report);
                ASPxWebDocumentViewer1.DataBind();
            }
        }
    }
}