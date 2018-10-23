using ShopOnline.Common;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session[CommonConstant.CurrentCulture] != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session[CommonConstant.CurrentCulture].ToString());
                //Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session[CommonConstant.CurrentCulture].ToString());
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            }
            else
            {
                Session[CommonConstant.CurrentCulture] = "en-US";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            }
        }

        //Changing Culture
        public ActionResult ChangeCulture(string ddlCulture, string returnUrl)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ddlCulture);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(ddlCulture);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            Session[CommonConstant.CurrentCulture] = ddlCulture;
            return Redirect(returnUrl);
        }

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
            if (messageType == "success")
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