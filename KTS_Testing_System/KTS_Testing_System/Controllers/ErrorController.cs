using KTS_Testing_System.Classes;
using KTS_Testing_System.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static KTS_Testing_System.Classes.Constants;

namespace KTS_Testing_System.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult HttpError404()
        {
            return View();
        }

        // GET: // server error
        public ActionResult HttpError500()
        {
            return View();
        }

        // GET: 
        public ActionResult General()
        {
            return View();
        }

        public ActionResult AjaxUnauthorized()
        {
            return View();
        }

        public ActionResult ErrorFooter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Message(CustomErrorVM argError)
        {
            return View("CustomError", argError);
        }

        string browserCompatibilityView = "~/Views/Error/BrowserCompatibility.cshtml";

        public ActionResult Compatibility()
        {
            string GlobalResult_Type = "success";
            string GlobalActivity_Type = "";
            string GlobalRequest_Type = "";
            string GlobalMessage = "";
            string GlobalStack_Trace = System.Web.HttpContext.Current.Request.Browser.Browser + " - " + System.Web.HttpContext.Current.Request.Browser.Version;

            try
            {
                GlobalMessage = GlobalStack_Trace = System.Web.HttpContext.Current.Request.Browser.Browser + " - " + System.Web.HttpContext.Current.Request.Browser.Version + "-UA=" + System.Web.HttpContext.Current.Request.UserAgent;
                return View(browserCompatibilityView);
            }
            catch (Exception ex)
            {
                GlobalResult_Type = "exception";
                GlobalMessage = ex.Message.Equals(ERROR_TITLE.CUSTOM_ERROR) ? ex.InnerException.Message : ex.Message;
                GlobalStack_Trace = ex.StackTrace;
                return View(Constants.CUSTOM_ERROR_URL, Logger.DefineCustomError(ex));
            }
            finally
            {
                Logger.CreateAuditLog("Compatibility", "Compatibility", this.ControllerContext.RouteData.Values["action"].ToString(), USER_ROLES.admin, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, null);
            }

        }
    }
}