using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Classes;

namespace WebClient.Classes
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is HandledException exception)
            {
                context.Result = new BadRequestObjectResult(exception.Message);
            }
        }
    }
}
