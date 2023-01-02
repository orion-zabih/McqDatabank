using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KTS_Entity;
using KTS_Testing_System.Models;
using KTS_Testing_System.Classes;
using System.Web.Script.Serialization;
using System.Web.Security;
using static KTS_Testing_System.Classes.Constants;

namespace KTS_Testing_System.Controllers
{
    public class AuthenticationController : BaseController
    {
        // GET: Authentication
        string GlobalResult_Type = "success";
        string GlobalActivity_Type = "";
        string GlobalRequest_Type = "";
        string GlobalMessage = "";
        string GlobalStack_Trace = "";
        readonly string changePasswordView = "~/Views/User/ChangePasswordByAdmin.cshtml";
        public ActionResult Index()
        {
           return  View("Login");
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Login(string returnUrl)
        {
            //var comp = new BrowserCompatibility();
            //if (!comp.IsValidBrowser())
            //{
            //    return RedirectToAction("Compatibility", "Error");
            //}

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        public ActionResult Authenticate(LoginViewModel model, string returnUrl)
        {
            //Kts_dataEntities context = new Kts_dataEntities();
            string user_id = model.username.ToLower();
            try
            {
                if (ModelState.IsValid)
                {
                    using (var entities = new Kts_dataEntities())
                    {
                        //entities.Database.Connection.ConnectionString = "DATA SOURCE=10.10.130.30:1522/Ora12c;PASSWORD=ecp_rts_qa;PERSIST SECURITY INFO=True;USER ID=ECP_RTS_QA";
                        string username = model.username.ToLower();
                        string HashPassword = Utility.MD5(model.Password);

                        //string HashPassword = model.Password;// Utility.MD5(model.Password);

                        var obj = entities.Users.SingleOrDefault(user => user.username.ToLower() == username && user.password == HashPassword);

                        //Claim claim = new ClaimsIdentity 
                        if (obj != null)
                        {
                            user_id = obj.user_Id.ToString();

                            if (obj.status.Equals("banned") || obj.status.Equals("deleted"))
                            {
                                GlobalMessage = "user banned/deleted";
                                GlobalResult_Type = "failed";
                                ModelState.AddModelError("", "Access against given user is revoked.");
                            }
                            //else if (obj.CHANGED_PWD == "t")
                            //{
                            //    //redirect to renew password
                            //    ChangePasswordRequestModel objChangePassword = new ChangePasswordRequestModel();
                            //    objChangePassword.Username = obj.USERNAME;
                            //    objChangePassword.OldPassword = obj.PASSWORD;


                            //    var key = Utility.GetUserSecretKey(obj.USER_ID, ref context);
                            //    string EncriptedUserName = Utility.EncryptUrlEncodedQueryString(obj.USERNAME, key);

                            //    return RedirectToAction("ChangePassword", new RouteValueDictionary(new { UserName = EncriptedUserName, uid = obj.USER_ID }));
                            //}

                            //else if (obj.PASSWORD_EXPIRY_DATE <= DateTime.Now)
                            //{
                            //    //redirect to renew password
                            //    ChangePasswordRequestModel objChangePassword = new ChangePasswordRequestModel();
                            //    objChangePassword.Username = obj.USERNAME;
                            //    objChangePassword.OldPassword = obj.PASSWORD;


                            //    var key = Utility.GetUserSecretKey(obj.USER_ID, ref context);
                            //    string EncriptedUserName = Utility.EncryptUrlEncodedQueryString(obj.USERNAME, key);
                            //    GlobalMessage = "Forcefully change password";
                            //    GlobalResult_Type = "Success";

                            //    return RedirectToAction("ChangePassword", new RouteValueDictionary(new { UserName = EncriptedUserName, uid = obj.USER_ID }));
                            //}
                            //else if (obj.Password_expiry_date < Utility.GetServerDateTime(ref entities))
                            //{
                            //    //redirect to renew password
                            //    //ChangePasswordRequestModel objChangePassword = new ChangePasswordRequestModel();
                            //    //objChangePassword.Username = obj.Username;
                            //    //objChangePassword.OldPassword = obj.Password;
                            //    return RedirectToAction("ChangePassword" , new RouteValueDictionary(new { controller = "Authentication", action = "ChangePassword", UserName = obj.Username }));
                            //}
                            else
                            {
                                //int[] roleMap = obj.User_role_maps.Select(s => s.Role_code.Value).ToArray();

                                CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                                serializeModel.Id = obj.user_Id;
                                serializeModel.FirstName = obj.first_names;
                                serializeModel.LastName = obj.last_name;
                                serializeModel.designation = obj.designation;
                                //serializeModel.roleMap = roleMap;
                                if(obj.Department!=null)
                                {
                                    serializeModel.department_code = obj.Department.department_id;
                                    serializeModel.department_name = obj.Department.description;                                    
                                }
                                
                                

                               
                                JavaScriptSerializer serializer = new JavaScriptSerializer();
                                string userData = serializer.Serialize(serializeModel);
                                //string userData = JsonConvert.SerializeObject(new
                                //{
                                //    serializeModel
                                //}, Formatting.Indented,
                                //new JsonSerializerSettings()
                                //{
                                //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                                //    PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None
                                //});

                                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                         2,
                                         obj.user_Id.ToString(),
                                         DateTime.Now,
                                         DateTime.Now.AddMinutes(10),
                                         false,
                                         userData);
                                string encTicket = FormsAuthentication.Encrypt(authTicket);
                                Session["GUID"] = Guid.NewGuid();
                                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                                //faCookie.Path = "ecp_management";

                                Response.Cookies.Add(faCookie);

                                //Insert into Session log Table
                                // TODO: ADD LOGGING TO DATABASE
                                //Activity Log
                                //Utilities.InsertLog(new ACTIVITY_LOGS() { ACTION = "Login", USER_ID = obj.USER_ID, SITE_ID = obj.SITES.SITE_ID, COUNTRY_CODE = obj.SITES.COUNTRY_CODE });

                                GlobalMessage = "User Login to System";
                                GlobalResult_Type = "Success";
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            // TODO: ADD LOGGING TO DATABASE
                            //Logger.LogToDatabase("", null, LogTypes.UserLogin, "Login Requested with Username: " + username, "Login Failed - Redirected to Login", "");
                            GlobalMessage = "User Login to System";
                            GlobalResult_Type = "Fail-invalid user/password";
                            ModelState.AddModelError("", "Username or password provided is incorrect.");
                        }
                    }
                }
                // If we got this far, something failed, redisplay form
                return View("Login", model);
            }
            catch (Exception ex)
            {
                //Logger.Write(this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                GlobalResult_Type = "exception";
                GlobalMessage = ex.Message.Equals(ERROR_TITLE.CUSTOM_ERROR) ? ex.InnerException.Message : ex.Message;
                GlobalStack_Trace = ex.StackTrace;
                return View("Login", model);
            }

            finally
            {
                Logger.CreateAuditLog(SYSTEM_MODULES.Authentication, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), USER_ROLES.admin, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, /*(LoggedUser != null) ? LoggedUser.Id.ToString() : null*/user_id);
            }
        }
        [AccessDeniedAuthorizeAttribute]
        public ActionResult ChangePassword()
        {
            ViewModel.ChangePasswordByAdminModel objChangePassword = new ViewModel.ChangePasswordByAdminModel();
            try
            {
                objChangePassword.user_id = long.Parse(LoggedUser.Id.ToString());
                using (Kts_dataEntities context = new Kts_dataEntities())
                {

                    {
                        var obj = context.Users.SingleOrDefault(user => user.user_Id == objChangePassword.user_id);
                        if (obj != null)
                        {
                            objChangePassword.UserName = obj.username;
                            objChangePassword.FullName = Utility.ToTitlecase(obj.first_names + " " + obj.last_name);
                            objChangePassword.UserType = obj.designation;

                        }

                    }
                }


                GlobalResult_Type = "Success";
                GlobalMessage = "Change Password self page called for Username:" + objChangePassword.UserName;
                return View(changePasswordView, objChangePassword);
            }
            catch (Exception ex)
            {
                Logger.Write(this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                objChangePassword.MessageTitle = "Exception";
                objChangePassword.MessageDescription = "An error occured while updating user password:" + ex.Message.Replace(System.Environment.NewLine, "\\n").Replace(@"\", @"\\\").Replace("'", "\\'").Replace("\n", "\\n");
                GlobalResult_Type = "exception";
                GlobalMessage = ex.Message.Equals(Constants.ERROR_TITLE.CUSTOM_ERROR) ? ex.InnerException.Message : ex.Message;
                GlobalStack_Trace = ex.StackTrace;
                return View(changePasswordView, objChangePassword);
            }
            finally
            {
                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.UserManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), Constants.USER_ROLES.admin, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);
            }
            //return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            //Activity Log
            //Logger.LogToDatabase("", null, LogTypes.UserLogin, "Logout Requested with UserID: " + LoggedUser.Id, "Redirected to Login", "");
            try
            {
                // using (var context = new Entities())
                {
                    //var obj = context.MANAGEMENT_USERS.SingleOrDefault(user => user.USER_ID == LoggedUser.Id);
                    //if (obj != null)
                    //{
                    //    //obj.User_sessions.Add(new User_sessions
                    //    //{
                    //    //    Users = obj,
                    //    //    System_ip = Utility.GetRemoteIPAddress(),
                    //    //    Activity_type = "logout"
                    //    //});
                    //}
                    //context.SaveChanges();
                    Session["GUID"] = null;
                    GlobalResult_Type = "success";
                    GlobalMessage = "logged out";
                }
            }
            catch (Exception ex)
            {
                // Logger.Write(this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                GlobalResult_Type = "exception";
                GlobalMessage = ex.Message.Equals(ERROR_TITLE.CUSTOM_ERROR) ? ex.InnerException.Message : ex.Message;
                GlobalStack_Trace = ex.StackTrace;
                return View("Login", null);
            }

            finally
            {
                Logger.CreateAuditLog(SYSTEM_MODULES.Authentication, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), USER_ROLES.admin, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);
            }

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Authentication", null);

        }
    }
}