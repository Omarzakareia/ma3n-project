using HospitalSystem.App_Data;
using HospitalSystem.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalSystem.Billings
{
    public partial class Invoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			SecurityService.CheckUserAccess("ADMIN", "STAFF");
			LoadPatients();
            LoadDoctors();
            LoadBillingVaults();
        }
        private void LoadPatients()
        {
            using (var db = DbService.Instance.GetDbContext())
            {
                var patients = db.Patients.Select(p => new
                {
                    p.PatientID,
                    FullName = p.FirstName + " " + p.LastName
                }).ToList();

                ddlPatient.DataSource = patients;
                ddlPatient.DataTextField = "FullName";
                ddlPatient.DataValueField = "PatientID";
                ddlPatient.DataBind();
            }
        }

        protected void LoadDoctors()
        {
            using (var db = DbService.Instance.GetDbContext())
            {
                var doctors = db.Doctors
                                .Join(db.Users,
                                      doctor => doctor.UserID,
                                      user => user.UserID,
                                      (doctor, user) => new
                                      {
                                          doctor.DoctorID,
                                          user.FullName
                                      })
                                .ToList();

                ddlDoctor.DataSource = doctors;
                ddlDoctor.DataTextField = "FullName";
                ddlDoctor.DataValueField = "DoctorID";
                ddlDoctor.DataBind();
            }
        }
        protected void LoadBillingVaults()
        {
            using (var db = DbService.Instance.GetDbContext())
            {
                var billingVaults = db.BillingVaults.Select(b => new
                {
                    b.BillingVaultID,
                    b.VaultName
                }).ToList();

                ddlBillingVault.DataSource = billingVaults;
                ddlBillingVault.DataTextField = "VaultName";
                ddlBillingVault.DataValueField = "BillingVaultID";
                ddlBillingVault.DataBind();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                decimal totalAmount;
                decimal amountPaid;

                bool isTotalAmountValid = decimal.TryParse(txtTotalAmount.Text, out totalAmount);
                bool isAmountPaidValid = decimal.TryParse(txtAmountPaid.Text, out amountPaid);

                if (isTotalAmountValid && isAmountPaidValid)
                {
                    int patientId = int.Parse(ddlPatient.SelectedValue);
                    int doctorId = int.Parse(ddlDoctor.SelectedValue);
                    int billingVaultId = int.Parse(ddlBillingVault.SelectedValue);
                    int staffId = GetStaffId();  
                    DateTime billingDate = DateTime.Now;  

                    using (var db = DbService.Instance.GetDbContext())
                    {
                        var newBilling = new Billing
                        {
                            PatientID = patientId,
                            AppointmentID = 1,
                            BillingVaultID = billingVaultId,
                            StaffID = staffId,
                            TotalAmount = totalAmount,
                            AmountPaid = amountPaid,
                            BillingDate = billingDate
                        };

                       
                        db.Billings.Add(newBilling);
                        db.SaveChanges();
                    }
                }
                else
                {
                    lblError.Text = "Please enter valid amounts for Total Amount and Amount Paid.";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Please fill all required fields correctly.";
                lblError.Visible = true;
            }
        }

        private int GetStaffId()
        {
            HttpCookie myCookie = HttpContext.Current.Request.Cookies["cooklogin"];
            if (myCookie == null)
            {
                Response.Redirect("~/Unauthorized.aspx");
            }

            string userRole = myCookie["role"];

            int? entityId = UserService.GetEntityId();
            if (entityId == null)
            {
                Response.Redirect("~/Unauthorized.aspx");
            }

            return userRole != "Staff" ? 1 : entityId.Value;
        }

    }
}