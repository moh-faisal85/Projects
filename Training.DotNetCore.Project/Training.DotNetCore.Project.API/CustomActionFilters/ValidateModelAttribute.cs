using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Training.DotNetCore.Project.API.CustomActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                // Return a 400 Bad Request with the model validation errors
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
