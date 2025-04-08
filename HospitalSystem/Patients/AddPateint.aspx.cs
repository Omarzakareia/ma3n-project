using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HospitalSystem.Services;

namespace HospitalSystem.Patients
{
    public partial class AddPateint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddPatient_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = DbService.Instance.GetDbContext())
                {
                    // Get the staff ID for the logged-in user
                    var staff = UserService.GetEntityId();
                    DateTime date = DOB.SelectedDate ?? DateTime.Now;

                    var newPatient = new Patient
                    {
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        Gender = Gender.SelectedValue,
                        Phone = txtPhone.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        StaffID = (int)staff,
                        Address = txtAddress.Text.Trim(),
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                        DOB = date.Date
                    };

                    db.Patients.Add(newPatient);
                    db.SaveChanges();

                    Response.Write("<script>alert('Patient added successfully!');</script>");
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