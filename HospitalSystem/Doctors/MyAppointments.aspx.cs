using HospitalSystem.App_Data;
using HospitalSystem.Services;
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
        private InternSmallHospitalConnectionString db = DbService.Instance.GetDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SecurityService.CheckUserAccess("ADMIN", "DOCTOR");
                RadGrid1.Rebind();
            }
        }

		private int getUserID()
		{
			HttpCookie myCookie = Request.Cookies["cooklogin"];
			return Convert.ToInt32(myCookie["userId"]);
		}

		protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			int userId = getUserID();

			var appointments = from a in db.Appointments
							   join p in db.Patients on a.PatientID equals p.PatientID
							   join d in db.Doctors on a.DoctorID equals d.DoctorID
							   join s in db.Staffs on a.StaffID equals s.StaffID
							   join u in db.Users on s.UserID equals u.UserID // Get Staff Name
							   where d.UserID == userId || s.UserID == userId  // Filter by Doctor or Staff
							   select new
							   {
								   a.AppointmentID,
								   PatientName = p.FirstName + " " + p.LastName, // Concatenate First & Last Name
								   StaffName = u.FullName, // Display Staff's Name
								   a.AppointmentDate,
								   a.Status
							   };

			RadGrid1.DataSource = appointments.ToList();
		}

	}

}