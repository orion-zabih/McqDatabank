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
    public class SubjectsController : BaseController
    {
        string GlobalResult_Type = "success";
        string GlobalActivity_Type = "";
        string GlobalRequest_Type = "";
        string GlobalMessage = "";
        string GlobalStack_Trace = "";
        readonly string indexView = "~/Views/Subjects/Index.cshtml";
        readonly string createView = "~/Views/Subjects/Create.cshtml";
        readonly string editView = "~/Views/Subjects/Edit.cshtml";
        public void SetAuditTrailVariables(string argActivityType, string argRequestType)
        {
            //SETTING VALUES FOR AUDIT TRAIL
            GlobalActivity_Type = argActivityType;
            GlobalRequest_Type = argRequestType;
            GlobalMessage = "";
            GlobalStack_Trace = "";
            GlobalResult_Type = "success";
        }
        // GET: Subjects
        public ActionResult Index(int? Page)
        {
            using (Kts_dataEntities context = new Kts_dataEntities())
            {
                SubjectsIndexVM objSubjects = new SubjectsIndexVM();
                return View(indexView, objSubjects);
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

                    IQueryable<subject> query = context.subjects;//.Where(g=>g.ac;

                    var data = query.Select(g => new
                    {
                        subject_id = g.subject_id,
                        description = g.description
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
                    string.Empty ? "subject_id desc" : orderByString);
                    List<FilteredSubjects> filteredSubjectsList = new List<FilteredSubjects>();
                    int filteredDataCount = 0;
                    // Apply filters for searching
                    if (!string.IsNullOrEmpty(request.Search.Value))
                    {
                        var sch_string = request.Search.Value.ToLower();


                        var filteredData = data.Where(_item =>
                                                     _item.description.ToLower().Contains(sch_string)
                                                    );
                        var dataPage = filteredData.Skip(request.Start).Take(request.Length).ToList();




                        filteredDataCount = filteredData.Count();
                        int count = 0;
                        dataPage.ForEach(g =>
                        {
                            count++;
                            filteredSubjectsList.Add(new FilteredSubjects
                            {
                                serial_no = count,
                                subject_id = g.subject_id,
                                description = g.description
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
                        int count = 0;
                        dataPage.ForEach(g =>
                        {
                            count++;
                            filteredSubjectsList.Add(new FilteredSubjects
                            {
                                serial_no = count,
                                subject_id = g.subject_id,
                                description = g.description
                            });
                        });



                        //var response = DataTablesResponse.Create(request, data.Count(), filteredDataCount, filteredData);
                        //var dtjson = new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
                        //return dtjson;

                        #endregion
                    }
                    var response = DataTablesResponse.Create(request, data.Count(), filteredDataCount, filteredSubjectsList);
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
                    _librartVM.subject = new subject();
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
                        if (context.subjects.Any(s => s.description == libraryVM.subject.description))
                        {
                            ModelState.AddModelError("", "Subject already exists.");
                            throw new Exception("validation");
                        }
                        #region SUBJECT_ADD 
                        if (libraryVM.subject.subject_id == 0)
                        {                            
                            libraryVM.subject.description = libraryVM.subject.description != null ? libraryVM.subject.description.Trim() : null;
                           
                            context.subjects.Add(libraryVM.subject);
                        }
                        #endregion


                        else
                        {
                            throw new HttpException(500, "Invalid Create Subject Request");
                        }


                        context.SaveChanges();
                        SubjectsIndexVM objSubjects = new SubjectsIndexVM();
                        objSubjects.MessageTitle = GlobalResult_Type = "Success";
                        objSubjects.MessageDescription = GlobalMessage = "New Subject: " + libraryVM.subject.description + " successfully created.";
                        return View(indexView, objSubjects);
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

                    libraryVM.MessageDescription = "An Error has Occurred while Inserting Subject";
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

            if (argViewModel.subject == null || string.IsNullOrEmpty(argViewModel.subject.description))
            {
                ModelState.AddModelError("", "Please enter subject.");
                throw new Exception("validation");
            }
            else
            {
                if (argViewModel.subject.description.Length > 128)
                {
                    ModelState.AddModelError("", "Subject Length Exceeds available Limit of 128 characters.");
                    errors = true;
                }
                else if (argViewModel.subject.description.Length < 3)
                {
                    ModelState.AddModelError("", "Subject Length must be atleast 3 characters.");
                    errors = true;
                }
                if (!Regex.IsMatch(argViewModel.subject.description, "^[a-zA-Z0-9._-]+"))
                {
                    ModelState.AddModelError("", "Only Alphanumeric Characters and Underscores Allowed in Subject");
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
                    throw new HttpException(500, "Invalid Edit Subject Request");
                using (Kts_dataEntities context = new Kts_dataEntities())
                {
                    long subject_id = long.Parse(id);
                    if (subject_id == 0)
                        throw new HttpException(500, "Invalid Edit Subject Request");

                    _librartVM.subject = context.subjects.Where(s => s.subject_id == subject_id).FirstOrDefault();
                    if (_librartVM.subject == null)
                        throw new HttpException(500, "Invalid Edit Subject Request");
                    

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
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Submitted for Subject: " + _librartVM.subject.description);



            //TRIM SPACES AT START AND END
            _librartVM.subject.description = _librartVM.subject.description != null ? _librartVM.subject.description.Trim() : null;
            // ####################### SERVER SIDE VALIDATIONS HERE ###########################
            //---------------------------------------------------------------------------------   

            Validation(_librartVM);

            try
            {
                if (ModelState.IsValid)
                {
                    using (Kts_dataEntities context = new Kts_dataEntities())
                    {
                        var dbObj = context.subjects.FirstOrDefault(x => x.subject_id == _librartVM.subject.subject_id);

                        //_librartVM.subject.description = dbObj.description;
                        if (_librartVM.subject.subject_id != 0)
                        {
                            #region SUBJECT_MODIFY


                            ObjectExt.CopyFrom(dbObj, _librartVM.subject);

                            #endregion


                        }

                        context.SaveChanges();
                        SubjectsIndexVM objSubjects = new SubjectsIndexVM();
                        objSubjects.MessageTitle = GlobalResult_Type = "Success";
                        objSubjects.MessageDescription = GlobalMessage = "Subject: " + dbObj.description + " successfully updated.";
                        return View(indexView, objSubjects);
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
                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.LibraryManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Submitted for subject: " + _librartVM.subject.description, Constants.USER_ROLES.library_management, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);
            }
        }
    }
}