using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace AutoVerse.API.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {    
        public void OnException(ExceptionContext context)
        {
            Log.Error(context.Exception, "An unhandled exception occurred.");
            context.Result = new ObjectResult(new
            {
                Message = "An unexpected error occurred. Try again later."
                
            })                   
            {
                StatusCode = 500// Tells the client that an internal server error
            };

            context.ExceptionHandled = true;
        }
        
    }
}
