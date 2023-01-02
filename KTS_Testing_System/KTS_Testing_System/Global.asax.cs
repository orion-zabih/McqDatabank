using KTS_Testing_System.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using static KTS_Testing_System.Classes.Constants;

namespace KTS_Testing_System
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var options = new DataTables.AspNet.Mvc5.Options()
          .EnableRequestAdditionalParameters()
          .EnableResponseAdditionalParameters();

            var binder = new DataTables.AspNet.Mvc5.ModelBinder();

            DataTables.AspNet.Mvc5.Configuration.RegisterDataTables(options, binder);
            MvcHandler.DisableMvcResponseHeader = true;
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();
            bool isAjaxCall = new HttpRequestWrapper(Context.Request).IsAjaxRequest();
            HttpException httpException = exception as HttpException;

            //NEED TO GRACEFULLY HANDLE SUCH ERRORS + PROPER LOG
            /*
            if (httpException == null)
                httpException = new HttpException(-1, "Auth Failed");
            */


            if (httpException != null)
            {
                string action;
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found
                        action = "HttpError404";
                        break;
                    case 500:
                        // server error
                        action = "HttpError500";
                        break;
                    default:
                        action = "General";
                        break;
                }

                Logger.Write("General", action, exception.Message.ToString(), exception.StackTrace.ToString());

                // clear error on server
                Server.ClearError();
                if (isAjaxCall)
                {
                    Context.Response.ContentType = "application/json";
                    Context.Response.StatusCode = httpException.GetHttpCode();
                    Context.Response.Write(
                        new JavaScriptSerializer().Serialize(
                            new { type = MESSAGE_TYPE.ERROR, error = exception.Message, stacktrace = exception.StackTrace }
                        )
                    );

                }
                else
                {
                    //Response.Redirect(String.Format("~/Error/{0}/?message={1}", action, exception.Message));
                    Response.Redirect(String.Format("~/Error/{0}/?message={1}", action, ""));
                }

            }
            if (!isAjaxCall)
            {
                // TO HANDLE YELLOW ERROR SCREEN BY REDIRECTING TO GENERAL ERROR PAGE
                Response.Redirect(String.Format("~/Error/General/?message={0}", ""));
            }

        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.Url.AbsoluteUri.Contains("/Content") || HttpContext.Current.Request.Url.AbsoluteUri.Contains("/content") || HttpContext.Current.Request.Url.AbsoluteUri.Contains("/Scripts"))
                return;

            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                        if (authCookie != null)
                        {
                            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                            if (authTicket.Expired)
                            {
                                JavaScriptSerializer serializer = new JavaScriptSerializer();
                                CustomPrincipalSerializeModel serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(authTicket.UserData);
                                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                                newUser.Id = serializeModel.Id;
                                FormsAuthentication.SignOut();
                                Session["GUID"] = null;
                            }
                            else
                            {
                                JavaScriptSerializer serializer = new JavaScriptSerializer();
                                CustomPrincipalSerializeModel serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(authTicket.UserData);
                                CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                                newUser.Id = serializeModel.Id;
                                newUser.FirstName = serializeModel.FirstName;
                                newUser.LastName = serializeModel.LastName;
                                newUser.Designation = serializeModel.designation;
                               

                                newUser.DepartmentCode = serializeModel.department_code;
                                newUser.DepartmentName = serializeModel.department_name;
                                
                                //newUser.lstRoles = serializeModel.roles;
                                HttpContext.Current.User = newUser;
                                //string userData = JsonConvert.SerializeObject(new
                                //{
                                //    serializeModel
                                //}, Formatting.Indented,
                                //new JsonSerializerSettings()
                                //{
                                //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                                //    PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None
                                //});
                                string userData = serializer.Serialize(serializeModel);
                                authTicket = new FormsAuthenticationTicket(
                                         2,
                                         newUser.Id.ToString(),
                                         DateTime.Now,
                                         DateTime.Now.AddMinutes(10),
                                         false,
                                         userData);
                                string encTicket = FormsAuthentication.Encrypt(authTicket);

                                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                                //faCookie.Path = "kts_testing_sys";
                                Response.Cookies.Add(faCookie);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    FormsAuthentication.SignOut();
                    //FormsAuthentication.RedirectToLoginPage(null);
                    //Response.Redirect("/Authentication/Login");
                    //Response.RedirectToRoute("~/Authentication/Login");
                }
            }
        }

        protected void Application_EndRequest()
        {
            // Any AJAX request that ends in a redirect should get mapped to an unauthorized request
            // since it should only happen when the request is not authorized and gets automatically
            // redirected to the login page.
            var context = new HttpContextWrapper(Context);
            if (context.Response.StatusCode == 302 && context.Request.IsAjaxRequest())
            {
                context.Response.Clear();
                Context.Response.StatusCode = 401;
                //return RedirectToAction("Login", "Authentication");

            }

        }
    }
}
