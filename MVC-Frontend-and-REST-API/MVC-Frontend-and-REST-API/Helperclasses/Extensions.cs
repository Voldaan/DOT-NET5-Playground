using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MVC_Frontend_and_REST_API.Helperclasses
{
    public static class Extensions
    {
        //Get UserId from the token
        public static string GetUserID(this HttpContext httpContext)
        {
            if (httpContext is null)
            {
                return string.Empty;
            }

            return httpContext.User.Claims.Single(x => x.Type == "Id").Value;
        }
    }
}
