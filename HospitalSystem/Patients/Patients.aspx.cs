using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HospitalSystem.Services;

namespace HospitalSystem.Patients
{
    public partial class Patients : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadPatientData();
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
    }
}