using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TodoApp.Api.Filters
{
    public class NullModelFilterAttribute: ActionFilterAttribute
    {
        public string ParameterName { get; set; }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if(actionContext.ActionArguments[ParameterName] == null)
            {
                actionContext.ModelState.AddModelError(string.Empty, "Model cannot be null");
            }

            base.OnActionExecuting(actionContext);
        }
    }
}