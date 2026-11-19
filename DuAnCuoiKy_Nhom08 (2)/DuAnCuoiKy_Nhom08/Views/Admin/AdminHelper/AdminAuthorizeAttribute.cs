using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DuAnCuoiKy_Nhom08.Helpers
{
    /// <summary>Chỉ cho phép người đã đăng nhập (Session["AdminID"]) vào action/controller.</summary>
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Session != null && httpContext.Session["AdminID"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var values = new RouteValueDictionary
            {
                { "controller", "PersonalPortfolio" },
                { "action", "Login" }
            };
            var request = filterContext.HttpContext.Request;
            if (request.HttpMethod == "GET")
            {
                values["returnUrl"] = request.RawUrl;
            }
            filterContext.Result = new RedirectToRouteResult(values);
        }
    }
}
