using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopOnline.Common
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public string RoleID { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //var isAuthorize = base.AuthorizeCore(httpContext);
            //if (!isAuthorize)
            //{
            //    return false;
            //}
            var session = (UserLogin)HttpContext.Current.Session[CommonConstant.USER_SESSION];
            if (session == null) return false;
            List<string> priviligeLevels = this.GetCredentialByLoggedInUser();

            if (priviligeLevels.Contains(this.RoleID) || session.GroupID == LoginHelper.ADMIN_GROUP)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult()
            {
                ViewName = "~/Areas/Admin/Views/Shared/Error401.cshtml"
            };
        }

        private List<string> GetCredentialByLoggedInUser()
        {
            var credential = (List<string>)HttpContext.Current.Session[CommonConstant.SESSION_CREDENTIALS];
            return credential;
        }
    }
}