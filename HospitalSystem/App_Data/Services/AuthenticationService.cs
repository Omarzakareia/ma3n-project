using System;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace HospitalSystem.Services
{
    public class AuthenticationService
    {
        private readonly InternSmallHospitalConnectionString _context;

        public AuthenticationService()
        {
            _context = DbService.Instance.GetDbContext();
        }

        public AuthenticationResult AuthenticateUser(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                if ((bool)user.IsLocked)
                {
                    return new AuthenticationResult(false, 0, null, null); // Account is locked
                }

                if (user.PasswordHash == password)
                {
                    ResetFailedLogins(email); // Reset failed attempts on success
                    return new AuthenticationResult(true, user.UserID, user.Role.RoleName, user.FullName);
                }
            }

            return new AuthenticationResult(false, 0, null, null);
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

        public void SetAuthCookie(string email,string name, string role, int userId, bool rememberMe)
        {
            HttpCookie authCookie = new HttpCookie("cooklogin");
            authCookie["email"] = HttpUtility.UrlEncode(email.ToUpper());
            authCookie["name"] = HttpUtility.UrlEncode(name.ToUpper());
            authCookie["role"] = HttpUtility.UrlEncode(role);
            authCookie["userId"] = userId.ToString();
            authCookie.Expires = rememberMe ? DateTime.Now.AddDays(7) : DateTime.Now.AddHours(1);
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }

    }

    // Updated AuthenticationResult to include the Name
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; }
        public int UserId { get; }
        public string Role { get; }
        public string Name { get; } // Added name property

        public AuthenticationResult(bool isAuthenticated, int userId, string role, string name)
        {
            IsAuthenticated = isAuthenticated;
            UserId = userId;
            Role = role;
            Name = name;
        }
    }
}
