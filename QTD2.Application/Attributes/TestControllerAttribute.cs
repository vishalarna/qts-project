using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QTD2.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TestControllerAttribute : ActionFilterAttribute
    {
        public TestControllerAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                context.HttpContext.Response.StatusCode = 404;
            }

            base.OnActionExecuting(context);
        }
    }
}
