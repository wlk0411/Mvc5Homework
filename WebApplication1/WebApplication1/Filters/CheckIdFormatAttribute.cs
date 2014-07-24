using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Filters
{
    public class CheckIdFormatAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting( ActionExecutingContext filterContext )
        {
            if( filterContext.RouteData.Values.ContainsKey("id") )
            {
                var id = 0;
                var temp = ( filterContext.RouteData.Values["id"] ?? string.Empty ).ToString();

                if( int.TryParse(temp, out id) )
                {
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    filterContext.Result = new RedirectResult("/");
                }
            }
        }
    }
}