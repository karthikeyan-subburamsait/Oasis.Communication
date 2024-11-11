using Microsoft.AspNetCore.Mvc.Filters;

namespace Oasis.Communication.Api.Filters
{
    public class SampleResourceFilter : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //Console.WriteLine("ResourceExecutedContext123");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.HttpContext.Items["OnResourceExecuting"] = "ResourceExecutingContext";
        }
    }
}
