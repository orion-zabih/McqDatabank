using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using KTS_Entity;
using KTS_Testing_System.Classes;

namespace KTS_Testing_System.Filter
{
    public class LoggingFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        Kts_dataEntities context;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                //INITIALIZING VARIABLES
                string actionName = "";
                string controllerName = "";
                string moduleCode = "";
                string HttpRequestMethod = filterContext.HttpContext.Request.HttpMethod;
                object userCode = null;
               // object electionCode = null;
                //roles on top of action
                var filters = new List<FilterAttribute>();
                filters.AddRange(filterContext.ActionDescriptor.GetFilterAttributes(false));
                filters.AddRange(filterContext.ActionDescriptor.ControllerDescriptor.GetFilterAttributes(false));
                //var roles = filters.OfType<AuthorizeAttribute>().Select(f => f.Roles);
                //SETTING COMMON VARIABLES
                actionName = filterContext.ActionDescriptor.ActionName;
                controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                if (controllerName != "ElectionsList" && controllerName != "Error" && actionName != "GetLocationCodes")
                    using (context = new Kts_dataEntities())
                    {                        

                        //GET USER INFO (USER ID / ELECTION CODE)
                        if ((HttpContext.Current.User as CustomPrincipal) != null)
                        {
                            userCode = (int)(HttpContext.Current.User as CustomPrincipal).Id;                          

                        }
                        
                        switch (controllerName)
                        {
                            case "Home":
                            case "Authentication":
                                moduleCode = "system";
                                break;
                            case "User":
                                moduleCode = "system"; //add new module code as 'user' or remove constraint from auditTrail table wrt module_code
                                break;
                            case "PollingVenue":
                                moduleCode = "pva";
                                break;
                            case "CandidateNomination":
                                moduleCode = "cn";
                                break;
                            case "PartyRegistration":
                                moduleCode = "pr";
                                break;
                            case "PostalVoter":
                                moduleCode = "pv";
                                break;
                            case "Logistic":
                            case "Shipment":
                            case "Inventory":
                            case "Packages":
                                moduleCode = "logistic";
                                break;
                            case "IndustrialElection":
                                moduleCode = "ie";
                                break;
                            default:
                                moduleCode = "system";
                                break;
                        }
                        
                    }
                base.OnActionExecuting(filterContext);

            }
            catch (Exception ex)
            {

            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            object obj = filterContext.RequestContext;
            var model = filterContext.Controller.ViewData.Model;

            if (filterContext.Exception != null)
                filterContext.HttpContext.Trace.Write("(Logging Filter)Exception thrown");
            /*
            context = new EntitiesModel();
            context.Add(new Audit_logs { Election_code = 1, Log_details = "OnActionExecuting", Module_code = "OnActionExecuting", System_ip = "", User_code = 1 });
            context.SaveChanges();
            */
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //filterContext.HttpContext.Trace.Write("(Logging Filter)Action Executing: " + filterContext.ActionDescriptor.ActionName);
            /*
            context = new EntitiesModel();
            context.Add(new Audit_logs { Election_code = 1, Log_details = "OnActionExecuting", Module_code = "OnActionExecuting", System_ip = "", User_code = 1 });
            context.SaveChanges();
            */
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
                filterContext.HttpContext.Trace.Write("(Logging Filter)Exception thrown");
            /*
            context = new EntitiesModel();
            context.Add(new Audit_logs { Election_code = 1, Log_details = "OnActionExecuting", Module_code = "OnActionExecuting", System_ip = "", User_code = 1 });
            context.SaveChanges();
            */
            base.OnResultExecuted(filterContext);
        }

        //protected override void OnActionExecuting(ResultExecutingContext ctx)
        //{
        //    base.OnResultExecuting(ctx);
        //    ctx.HttpContext.Trace.Write("Log: OnResultExecuting",
        //       "Before Result " +
        //         ctx.HttpContext.Timestamp.Ticks.ToString());
        //}
    }
}