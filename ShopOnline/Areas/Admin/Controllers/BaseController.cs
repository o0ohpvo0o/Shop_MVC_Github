using ShopOnline.Common;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstant.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }

        protected void SetAlert(string message, string messageType)
        {
            TempData["MessageAlert"] = message;
            if(messageType ==  "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (messageType == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (messageType == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}