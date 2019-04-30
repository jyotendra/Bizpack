

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

// TODO: Create a middleware that needs no prefixing before every action method and that distinguishes response-type by itself.
namespace Bizpack.Service.ApiResponse {
    public class ValidationFilterAttribute : IActionFilter {
        
        public void OnActionExecuting (ActionExecutingContext context) {
            // do something before the action executes
            ModelStateDictionary state = context.ModelState;
            if(!state.IsValid) {
                context.Result = new BadRequestObjectResult(new ApiBadRequestResponse(context.ModelState));
            }
        }

        public void OnActionExecuted (ActionExecutedContext context) {
            // do something after the action executes

        }
    }
}
