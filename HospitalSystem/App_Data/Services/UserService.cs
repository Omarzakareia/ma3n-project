using System;
using System.Linq;
using System.Web;

namespace HospitalSystem.Services
{
	public static class UserService
	{

		public static int? GetUserId()
		{
			try
			{
				HttpCookie myCookie = HttpContext.Current.Request.Cookies["cooklogin"];
				if (myCookie != null && int.TryParse(myCookie["userId"], out int userId))
				{
					return userId;
				}
			}
			catch (Exception ex)
			{
				RedirectToPageWithAlert("~/Unauthorized.aspx", $"Error: {ex.Message.Replace("'", "\\'")}");
			}

			RedirectToPageWithAlert("~/Unauthorized.aspx", "Error: Access Denied!");
			return null;
		}

		public static int? GetEntityId()
		{
			try
			{
				int? userId = GetUserId();
				if (userId == null) return null;

				HttpCookie myCookie = HttpContext.Current.Request.Cookies["cooklogin"];
				string userRole = myCookie?["role"];

				using (var db = DbService.Instance.GetDbContext())
				{
					if (userRole == "Admin")
					{
						return userId; // Admins use their UserID directly
					}
					else if (userRole == "Doctor")
					{
						var doctor = db.Doctors.FirstOrDefault(d => d.UserID == userId);
						return doctor?.DoctorID;
					}
					else if (userRole == "Staff")
					{
						var staff = db.Staffs.FirstOrDefault(s => s.UserID == userId);
						return staff?.StaffID;
					}
				}
			}
			catch (Exception ex)
			{
				RedirectToPageWithAlert("~/Unauthorized.aspx", $"Error: {ex.Message.Replace("'", "\\'")}");
			}

			RedirectToPageWithAlert("~/Unauthorized.aspx", "Error: Access Denied!");
			return null;
		}

		private static void RedirectToPageWithAlert(string relativeUrl, string alertMessage)
		{
			string absoluteUrl = VirtualPathUtility.ToAbsolute(relativeUrl);
			HttpContext.Current.Response.Write($"<script>alert('{alertMessage}'); window.location.href='{absoluteUrl}';</script>");
			HttpContext.Current.Response.End();
		}
	}
}