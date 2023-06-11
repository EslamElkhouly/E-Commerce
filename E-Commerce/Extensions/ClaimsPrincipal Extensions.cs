using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace E_Commerce.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string RetreiveEmailFromPrincipal(this ClaimsPrincipal user)
            => user.FindFirstValue(ClaimTypes.Email);

    }
}