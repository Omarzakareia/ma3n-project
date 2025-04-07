using HospitalSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace HospitalSystem.Billings
{
    public partial class BillingHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void RadGridActive_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                using (var db = DbService.Instance.GetDbContext())
                {
                    var billingData = db.PartialPaymentBills.ToList();
                    RadGridActive.DataSource = billingData;
                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
            }
        }
        protected void RadGridFull_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                using (var db = DbService.Instance.GetDbContext())
                {
                    var billingData = db.FullPaymentBills.ToList();
                    RadGridFull.DataSource = billingData;
                }
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
            }
        }
        protected void btnToggleVisibility_Click(object sender, EventArgs e)
        {
            // Toggle visibility of the panels
            pnlPartialBill.Visible = !pnlPartialBill.Visible;
            pnlFullBill.Visible = !pnlFullBill.Visible;

            // Change button text based on visibility of panels
            if (pnlFullBill.Visible)
            {
                btnToggleVisibility.Text = "Show Partial Bill";
                RadGridFull.Rebind();
            }
            else
            {
                btnToggleVisibility.Text = "Show Full Bill";  
                RadGridActive.Rebind();
            }
        }


        protected void RadGridActive_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "AddPayment")
            {
                GridDataItem item = e.Item as GridDataItem;
                if (item != null)
                {
                    int billingId = Convert.ToInt32(e.CommandArgument);
                    var txtAdditionalPayment = (RadNumericTextBox)item.FindControl("txtAdditionalPayment");

                    if (txtAdditionalPayment != null && txtAdditionalPayment.Value.HasValue)
                    {
                        decimal additionalPayment = (decimal)txtAdditionalPayment.Value;
                        AddPaymentToBilling(billingId, additionalPayment);
                        RadGridActive.Rebind();
                    }
                }
            }
        }

        private void AddPaymentToBilling(int billingId, decimal additionalPayment)
        {
            try
            {
                using (var db = DbService.Instance.GetDbContext())
                {
                    var billingRecord = db.Billings.FirstOrDefault(b => b.BillingID == billingId);
                    if (billingRecord != null)
                    {
                        decimal currentBalance = billingRecord.TotalAmount - billingRecord.AmountPaid;

                        if (additionalPayment > currentBalance)
                        {
                            Response.Write("<script>alert('Error: Payment amount exceeds the remaining balance.');</script>");
                            return;
                        }

                        // Update AmountPaid
                        billingRecord.AmountPaid += additionalPayment;

                        // Recalculate balance
                        billingRecord.Balance = billingRecord.TotalAmount - billingRecord.AmountPaid;

                        // If the balance is now zero, change status to 'Fulfilled'
                        if (billingRecord.Balance == 0)
                        {
                            billingRecord.PaymentStatus = "Paid";
                        }
                        billingRecord.BillingDate = billingRecord.BillingDate = DateTime.Now;


                        // Save changes to the database
                        db.SaveChanges();

                        // Refresh grid
                        RadGridActive.Rebind();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
            }
        }



    }
}
