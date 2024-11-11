using Microsoft.AspNetCore.Mvc.Filters;

namespace Oasis.Communication.Api.Filters
{
    public class SampleAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Items["OnAuthorization"] = "AuthorizationFilterContext";
        }
    }
}
