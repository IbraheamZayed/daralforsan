using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Security
{
    public class UserManager
    {
        public async Task<bool> SignIn(HttpContext httpContext, string UserName, string Password, bool isPersistent = false)
        {
            bool IsAuthenticated = false;
            Services.User user = new Services.User();
            Core.Security.Identity Identity = user.Authenticate(UserName, Password);
            //if authenticated
            if (Identity != null)
            {
                ClaimsIdentity cidentity = new ClaimsIdentity(ExtractClaims(Identity), CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(cidentity);
                httpContext.User = principal;
                AuthenticationProperties props = new AuthenticationProperties
                {
                    IsPersistent = isPersistent,//user should automatically be logged out when the browser closed
                    AllowRefresh = true
                };

                IsAuthenticated = true;
                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
            }
            return IsAuthenticated;
        }
        //-----------------------------------------------------------------------------------------
        public async void SignOut(HttpContext httpContext)
        {
            httpContext.Session.Clear();
            await httpContext.SignOutAsync();
        }
        //-----------------------------------------------------------------------------------------
        private IEnumerable<Claim> ExtractClaims(Core.Security.Identity Identity)
        {
            List<Claim> claims = new List<Claim>();

            //User Claims
            claims.Add(new Claim(ClaimTypes.NameIdentifier, Identity.UserID.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, Identity.LocalName));
            claims.Add(new Claim(Core.Security.Extensions.ClaimTypes.LocalName, Identity.LocalName));
            claims.Add(new Claim(Core.Security.Extensions.ClaimTypes.LatinName, Identity.LatinName));
            
            if (!string.IsNullOrEmpty(Identity.Email))
                claims.Add(new Claim(ClaimTypes.MobilePhone, Identity.MobileNo));
            
            if (!string.IsNullOrEmpty(Identity.MobileNo))
                claims.Add(new Claim(ClaimTypes.MobilePhone, Identity.MobileNo));
            
            if (!string.IsNullOrEmpty(Identity.Theme))
                claims.Add(new Claim(Core.Security.Extensions.ClaimTypes.Theme, Identity.Theme));
            else
                claims.Add(new Claim(Core.Security.Extensions.ClaimTypes.Theme, "blue"));
            
            claims.Add(new Claim(Core.Security.Extensions.ClaimTypes.ISOLanguageName, Identity.IsoLanguageName));
            if (Identity.DeviceID != null)
                claims.Add(new Claim(Core.Security.Extensions.ClaimTypes.DeviceID, Identity.DeviceID));
            //Role Claims
            //foreach (Core.Security.Role role in Identity.Roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role.Name));
            //}
            return claims;
        }
        //-----------------------------------------------------------------------------------------
    }
}