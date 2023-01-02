using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KTS_Testing_System.Classes
{
    public class AccessDeniedAuthorizeAttribute : AuthorizeAttribute
    {
        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        public AccessDeniedAuthorizeAttribute()
        {
            Roles = base.Roles;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                //if (HttpContext.Current.User == null || !HttpContext.Current.User.Identity.IsAuthenticated)
                //{
                //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Authentication", action = "Index" }));
                //}

                if (!String.IsNullOrEmpty(Roles))
                {
                    string[] rolesArray = Roles.Split(',');
                    bool Authorize = false;
                    foreach (string currRole in rolesArray)
                    {
                        Authorize = CurrentUser.IsInRole(currRole);
                        if (Authorize)
                        {
                            break;
                        }
                    }
                    if (!Authorize)
                    {
                        filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Authentication", action = "AccessDenied" }));
                        //Logger.Write("Access Denied", "On Authorization", "Access Denied", "User " + CurrentUser + "Not in Role" + Roles);

                        // base.OnAuthorization(filterContext); //returns to login url
                    }
                }

                if (!String.IsNullOrEmpty(Users))
                {
                    if (!Users.Contains(CurrentUser.Id.ToString()))
                    {
                        filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Authentication", action = "AccessDenied" }));

                        // base.OnAuthorization(filterContext); //returns to login url
                    }
                }
            }
            else
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new RedirectResult("~/Error/AjaxUnauthorized");
                }
                else
                {
                    //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Authentication", action = "Login" }));
                    //filterContext.Result = new ViewResult { ViewName = "Login" };
                    //filterContext.Result = new RedirectResult("/Authentication/Login");

                    filterContext.Result = new RedirectResult(string.Format("~/Authentication/Login")); return;
                }
            }
        }

        /*
            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                base.OnAuthorization(filterContext);

                if (filterContext.Result is HttpUnauthorizedResult)
                {
                    filterContext.Result = new RedirectResult("/Authentication/AccessDenied");
                }
            }
        */
    }
}