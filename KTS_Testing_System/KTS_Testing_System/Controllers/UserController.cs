using KTS_Testing_System.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using KTS_Entity;
using KTS_Testing_System.ViewModel;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System.Data.Entity;
using System.IO;
using System.Text.RegularExpressions;
using KTS_Testing_System.Extensions;

namespace KTS_Testing_System.Controllers
{
    [AccessDeniedAuthorizeAttribute(Roles = Constants.USER_ROLES.admin)]
    public class UserController : BaseController
    {
        readonly string indexView = "~/Views/User/Index.cshtml";
        readonly string createView = "~/Views/User/Create.cshtml";
        readonly string editView = "~/Views/User/Edit.cshtml";
        readonly string changePasswordView = "~/Views/User/ChangePasswordByAdmin.cshtml";
        string GlobalResult_Type = "success";
        string GlobalActivity_Type = "";
        string GlobalRequest_Type = "";
        string GlobalMessage = "";
        string GlobalStack_Trace = "";
        public void SetAuditTrailVariables(string argActivityType, string argRequestType)
        {
            //SETTING VALUES FOR AUDIT TRAIL
            GlobalActivity_Type = argActivityType;
            GlobalRequest_Type = argRequestType;
            GlobalMessage = "";
            GlobalStack_Trace = "";
            GlobalResult_Type = "success";
        }
       
        // GET: User
        public ActionResult Index(int? Page)
        {
            //using ()
            //using(Kts_dataEntities context=new Kts_dataEntities())
            {

                UserIndexVM objUsers = new UserIndexVM();

                //objUsers.UsersList = Lookup.GetAllUsers();

                return View(indexView, objUsers);
            }

        }
        
