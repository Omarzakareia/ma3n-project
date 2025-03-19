using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.PageLayout;
using Telerik.Windows.Documents.Model.Drawing.Charts;

namespace HospitalSystem.Doctors
{
	public partial class Departments : System.Web.UI.Page
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
			var departments = db.Departments.Where(d => d.IsDeleted == false)
							.Select(d => new
							{
								d.DepartmentID,
								d.DepartmentName
							})
							.ToList();

			RadGrid1.DataSource = departments; 
		}


		protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
		{
			if (e.Item is GridDataItem item)
			{
				DropDownList ddlDoctors = (DropDownList)item.FindControl("ddlDoctors");
				if (ddlDoctors != null)
				{
					ddlDoctors.DataSource = db.Doctors
											  .Where(d => d.DepartmentID == null || d.DepartmentID == 0)
											  .Select(d => new { d.DoctorID, DoctorName = d.User.FullName })
											  .ToList();
					ddlDoctors.DataTextField = "DoctorName";
					ddlDoctors.DataValueField = "DoctorID";
					ddlDoctors.DataBind();
				}
				int departmentId = Convert.ToInt32(item.GetDataKeyValue("DepartmentID"));

				var rptDoctors = (Repeater)item.FindControl("rptDoctors");
				if (rptDoctors != null)
				{
					var doctors = db.Doctors
						.Where(d => d.DepartmentID == departmentId)
						.Select(d => new
						{
							d.DoctorID,
							d.User.FullName,
							d.Speciality.SpecialityName
						})
						.ToList();

					rptDoctors.DataSource = doctors;
					rptDoctors.DataBind();
				}
			}
		}

		protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
		{
			if (e.CommandName == "AssignDoctor")
			{
				GridDataItem item = (GridDataItem)e.Item;
				DropDownList ddlDoctors = (DropDownList)item.FindControl("ddlDoctors");

				if (ddlDoctors != null && !string.IsNullOrEmpty(ddlDoctors.SelectedValue))
				{
					int doctorId = Convert.ToInt32(ddlDoctors.SelectedValue);
					int departmentId = Convert.ToInt32(e.CommandArgument);

					
						var doctor = db.Doctors.FirstOrDefault(d => d.DoctorID == doctorId);
						if (doctor != null)
						{
							doctor.DepartmentID = departmentId;
							db.SaveChanges();
						}
					

					RadGrid1.Rebind();
				}
			}
			else if (e.CommandName == "UnassignDoctor")
			{
				int doctorId = Convert.ToInt32(e.CommandArgument);

				var doctor = db.Doctors.FirstOrDefault(d => d.DoctorID == doctorId);
				if (doctor != null)
				{
					doctor.DepartmentID = null; 
					db.SaveChanges();
				}

				RadGrid1.Rebind();
			}

		}



		


		protected void RadGrid1_ItemInsert(object sender, GridCommandEventArgs e)
		{
			if (e.Item is GridEditableItem editableItem)
			{
				
				GridEditableItem item = (GridEditableItem)e.Item;
				TextBox txtDepartmentName = (TextBox)item["DepartmentName"].Controls[0];

				if (txtDepartmentName != null)
				{
					string departmentName = txtDepartmentName.Text.Trim();

					if (!string.IsNullOrEmpty(departmentName))
					{
						
						
							
							Department newDepartment = new Department
							{
								DepartmentName = departmentName,
							    IsDeleted=false
							};

							
							db.Departments.Add(newDepartment);
							db.SaveChanges();
						

						
						RadGrid1.Rebind();
					}
				}
			}
		}
		protected void RadGrid1_DeleteCommand(object sender, GridCommandEventArgs e)
		{
			try
			{
				if (e.Item is GridDataItem item)
				{
					int departmentId = Convert.ToInt32(item.GetDataKeyValue("DepartmentID"));

					
					var doctors = db.Doctors.Where(d => d.DepartmentID == departmentId).ToList();
					foreach (var doctor in doctors)
					{
						doctor.DepartmentID = null; 
					}

					
					var department = db.Departments.FirstOrDefault(d => d.DepartmentID == departmentId);
					if (department != null)
					{
						department.IsDeleted = true;
						department.DeletedAt = DateTime.Now;
					}

					db.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				
				Console.WriteLine("Error deleting department: " + ex.Message);
			}
		}


		protected void RadGrid1_UpdateCommand(object sender, GridCommandEventArgs e)
		{
			try
			{
				if (e.Item is GridEditableItem editableItem)
				{
					int departmentId = Convert.ToInt32(editableItem.GetDataKeyValue("DepartmentID"));
					string newDepartmentName = ((TextBox)editableItem["DepartmentName"].Controls[0]).Text.Trim();

					var department = db.Departments.FirstOrDefault(d => d.DepartmentID == departmentId);
					if (department != null)
					{
						department.DepartmentName = newDepartmentName;
						db.SaveChanges();
					}
				}
			}
			catch (Exception ex)
			{
				
				Console.WriteLine("Error updating department: " + ex.Message);
			}
		}


		protected void btnUnassignDoctor_Command(object sender, CommandEventArgs e)
		{
			if (e.CommandName == "UnassignDoctor")
			{
				int doctorId = Convert.ToInt32(e.CommandArgument);

				
				
					var doctor = db.Doctors.FirstOrDefault(d => d.DoctorID == doctorId);
					if (doctor != null)
					{
						doctor.DepartmentID = null; // Unassign doctor from department
						db.SaveChanges();
					}
				

				RadGrid1.Rebind(); // Refresh the grid
			}
		}








	}
}
