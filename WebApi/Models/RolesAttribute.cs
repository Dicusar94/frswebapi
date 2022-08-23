using Microsoft.AspNetCore.Authorization;

namespace WebApi.Models
{
    public class RolesAttribute : AuthorizeAttribute
    {
        public RolesAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles!);
        }
    }
}
