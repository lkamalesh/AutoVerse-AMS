using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Serilog;

namespace AutoVerse.Web.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            Log.Error(context.Exception, "An unhandled exception occurred.");
            context.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())// To get access to the controller's ViewData here.
                { 
                   Model = context.Exception.Message
                }
            };

            context.ExceptionHandled = true;
        }
    }
}
