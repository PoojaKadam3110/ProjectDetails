using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectDetailsAPI.CustomeActionFilters
{
    public class ValidateModuleAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.ModelState.IsValid == false) 
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}
