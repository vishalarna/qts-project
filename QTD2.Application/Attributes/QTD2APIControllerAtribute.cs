using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace QTD2.Application.Attributes
{
    public class QTD2APIControllerAtribute : ActionFilterAttribute
    {
        public object id { get; set; }

        public QTD2APIControllerAtribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            Log.ForContext<QTD2APIControllerAtribute>().Information($"Timestamp {DateTime.Now}, ActionType : 'EXECUTING' , Controller Name : {context.ActionDescriptor.DisplayName} , Path : {context.HttpContext.Request.Path} , Request Type : {context.HttpContext.Request.Method}");
            id = context.ActionArguments.FirstOrDefault().Value;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            Log.ForContext<QTD2APIControllerAtribute>().Information($"Timestamp {DateTime.Now}, ActionType : 'EXECUTED' , Controller Name : {context.ActionDescriptor.DisplayName} , Path : {context.HttpContext.Request.Path} , Request Type : {context.HttpContext.Request.Method}");
        }
    }
}
