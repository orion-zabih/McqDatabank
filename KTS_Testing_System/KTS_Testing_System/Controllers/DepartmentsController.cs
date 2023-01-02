using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using KTS_Entity;
using KTS_Testing_System.Classes;
using KTS_Testing_System.Extensions;
using KTS_Testing_System.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;


namespace KTS_Testing_System.Controllers
{
    [AccessDeniedAuthorizeAttribute(Roles = Constants.USER_ROLES.library_management)]
    public class DepartmentsController : BaseController
    {
        string GlobalResult_Type = "success";
        string GlobalActivity_Type = "";
        string GlobalRequest_Type = "";
        string GlobalMessage = "";
        string GlobalStack_Trace = "";
        readonly string indexView = "~/Views/Departments/Index.cshtml";
        readonly string createView = "~/Views/Departments/Create.cshtml";
        readonly string editView = "~/Views/Departments/Edit.cshtml";
        public void SetAuditTrailVariables(string argActivityType, string argRequestType)
        {
            //SETTING VALUES FOR AUDIT TRAIL
            GlobalActivity_Type = argActivityType;
            GlobalRequest_Type = argRequestType;
            GlobalMessage = "";
            GlobalStack_Trace = "";
            GlobalResult_Type = "success";
        }
        // GET: Departments
        public ActionResult Index(int? Page)
        {
            using (Kts_dataEntities context = new Kts_dataEntities())
            {
                DepartmentIndexVM objDepartments = new DepartmentIndexVM();
                return View(indexView, objDepartments);
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

                    IQueryable<Department> query = context.Departments;//.Where(g=>g.ac;

                    var data = query.Select(g => new
                    {
                        department_id = g.department_id,
                        description = g.description,
                        //user = g.Users != null ? g.Users.first_names + " " + g.User.last_name : "",
                        insertion_timestamp = g.insertion_timestamp

                    });

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
                    string.Empty ? "question_id desc" : orderByString);
                    List<FilteredDepartments> filteredDepartmentsList = new List<FilteredDepartments>();
                    int filteredDataCount = 0;
                    // Apply filters for searching
                    if (!string.IsNullOrEmpty(request.Search.Value))
                    {
                        var sch_string = request.Search.Value.ToLower();


                        var filteredData = data.Where(_item =>
                                                     _item.description.ToLower().Contains(sch_string)
                                                    //|| _item.user.ToLower().Contains(sch_string)
                                                    || (!string.IsNullOrEmpty(_item.description) ? _item.description : "").ToLower().Contains(sch_string)
                                                    //|| (!string.IsNullOrEmpty(_item.difficulty_code) ? _item.difficulty_code : "").ToLower().Contains(sch_string)
                                                    //|| (!string.IsNullOrEmpty(_item.importance_code) ? _item.importance_code : "").ToLower().Contains(sch_string)
                                                    //|| _item.marks.ToString().Contains(sch_string)
                                                    //|| (!string.IsNullOrEmpty(_item.question_level) ? _item.question_level : "").ToLower().Contains(sch_string)
                                                    //|| (_item.insertion_timestamp!=null ? Utility.DateFormat(_item.insertion_timestamp, "dd-MM-yyyy hh:mm:ss tt") : "").ToLower().Contains(sch_string)
                                                    );
                        var dataPage = filteredData.Skip(request.Start).Take(request.Length).ToList();




                        filteredDataCount = filteredData.Count();
                        int count = 0;
                        dataPage.ForEach(g =>
                        {
                            count++;
                            filteredDepartmentsList.Add(new FilteredDepartments
                            {
                                serial_no = count,
                                department_id = g.department_id,
                                description = g.description,
                                insertion_timestamp = Utility.DateFormat(g.insertion_timestamp, "dd-MM-yyyy hh:mm:ss tt")
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
                        int count = 0;
                        var dataPage = filteredData.Skip(request.Start).Take(request.Length).ToList();
                        dataPage.ForEach(g =>
                        {
                            count++;
                            filteredDepartmentsList.Add(new FilteredDepartments
                            {
                                serial_no = count,
                                department_id = g.department_id,
                                description = g.description,
                                insertion_timestamp = Utility.DateFormat(g.insertion_timestamp, "dd-MM-yyyy hh:mm:ss tt")
                            });
                        });



                        //var response = DataTablesResponse.Create(request, data.Count(), filteredDataCount, filteredData);
                        //var dtjson = new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
                        //return dtjson;

                        #endregion
                    }
                    var response = DataTablesResponse.Create(request, data.Count(), filteredDataCount, filteredDepartmentsList);
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

                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.LibraryManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), Constants.USER_ROLES.library_management, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);

            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Page Called");
            LibraryVM _librartVM = new LibraryVM();
            try
            {

                using (Kts_dataEntities context = new Kts_dataEntities())
                // Kts_dataEntities context = new Kts_dataEntities();
                {
                    _librartVM.department = new Department();
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

                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.LibraryManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Page Called", Constants.USER_ROLES.library_management, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);

            }
            return View(createView, _librartVM);
        }
        [HttpPost]
        public ActionResult Create(LibraryVM libraryVM)
        {

            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Submitted");
            try
            {
                //VALIDATION HERE               
                Validation(libraryVM);

                if (ModelState.IsValid)
                {
                    using (Kts_dataEntities context = new Kts_dataEntities())
                    {
                        //
                        if (context.Departments.Any(s => s.description == libraryVM.department.description))
                        {
                            ModelState.AddModelError("", "Department already exists.");
                            throw new Exception("validation");
                        }
                        #region DEPARTMENT_ADD 
                        if (libraryVM.department.department_id == 0)
                        {
                            libraryVM.department.description = libraryVM.department.description != null ? libraryVM.department.description.Trim() : null;
                            libraryVM.department.insertion_timestamp = Utility.GetServerDateTime(context);
                            context.Departments.Add(libraryVM.department);
                        }
                        #endregion


                        else
                        {
                            throw new HttpException(500, "Invalid Create Department Request");
                        }


                        context.SaveChanges();
                        DepartmentIndexVM objDepartments = new DepartmentIndexVM();
                        objDepartments.MessageTitle = GlobalResult_Type = "Success";
                        objDepartments.MessageDescription = GlobalMessage = "New Department: " + libraryVM.department.description + " successfully created.";
                        return View(indexView, objDepartments);
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

                    libraryVM.MessageDescription = "An Error has Occurred while Inserting Department";
                }

                GlobalResult_Type = "exception";
                GlobalMessage = ex.Message.Equals(Constants.ERROR_TITLE.CUSTOM_ERROR) ? ex.InnerException.Message : ex.Message;
                GlobalStack_Trace = ex.StackTrace;

                return View(createView, libraryVM);
            }
            finally
            {
                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.LibraryManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Request Submitted", Constants.USER_ROLES.library_management, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);
            }
        }
        #region VALIDATIONS
        private bool Validation(LibraryVM argViewModel)
        {
            bool errors = false;

            if (argViewModel.department == null || string.IsNullOrEmpty(argViewModel.department.description))
            {
                ModelState.AddModelError("", "Please enter Department.");
                throw new Exception("validation");
            }
            else
            {
                if (argViewModel.department.description.Length > 128)
                {
                    ModelState.AddModelError("", "Department Length Exceeds available Limit of 128 characters.");
                    errors = true;
                }
                else if (argViewModel.department.description.Length < 3)
                {
                    ModelState.AddModelError("", "Department Length must be atleast 3 characters.");
                    errors = true;
                }
                if (!Regex.IsMatch(argViewModel.department.description, "^[a-zA-Z0-9._-]+"))
                {
                    ModelState.AddModelError("", "Only Alphanumeric Characters and Underscores Allowed in Department");
                    errors = true;
                }
            }

            return errors;
        }
        #endregion
        public ActionResult Edit(string id)
        {
            LibraryVM _librartVM = new LibraryVM();
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Page Called for id:" + id);
            try
            {

                if (string.IsNullOrEmpty(id))
                    throw new HttpException(500, "Invalid Edit Department Request");
                using (Kts_dataEntities context = new Kts_dataEntities())
                {
                    long department_id = long.Parse(id);
                    if (department_id == 0)
                        throw new HttpException(500, "Invalid Edit Department Request");

                    _librartVM.department = context.Departments.Where(s => s.department_id == department_id).FirstOrDefault();
                    if (_librartVM.department == null)
                        throw new HttpException(500, "Invalid Edit Department Request");


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

                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.LibraryManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Page Called", Constants.USER_ROLES.library_management, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);

            }

            return View(editView, _librartVM);
        }
        [HttpPost]
        public ActionResult Update(LibraryVM _librartVM)
        {
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Submitted for Department: " + _librartVM.department.description);



            //TRIM SPACES AT START AND END
            _librartVM.department.description = _librartVM.department.description != null ? _librartVM.department.description.Trim() : null;
            // ####################### SERVER SIDE VALIDATIONS HERE ###########################
            //---------------------------------------------------------------------------------   

            Validation(_librartVM);

            try
            {
                if (ModelState.IsValid)
                {
                    using (Kts_dataEntities context = new Kts_dataEntities())
                    {
                        var dbObj = context.Departments.FirstOrDefault(x => x.department_id == _librartVM.department.department_id);

                        //_librartVM.Department.description = dbObj.description;
                        if (_librartVM.department.department_id != 0)
                        {
                            #region DEPARTMENT_MODIFY


                            ObjectExt.CopyFrom(dbObj, _librartVM.department);

                            #endregion


                        }

                        context.SaveChanges();
                        DepartmentIndexVM objDepartments = new DepartmentIndexVM();
                        objDepartments.MessageTitle = GlobalResult_Type = "Success";
                        objDepartments.MessageDescription = GlobalMessage = "Department: " + dbObj.description + " successfully updated.";
                        return View(indexView, objDepartments);
                    }
                }
                else
                {
                    throw new Exception("validation");
                }
            }
            catch (Exception ex)
            {

                GlobalResult_Type = "exception";
                GlobalMessage = ex.Message.Equals(Constants.ERROR_TITLE.CUSTOM_ERROR) ? ex.InnerException.Message : ex.Message;
                GlobalStack_Trace = ex.StackTrace;

                return View(editView, _librartVM);
            }
            finally
            {
                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.LibraryManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Submitted for Department: " + _librartVM.department.description, Constants.USER_ROLES.library_management, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);
            }
        }
    }
}