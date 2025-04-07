using HospitalSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace HospitalSystem.Patients
{
    public partial class Booking : System.Web.UI.Page
    {
        InternSmallHospitalConnectionString db = DbService.Instance.GetDbContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPatients();
                LoadDoctors();
            }
        }
        private void LoadPatients()
        {
            var patients = db.Patients.Select(p => new { p.PatientID, FullName = p.FirstName + " " + p.LastName }).ToList();
            ddlPatient.DataSource = patients;
            ddlPatient.DataTextField = "FullName";
            ddlPatient.DataValueField = "PatientID";
            ddlPatient.DataBind();
        }

        private void LoadDoctors()
        {
            var doctors = (from d in db.Doctors
                           join u in db.Users on d.UserID equals u.UserID // Adjust UserID with the actual foreign key field
                           where d.DepartmentID != null
                           select new
                           {
                               d.DoctorID,
                               u.FullName 
                           }).ToList();

            ddlDoctor.DataSource = doctors;
            ddlDoctor.DataTextField = "FullName";
            ddlDoctor.DataValueField = "DoctorID";
            ddlDoctor.DataBind();
        }


        protected void btnBook_Click(object sender, EventArgs e)
        {
            HttpCookie myCookie = Request.Cookies["cooklogin"];

            int patientId = int.Parse(ddlPatient.SelectedValue);
            int doctorId = int.Parse(ddlDoctor.SelectedValue);
            int staffId = int.Parse(myCookie["userId"]);
            DateTime date = AppointmentDate.SelectedDate ?? DateTime.Now;
            TimeSpan time = AppointmentTime.SelectedDate?.TimeOfDay ?? TimeSpan.Zero;
            string status = ddlStatus.SelectedValue;

            var appointment = new Appointment
            {
                PatientID = patientId,
                DoctorID = doctorId,
                StaffID = staffId,
                AppointmentDate = date.Date + time,
                Status = status
            };

            db.Appointments.Add(appointment);
            db.SaveChanges();
            Response.Redirect("~/Default.aspx");
        }
    }
}