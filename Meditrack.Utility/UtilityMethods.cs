using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Meditrack.Utility
{
    public static class UtilityMethods
    {
        public static string GetCurrentUserId(HttpContext httpContext)
        {
            // Check if HttpContext is available
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            // Get the user's ID from the HttpContext
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return userId;
        }
    }
}