        [HttpPost]
        public ActionResult GetIndex(IDataTablesRequest request, string id)
        {
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod);
            try
            {
                using (Kts_dataEntities context = new Kts_dataEntities())
                {

                    IQueryable<User> query = context.Users;

                    var data = query.Select(g => new
                    {
                        user_id = g.user_Id,
                        user_name = g.username,
                        first_names = g.first_names,
                        last_name = g.last_name,
                        status = g.status,
                        full_name = g.first_names + " " + g.last_name,
                        user_type = ""
                    });

                    //var data = from q in query
                    //           select new { serial_no = counter++, };
                    var totalCount = query.Count();
                    #region Filtering

                    var sortedColumns = request.Columns.Where(s => s.Sort != null);
                    var orderByString = String.Empty;

                    foreach (var column in sortedColumns)
                    {
                        orderByString += orderByString != String.Empty ? "," : "";
                        if (column.Field == "user_type")
                        {
                            orderByString += "user_level" +
                            (column.Sort.Direction ==
                            SortDirection.Ascending ? " asc" : " desc");
                        }
                        else
                        {
                            orderByString += (column.Field) +
                            (column.Sort.Direction ==
                            SortDirection.Ascending ? " asc" : " desc");
                        }
                    }

                    data = data.OrderBy(orderByString ==
                    string.Empty ? "user_id desc" : orderByString);
                    List<FilteredUser> filteredUserList = new List<FilteredUser>();
                    int filteredDataCount = 0;
                    // Apply filters for searching
                    if (!string.IsNullOrEmpty(request.Search.Value))
                    {
                        var sch_string = request.Search.Value.ToLower();


                        var filteredData = data.Where(_item =>
                                                     _item.user_name.ToLower().Contains(sch_string)
                                                    || _item.first_names.ToLower().Contains(sch_string)
                                                    || _item.last_name.ToLower().Contains(sch_string)
                                                    || sch_string.Contains(_item.first_names.ToLower())
                                                    || sch_string.Contains(_item.last_name.ToLower())
                                                    || _item.status.ToLower().Contains(sch_string)
                                                    );
                        var dataPage = filteredData.Skip(request.Start).Take(request.Length).ToList();




                        filteredDataCount = filteredData.Count();
                        dataPage.ForEach(l =>
                        {
                           
                            filteredUserList.Add(new FilteredUser
                            {
                                user_id = l.user_id,
                                user_name = l.user_name,
                                full_name = Utility.ToTitlecase(l.first_names + " " + l.last_name),
                                first_names=l.first_names,
                                last_name=l.last_name,
                                status = l.status
                            });
                        });
                        //var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), filteredUserList);
                        //var dtjson = new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
                        //return dtjson;
                    }
                    else
                    {

                        var filteredData = data;
                        filteredDataCount = filteredData.Count();
                        var dataPage = filteredData.Skip(request.Start).Take(request.Length).ToList();
                        dataPage.ForEach(l =>
                        {

                            filteredUserList.Add(new FilteredUser
                            {
                                user_id = l.user_id,
                                user_name = l.user_name,
                                full_name = Utility.ToTitlecase(l.first_names + " " + l.last_name),
                                first_names = l.first_names,
                                last_name = l.last_name,
                                status = l.status
                            });
                        });



                        //var response = DataTablesResponse.Create(request, data.Count(), filteredDataCount, filteredData);
                        //var dtjson = new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
                        //return dtjson;

                        #endregion
                    }
                    var response = DataTablesResponse.Create(request, data.Count(), filteredDataCount, filteredUserList);
                    var dtjson = new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
                    return dtjson;
                }
            }
            catch (Exception ex)
            {
                GlobalResult_Type = "exception";
                GlobalMessage = ex.Message.Equals(Constants.ERROR_TITLE.CUSTOM_ERROR) ? ex.InnerException.Message : ex.Message;
                GlobalStack_Trace = ex.StackTrace;
                return View(Constants.CUSTOM_ERROR_URL, Logger.DefineCustomError(ex));
            }
            finally
            {

                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.UserManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), Constants.USER_ROLES.admin, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);

            }
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Page Called");
            UserVM _userVM = new UserVM();
            try
            {

                using (Kts_dataEntities context = new Kts_dataEntities())
               // Kts_dataEntities context = new Kts_dataEntities();
                {
                    _userVM.User = new User();
                    _userVM = PrepareUser(_userVM, context);
                }

                    //var allowed_levels = Lookup.GetUserAllowedLevels(LoggedUser.level_name).OrderByDescending(s => s.RANK).Select(s => s.NAME);
                    
            }
            catch (Exception ex)
            {
                GlobalResult_Type = "exception";
                GlobalMessage = ex.Message.Equals(Constants.ERROR_TITLE.CUSTOM_ERROR) ? ex.InnerException.Message : ex.Message;
                GlobalStack_Trace = ex.StackTrace;
                return View(Constants.CUSTOM_ERROR_URL, Logger.DefineCustomError(ex));
            }
            finally
            {

                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.UserManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Page Called", Constants.USER_ROLES.admin, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);

            }
            return View(createView, _userVM);
        }
        private UserVM PrepareUser(UserVM _userVM, Kts_dataEntities context)
        {
          
            _userVM = GetUpdatedRoles(_userVM, context);
            return _userVM;
        }
        public UserVM GetUpdatedRoles(UserVM userVM,Kts_dataEntities context)
        {
            userVM.Roles = context.Roles.ToList();
            return userVM;
        }
        
        [HttpPost]
        public ActionResult Create(UserVM userVM)
        {

            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Submitted");
            try
            {
                //VALIDATION HERE               
                Validation(userVM);
                if (ModelState.IsValid)
                {
                    using (Kts_dataEntities context = new Kts_dataEntities())
                    {
                        //
                        if (context.Users.Any(s => s.username == userVM.User.username))
                        {
                            ModelState.AddModelError("", "Username already exists with Specified Username.");
                            throw new Exception("validation");
                        }
                        #region USER_ADD 
                        if (userVM.User.user_Id == 0)
                        {
                            userVM.User.insertion_timestamp = Utility.GetServerDateTime(context);
                            userVM.User.creation_user_id = (long)LoggedUser.Id;
                            userVM.User.password = Utility.MD5(userVM.User.password);
                            //TRIM SPACES AT START AND END
                            userVM.User.username = userVM.User.username != null ? userVM.User.username.Trim() : null;
                            userVM.User.first_names = userVM.User.first_names != null ? userVM.User.first_names.Trim() : null;
                            userVM.User.last_name = userVM.User.last_name != null ? userVM.User.last_name.Trim() : null;
                            userVM.User.designation = userVM.User.designation != null ? userVM.User.designation.Trim() : null;
                            //userVM.User.EMAIL = userVM.User.EMAIL != null ? userVM.User.EMAIL.Trim() : null;
                            if (userVM.SelectedRoles != null && userVM.SelectedRoles.Count > 0)
                            {
                                #region USER_ROLES

                                foreach (var role in userVM.SelectedRoles)
                                {
                                    Role objRole = context.Roles.FirstOrDefault(g => g.role_id.ToString() == role);
                                    userVM.User.Roles.Add(objRole);

                                }
                                #endregion
                            }


                            
                            context.Users.Add(userVM.User);
                            //objCitizenVerificationData.INSERTION_TIMESTAMP = serverTime;


                        }
                        #endregion


                        else
                        {
                            throw new HttpException(500, "Invalid Create User Request");
                        }


                        context.SaveChanges();
                        //return RedirectToAction(indexView);
                        UserIndexVM objUsers = new UserIndexVM();
                        objUsers.MessageTitle = GlobalResult_Type = "Success";
                        objUsers.MessageDescription = GlobalMessage = "New user with username: " + userVM.User.username + " successfully created.";

                        return View(indexView, objUsers);
                    }
                }

                else
                {
                    throw new Exception("validation");
                }

            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("validation"))
                {
                    
                    userVM.MessageDescription = "An Error has Occurred while Inserting User";
                }
                using (Kts_dataEntities context = new Kts_dataEntities())
                {
                    userVM = PrepareUser(userVM, context);
                }
                GlobalResult_Type = "exception";
                GlobalMessage = ex.Message.Equals(Constants.ERROR_TITLE.CUSTOM_ERROR) ? ex.InnerException.Message : ex.Message;
                GlobalStack_Trace = ex.StackTrace;

                return View(createView, userVM);
            }
            finally
            {
                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.UserManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Request Submitted", Constants.USER_ROLES.admin, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);
            }
        }
        #region VALIDATIONS
        private bool Validation(UserVM argViewModel)
        {
            bool errors = false;

            if (string.IsNullOrEmpty(argViewModel.User.username))
            {
                ModelState.AddModelError("", "Username is Required.");
                errors = true;
            }
            else
            {
                if (argViewModel.User.username.Length > 32)
                {
                    ModelState.AddModelError("", "Username Length Exceeds available Limit of 32 characters.");
                    errors = true;
                }
                else if (argViewModel.User.username.Length < 3)
                {
                    ModelState.AddModelError("", "Username Length must be atleast 3 characters.");
                    errors = true;
                }
                if (!Regex.IsMatch(argViewModel.User.username, "^[a-zA-Z0-9._-]+"))
                {
                    ModelState.AddModelError("", "Only Alphanumeric Characters and Underscores Allowed in Username");
                    errors = true;
                }
            }
            if (argViewModel.User.user_Id == 0)
            {
                if (string.IsNullOrEmpty(argViewModel.User.password))
                {
                    ModelState.AddModelError("", "Password is Required.");
                    errors = true;
                }
                if (argViewModel.User.password.Length>64)
                {
                    ModelState.AddModelError("", "Password Length Exceeds available Limit of 64 characters.");
                    errors = true;
                }
                else if (!Regex.IsMatch(argViewModel.User.password, @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$"))
                {
                    ModelState.AddModelError("", "Invalid Password\n   PASSWORD POLICY \n" +
            "Passwords will contain at least: \n" +
            "1 upper case letter \n" +
            "1 lower case letter \n" +
            "1 number or special character \n" +
            "8 characters in length \n" +
            "Password maximum length should not be arbitrarily limited");
                    errors = true;
                }
            }


            if (!string.IsNullOrEmpty(argViewModel.User.first_names))
            {
                if (argViewModel.User.first_names.Length > 128)
                {
                    ModelState.AddModelError("", "First name Length Exceeds available Limit of 128 characters.");
                    errors = true;
                }
                if (!Regex.IsMatch(argViewModel.User.first_names, "^[a-zA-Z\\s]+"))
                {
                    ModelState.AddModelError("", "Only Alphabets Allowed in First name");
                    errors = true;
                }
            }

            if (string.IsNullOrEmpty(argViewModel.User.last_name))
            {
                ModelState.AddModelError("", "Last Name is Required");
                errors = true;
            }
            if (!string.IsNullOrEmpty(argViewModel.User.last_name))
            {
                if (argViewModel.User.last_name.Length > 128)
                {
                    ModelState.AddModelError("", "Last name Length Exceeds available Limit of 64 characters.");
                    errors = true;
                }
                if (!Regex.IsMatch(argViewModel.User.last_name, "^[a-zA-Z\\s]+"))
                {
                    ModelState.AddModelError("", "Only Alphabets Allowed in Last name");
                    errors = true;
                }
            }

            if (!string.IsNullOrEmpty(argViewModel.User.designation))
            {
                if (argViewModel.User.designation.Length > 64)
                {
                    ModelState.AddModelError("", "Designation Length Exceeds available Limit of 64 characters.");
                    errors = true;
                }
                if (!Regex.IsMatch(argViewModel.User.designation, "^[a-zA-Z0-9._-]+"))
                {
                    ModelState.AddModelError("", "Only Alphanumeric Characters and Underscores Allowed in Designation");
                    errors = true;
                }
            }

            if (string.IsNullOrEmpty(argViewModel.User.status))
            {
                ModelState.AddModelError("", "Please select status.");
                errors = true;
            }
            if (argViewModel.User.user_Id != 0)
            {
                var error = ModelState.Values.Where(g => g.Errors.Count > 0).FirstOrDefault();
                if (error != null && error.Errors[0].ErrorMessage == "The Password field is required.")
                {
                    if (!errors)
                        ModelState.Clear();
                }
                else if (argViewModel.User.user_Id == 0)
                {
                    if (!errors)
                        ModelState.Clear();
                }
            }

            return errors;
        }
        #endregion
        
        [HttpGet]
        public ActionResult Edit(string id)
        {
            UserVM _userVM = new UserVM();
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Page Called for id:" + id);
            try
            {

                if (string.IsNullOrEmpty(id))
                    throw new HttpException(500, "Invalid Edit User Request");
                using (Kts_dataEntities context = new Kts_dataEntities())
                {
                    long user_id = long.Parse(id);
                    if (user_id == 0)
                        throw new HttpException(500, "Invalid Edit User Request");

                    _userVM.User = context.Users.Where(s => s.user_Id == user_id).FirstOrDefault();
                    if (_userVM.User == null)
                        throw new HttpException(500, "Invalid Edit User Request");

                     _userVM = PrepareUser(_userVM, context);
                    _userVM.SelectedRoles = _userVM.User.Roles.Select(s => s.role_id.ToString()).ToList();
                    
                }
                   
            }
            catch (Exception ex)
            {
                GlobalResult_Type = "exception";
                GlobalMessage = ex.Message.Equals(Constants.ERROR_TITLE.CUSTOM_ERROR) ? ex.InnerException.Message : ex.Message;
                GlobalStack_Trace = ex.StackTrace;
                return View(Constants.CUSTOM_ERROR_URL, Logger.DefineCustomError(ex));
            }
            finally
            {

                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.UserManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Page Called", Constants.USER_ROLES.admin, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);

            }

            return View(editView, _userVM);
        }
        
        [HttpPost]
        public ActionResult Update(UserVM userVM)
        {
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Submitted for user: " + userVM.User.username);


          
            //TRIM SPACES AT START AND END
            userVM.User.username = userVM.User.username != null ? userVM.User.username.Trim() : null;
            userVM.User.first_names = userVM.User.first_names != null ? userVM.User.first_names.Trim() : null;
            userVM.User.last_name = userVM.User.last_name != null ? userVM.User.last_name.Trim() : null;
            userVM.User.designation = userVM.User.designation != null ? userVM.User.designation.Trim() : null;
            // ####################### SERVER SIDE VALIDATIONS HERE ###########################
            //---------------------------------------------------------------------------------   

            Validation(userVM);

            try
            {
                if (ModelState.IsValid)
                {
                    using (Kts_dataEntities context = new Kts_dataEntities())
                    {
                        var dbObj = context.Users.Where(x => x.user_Id == userVM.User.user_Id).FirstOrDefault();

                        userVM.User.password = dbObj.password;
                        userVM.User.creation_user_id = dbObj.creation_user_id;
                        userVM.User.insertion_timestamp = dbObj.insertion_timestamp;
                        //DateTime? InsertionTimeStamp = Utility.GetServerDateTime(context);
                        if (userVM.User.user_Id != 0)
                        {
                            #region USER_MODIFY

                            //OBJECT ASSIGNMENT
                            var rolesToremove = dbObj.Roles.ToList();
                            ObjectExt.CopyFrom(dbObj, userVM.User);

                            #endregion

                            #region RoleMap

                            if (rolesToremove.Count() > 0)
                            {
                                //    context.Roles.RemoveRange(rolesToremove);
                                rolesToremove.ForEach(g =>
                                {
                                    dbObj.Roles.Remove(g);
                                });
                            }

                            if (userVM.SelectedRoles != null && userVM.SelectedRoles.Count != 0)
                            {
                                foreach (var role in userVM.SelectedRoles)
                                {
                                    if (!dbObj.Roles.Any(s => s.role_id.ToString() == role))
                                    {
                                        Role objRole = context.Roles.FirstOrDefault(g => g.role_id.ToString() == role);
                                        dbObj.Roles.Add(objRole);
                                    }
                                    
                                }
                            }
                        }
                        #endregion

                       
                        context.SaveChanges();
                        UserIndexVM objUsers = new UserIndexVM();
                        objUsers.MessageTitle = GlobalResult_Type = "Success";
                        objUsers.MessageDescription = GlobalMessage = "User: " + dbObj.username + " successfully updated.";
                        return View(indexView, objUsers);
                    }
                }
                else
                {
                    throw new Exception("validation");
                }
            }
            catch (Exception ex)
            {
                using (Kts_dataEntities context = new Kts_dataEntities())
                {
                    userVM = PrepareUser(userVM, context);
                }
               
                GlobalResult_Type = "exception";
                GlobalMessage = ex.Message.Equals(Constants.ERROR_TITLE.CUSTOM_ERROR) ? ex.InnerException.Message : ex.Message;
                GlobalStack_Trace = ex.StackTrace;

                return View(editView, userVM);
            }
            finally
            {
                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.UserManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Submitted for user: " + userVM.User.username, Constants.USER_ROLES.admin, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);
            }
        }
        public ActionResult ChangePasswordByAdmin(string id)
        {
            ChangePasswordByAdminModel objChangePassword = new ChangePasswordByAdminModel();
            try
            {
                objChangePassword.user_id = long.Parse(id);
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
                GlobalMessage = "Change Password by Admin page called for Username:" + objChangePassword.UserName;
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
        
        [HttpPost]
        public ActionResult ChangePasswordByAdmin(ChangePasswordByAdminModel model)
        {

            if (string.IsNullOrEmpty(model.NewPassword))
            {
                ModelState.AddModelError("", "Please enter New password");
            }
            if (string.IsNullOrEmpty(model.ConfirmPassword))
            {
                ModelState.AddModelError("", "Please enter Confirm password");
            }
            if (!string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.ConfirmPassword))
            {
                if (model.NewPassword != model.ConfirmPassword)
                    ModelState.AddModelError("", "New password and Confirm password do not match.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    using(Kts_dataEntities context = new Kts_dataEntities())
                {
                        
                        var obj = context.Users.SingleOrDefault(user => user.user_Id == model.user_id);
                        if (obj != null)
                        {
                            obj.password = Utility.MD5(model.NewPassword);
                            
                            context.SaveChanges();
                            //return RedirectToAction("Index");
                            UserIndexVM objUsers = new UserIndexVM();
                            objUsers.MessageTitle = GlobalResult_Type = "Success";
                            objUsers.MessageDescription = GlobalMessage = "Password successfully changed for user: " + obj.username;
                            // objUsers.UsersList = Lookup.GetAllUsers(LoggedUser.level_name, LoggedUser.level_codes,LoggedUser.election_code);//.ToPagedList(Page.Value, 15);

                            return View("Index", objUsers);
                        }
                        else
                        {
                            model.MessageTitle = "Error";
                            model.MessageDescription = "User not found";
                            return View(changePasswordView, model);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    model.MessageTitle = "Exception";
                    model.MessageDescription = "An error occured while updating user password:" + ex.Message.Replace(System.Environment.NewLine, "\\n").Replace(@"\", @"\\\").Replace("'", "\\'").Replace("\n", "\\n");

                    GlobalResult_Type = "exception";
                    GlobalMessage = ex.Message.Equals(Constants.ERROR_TITLE.CUSTOM_ERROR) ? ex.InnerException.Message : ex.Message;
                    GlobalStack_Trace = ex.StackTrace;


                    return View(changePasswordView, model);
                }
                finally
                {
                    Logger.CreateAuditLog(Constants.SYSTEM_MODULES.UserManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), Constants.USER_ROLES.admin, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);
                }

            }
            else
            {
                return View(changePasswordView, model);
            }
        }
    }
}