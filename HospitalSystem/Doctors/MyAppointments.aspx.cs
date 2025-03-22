using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace HospitalSystem.Doctors
{
	public partial class MyAppointments : System.Web.UI.Page
	{
		private InternSmallHospitalConnectionString db = new InternSmallHospitalConnectionString();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				RadGrid1.Rebind();
			}
		}

		protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{

			var appointments = from a in db.Appointments
							   join p in db.Patients on a.PatientID equals p.PatientID
							   join s in db.Staffs on a.StaffID equals s.StaffID
							   join u in db.Users on s.UserID equals u.UserID
							   select new
							   {
								   a.AppointmentID,
								   PatientName = p.FirstName,  
								   StaffName = u.FullName,     
								   a.AppointmentDate,
								   a.Status
							   };

			RadGrid1.DataSource = appointments.ToList();

		}

	}

}