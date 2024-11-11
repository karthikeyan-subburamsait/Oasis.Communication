using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Oasis.Communication.Api.Filters
{
    public class SampleActionFilter : Microsoft.AspNetCore.Mvc.Filters.IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Items["OnActionExecuted"] = "ActionExecutedContext";
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items["OnActionExecuting"] = "ActionExecutingContext";
        }
    }
}
