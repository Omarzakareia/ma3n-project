using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.DocumentServices.ServiceModel.DataContracts;
using HospitalSystem.App_Data;

namespace HospitalSystem.Reports
{
	public partial class AppointmentsReport : System.Web.UI.Page
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
		protected void btnAll_Click(object sender, EventArgs e)
		{
			LoadReport("All");
		}


		private void LoadReport(string type)
		{
			// Create Report Instance
			DailyMonthlyAppointment report = new DailyMonthlyAppointment();

			// Check if parameters exist before assigning
			if (report.Parameters["StartDate"] != null && report.Parameters["EndDate"] != null)
			{
				if (type == "Daily")
				{
					DateTime startDate = DateTime.Today;
					DateTime endDate = DateTime.Today.AddDays(1).AddTicks(-1);

					report.Parameters["StartDate"].Value = startDate;
					report.Parameters["EndDate"].Value = endDate;
				}
				else if (type == "Monthly")
				{
					DateTime startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
					DateTime endDate = startDate.AddMonths(1).AddDays(-1);

					report.Parameters["StartDate"].Value = startDate;
					report.Parameters["EndDate"].Value = endDate;
				}
				else if (type == "All")
				{
					report.Parameters["StartDate"].Value = new DateTime(2000, 1, 1);
					report.Parameters["EndDate"].Value = DateTime.Today.AddDays(1).AddTicks(-1); 
				}

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
