using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VcksCore.Filters
{
    //public class AjaxOrChildActionOnlyAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        if (!filterContext.HttpContext.Request.IsAjaxRequest() &&
    //            !filterContext.IsChildAction
    //        )
    //        {
    //            filterContext.Result = new HttpNotFoundResult();
    //        }
    //    }
    //}
}