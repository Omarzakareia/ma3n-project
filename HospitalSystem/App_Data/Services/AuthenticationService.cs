using System;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace HospitalSystem.Services
{
    public class AuthenticationService
    {
        private readonly HospitalEntities _context;

        public AuthenticationService()
        {
            _context = new HospitalEntities();
        }

        public bool AuthenticateUser(string email, string password, out int userId, out string role)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                if ((bool)user.IsLocked)
                {
                    userId = 0;
                    role = null;
                    return false; // Account is locked
                }

                if (user.PasswordHash == password)
                {
                    userId = user.UserID;
                    role = user.Role.RoleName;
                    ResetFailedLogins(email); // Reset failed attempts on success
                    return true;
                }
            }

            userId = 0;
            role = null;
            return false;
        }

        public bool IsAccountLocked(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user?.IsLocked ?? false;
        }

        public void IncrementFailedLogins(string email, int maxAttempts)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                user.FailedLogins++;

                if (user.FailedLogins >= maxAttempts)
                {
                    user.IsLocked = true; // Lock account
                }

                _context.SaveChanges();
            }
        }

        public void ResetFailedLogins(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                user.FailedLogins = 0;
                user.IsLocked = false;
                _context.SaveChanges();
            }
        }

        public void SetAuthCookie(string email, string role, int userId, bool rememberMe)
        {
            FormsAuthentication.SetAuthCookie(email, rememberMe);

            HttpCookie authCookie = new HttpCookie("cooklogin");
            authCookie["email"] = HttpUtility.UrlEncode(email.ToUpper());
            authCookie["role"] = HttpUtility.UrlEncode(role);
            authCookie["userId"] = userId.ToString();
            authCookie.Expires = rememberMe ? DateTime.Now.AddDays(7) : DateTime.Now.AddHours(1); 
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }
    }
}
