using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{
    [CheckIdFormat]
    public class BaseController : Controller
    {
        protected override void HandleUnknownAction( string actionName )
        {
            if( this.Request.IsAjaxRequest() )
            {
                base.HandleUnknownAction(actionName);
            }
            else
            {
                this.RedirectToAction("NotFound").ExecuteResult(this.ControllerContext);
            }
        }


        public ActionResult NotFound()
        {
            return View();
        }
    }
}