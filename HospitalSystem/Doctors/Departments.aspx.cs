using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace HospitalSystem.Doctors
{
	public partial class Departments : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

        }

		protected void SmallHospital_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
		{

		}
		protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
		{
			if (e.Item is GridDataItem)
			{
				GridDataItem item = (GridDataItem)e.Item;
				DropDownList ddlDoctors = (DropDownList)item.FindControl("ddlDoctors");

				if (ddlDoctors != null)
				{
					string connStr = ConfigurationManager.ConnectionStrings["InternSmallHospitalConnectionString"].ConnectionString;
					using (SqlConnection conn = new SqlConnection(connStr))
					{
						conn.Open();
						string query = "SELECT d.DoctorID, u.FullName AS DoctorName FROM Doctor d INNER JOIN [User] u ON d.UserID = u.UserID WHERE d.DepartmentID IS NULL OR d.DepartmentID = 0";
						SqlCommand cmd = new SqlCommand(query, conn);
						SqlDataReader reader = cmd.ExecuteReader();

						ddlDoctors.DataSource = reader;
						ddlDoctors.DataTextField = "DoctorName";
						ddlDoctors.DataValueField = "DoctorID";
						ddlDoctors.DataBind();
						reader.Close();
					}

					// Add default "Select Doctor" item
					ddlDoctors.Items.Insert(0, new ListItem("Select Doctor", ""));
				}
			}
		}


	}
}