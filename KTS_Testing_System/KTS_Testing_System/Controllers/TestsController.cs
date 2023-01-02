using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using KTS_Entity;
using KTS_Reporting;
using KTS_Testing_System.Classes;
using KTS_Testing_System.Extensions;
using KTS_Testing_System.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace KTS_Testing_System.Controllers
{
    [AccessDeniedAuthorizeAttribute(Roles = Constants.USER_ROLES.test_compiler)]
    public class TestsController : BaseController
    {
        readonly string indexView = "~/Views/Tests/Index.cshtml";
        readonly string createView = "~/Views/Tests/Create.cshtml";
        readonly string editView = "~/Views/Tests/Edit.cshtml";
        readonly string indexVersionsView = "~/Views/Tests/IndexVersions.cshtml";
        readonly string viewVersionView = "~/Views/Tests/ViewTestVersion.cshtml";
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
        // GET: Tests
        [HttpGet]
        public ActionResult Index()
        {
            TestIndexVM testIndexVM = new TestIndexVM();
            return View(indexView, testIndexVM);
        }
        [HttpPost]
        public ActionResult GetIndex(IDataTablesRequest request, string id)
        {
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod);
            try
            {
                using (Kts_dataEntities context = new Kts_dataEntities())
                {

                    IQueryable<User_Tests> query = context.User_Tests;

                    var data = query.Select(g => new
                    {
                        test_id = g.test_id,
                        user_id = g.user_Id,
                        description = g.description,
                        total_questions = g.total_questions,
                        total_marks = g.total_marks,
                        total_time_minutes = g.total_time_minutes,
                        test_creator = g.test_creator,
                        status = g.status,
                        test_versions = g.test_versions,
                        level = g.Test_questions.FirstOrDefault() != null ? g.Test_questions.FirstOrDefault().question.question_levels.description : "",
                        insertion_timestamp = g.insertion_timestamp
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
                    string.Empty ? "test_id asc" : orderByString);
                    List<FilteredTests> filteredTests = new List<FilteredTests>();
                    int filteredDataCount = 0;
                    // Apply filters for searching
                    if (!string.IsNullOrEmpty(request.Search.Value))
                    {
                        var sch_string = request.Search.Value.ToLower();


                        var filteredData = data.Where(_item =>
                                                     _item.description.ToLower().Contains(sch_string)
                                                    || _item.test_creator.ToLower().Contains(sch_string)
                                                    || _item.level.ToLower().Contains(sch_string)
                                                    || _item.total_questions.ToString().Contains(sch_string)
                                                    || _item.total_marks.ToString().Contains(sch_string)
                                                    || _item.test_versions.ToString().Contains(sch_string)
                                                    || _item.total_time_minutes.ToString().Contains(sch_string)
                                                    || _item.status.ToLower().Contains(sch_string)
                                                    );
                        var dataPage = filteredData.Skip(request.Start).Take(request.Length).ToList();




                        filteredDataCount = filteredData.Count();
                        int count = 0;
                        dataPage.ForEach(l =>
                        {
                            count++;
                            filteredTests.Add(new FilteredTests
                            {
                                serial_no = count,
                                test_id = l.test_id,
                                user_Id = l.user_id,
                                description = l.description,
                                level = l.level,
                                test_creator = Utility.ToTitlecase(l.test_creator),
                                total_questions = l.total_questions,
                                total_marks = l.total_marks,
                                test_versions = l.test_versions,
                                total_time_minutes = l.total_time_minutes,
                                insertion_timestamp_string = Utility.DateFormat(l.insertion_timestamp, "dd-MM-yyyy hh:mm:ss tt"),
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
                        int count = 0;
                        var dataPage = filteredData.Skip(request.Start).Take(request.Length).ToList();
                        dataPage.ForEach(l =>
                        {
                            count++;
                            filteredTests.Add(new FilteredTests
                            {
                                serial_no = count,
                                test_id = l.test_id,
                                user_Id = l.user_id,
                                description = l.description,
                                level = l.level,
                                test_creator = Utility.ToTitlecase(l.test_creator),
                                total_questions = l.total_questions,
                                total_marks = l.total_marks,
                                test_versions = l.test_versions,
                                total_time_minutes = l.total_time_minutes,
                                insertion_timestamp_string = Utility.DateFormat(l.insertion_timestamp, "dd-MM-yyyy hh:mm:ss tt"),
                                status = l.status
                            });
                        });



                        //var response = DataTablesResponse.Create(request, data.Count(), filteredDataCount, filteredData);
                        //var dtjson = new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
                        //return dtjson;

                        #endregion
                    }
                    var response = DataTablesResponse.Create(request, data.Count(), filteredDataCount, filteredTests);
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

                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.TestManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), Constants.USER_ROLES.test_compiler, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);

            }
        }

        public ActionResult Create()
        {
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Page Called");
            TestVM _testVM = new TestVM();
            try
            {

                using (Kts_dataEntities context = new Kts_dataEntities())
                // Kts_dataEntities context = new Kts_dataEntities();
                {
                    _testVM.Tests = new User_Tests();
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

                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.TestManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Page Called", Constants.USER_ROLES.test_compiler, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);

            }
            return View(createView, _testVM);
        }
        private TestVM GetSelectedQuestionsList(TestVM testVM, Kts_dataEntities context)
        {
            int count = 0;
            long questionId = 0;
            testVM.selected_questions.ForEach(s =>
            {
                count++;
                questionId = long.Parse(s);
                question objQuestion = context.questions.FirstOrDefault(q => q.question_id == questionId);
                if (objQuestion != null)
                {
                    testVM.listSelectedQuestions.Add(new SelectedQuestions
                    {
                        serial_no = count,
                        question_id = objQuestion.question_id,
                        level = objQuestion.question_levels.description,
                        subject = objQuestion.subject.description,
                        difficulty = objQuestion.question_difficulty.description,
                        importance = objQuestion.question_importance.description,
                        question = objQuestion.description
                    });
                }

            });
            return testVM;
        }
        [HttpPost]
        public ActionResult GetQuestionsPool(IDataTablesRequest request, string LevelCode, List<string> SubjectCode, string DifficultyCode, string ImportanceCode)
        {
            int DataCount = 0;
            //SETTING DEFAULT VALUES FOR AUDIT TRAIL
            SetAuditTrailVariables("GetQuestionsPool for " + LevelCode + " " + SubjectCode, this.ControllerContext.HttpContext.Request.HttpMethod);
            try
            {
                using (Kts_dataEntities context = new Kts_dataEntities())
                {
                    //IQueryable<MANAGEMENT_USERS> query = Lookup.GetAllUsersIQ(LoggedUser.level_name, LoggedUser.level_code, ref context);
                    string Qry = @"select question_id,l.description level,s.description subject,d.description difficulty,i.description importance,q.description question,q.marks,q.insertion_timestamp,q.user_Id,CONCAT(u.first_names,' ',u.last_name) users 
from questions q 
inner join question_levels l on q.question_level_id=l.level_id 
inner join subjects s on q.subject_id=s.subject_id 
inner join question_difficulty d on q.difficulty_code=d.difficulty_code 
inner join question_importance i on q.importance_code=i.importance_code 
inner join Users u on q.user_Id=u.user_Id
--##level##--where q.question_level_id=##level_id##
--##subject##--and q.subject_id in (##subject_id##)
--##difficulty##--and q.difficulty_code==##difficulty_code##
--##importance##--and q.importance_code=##importance_code##";

                    Qry = Qry.Replace("--##level##--", string.Empty).Replace("##level_id##", LevelCode);

                    if (SubjectCode != null)
                    {
                        string subjects = "";
                        int case_count = 0;
                        SubjectCode.ForEach(g =>
                        {
                            case_count++;
                            //subjects = subjects + "'" + g + "'" + (case_count == SubjectCode.Count ? "" : ",");
                            subjects = subjects + g + (case_count == SubjectCode.Count ? "" : ",");
                        });
                        Qry = Qry.Replace("--##subject##--", string.Empty).Replace("##subject_id##", subjects);
                    }
                    if (!string.IsNullOrWhiteSpace(DifficultyCode))
                    {
                        Qry = Qry.Replace("--##difficulty##--", string.Empty).Replace("##difficulty_code##", DifficultyCode);
                    }
                    if (!string.IsNullOrWhiteSpace(ImportanceCode))
                    {
                        Qry = Qry.Replace("--##importance##--", string.Empty).Replace("##importance_code##", ImportanceCode);
                    }

                    List<FilteredQuestionsPool> listFilteredQuestionsPool = new List<FilteredQuestionsPool>();
                    listFilteredQuestionsPool = context.Database.SqlQuery<FilteredQuestionsPool>(Qry).ToList();
                    var data = listFilteredQuestionsPool.AsEnumerable<FilteredQuestionsPool>().Select(g => new FilteredQuestionsPool
                    {
                        question_id = g.question_id,
                        level = g.level,
                        subject = g.subject,
                        difficulty = g.difficulty,
                        importance = g.importance,
                        question = g.question,
                        marks = g.marks,
                        user_Id = g.user_Id,
                        users = g.users,
                        insertion_timestamp = g.insertion_timestamp,

                    });

                    //var data = from q in query
                    //           select new { serial_no = counter++, };
                    DataCount = data.Count();
                    //var totalCount = query.Count();
                    #region Filtering

                    var sortedColumns = request.Columns.Where(s => s.Sort != null);
                    var orderByString = String.Empty;

                    foreach (var column in sortedColumns)
                    {
                        orderByString += orderByString != String.Empty ? "," : "";
                        orderByString += (column.Field) +
                        (column.Sort.Direction ==
                        SortDirection.Ascending ? " asc" : " desc");
                    }

                    data = data.OrderBy(orderByString ==
                    string.Empty ? "constituency_id desc" : orderByString);

                    List<FilteredQuestionsPool> filteredQuestionsPoolList = new List<FilteredQuestionsPool>();
                    int filteredDataCount = 0;
                    // Apply filters for searching
                    if (!string.IsNullOrEmpty(request.Search.Value))
                    {
                        var sch_string = request.Search.Value.ToLower();
                        var filteredData = data.Where(_item =>
                                                     _item.level.ToLower().Contains(sch_string)
                                                     || _item.subject.ToLower().Contains(sch_string)
                                                    || _item.difficulty.ToLower().Contains(sch_string)
                                                    || _item.importance.ToLower().Contains(sch_string)
                                                    || _item.question.ToLower().Contains(sch_string)
                                                    || _item.marks.ToString().Contains(sch_string)
                                                    );

                        if (filteredData.ToList().Count == 0)
                        {
                            var response = DataTablesResponse.Create(request, data.Count(), 0, filteredData);
                            var dtjson = new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
                            return dtjson;
                        }
                        else
                        {
                            //var dataPage = filteredData.Skip(request.Start).Take(request.Length).ToList();

                            var dataPage = new List<FilteredQuestionsPool>();
                            if (request.Length == -1)
                            {
                                dataPage = filteredData.ToList();
                                filteredDataCount = dataPage.Count();
                            }
                            else
                            {
                                dataPage = filteredData.Skip(request.Start).Take(request.Length).ToList();
                                filteredDataCount = dataPage.Count();
                            }
                            int i = 0;
                            dataPage.ForEach(p =>
                            {
                                i++;
                                p.serial_no = i;


                            });

                            filteredDataCount = dataPage.Count();
                            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);
                            var dtjson = new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
                            return dtjson;
                        }

                    }
                    else
                    {

                        var filteredData = data;
                        var dataPage = new List<FilteredQuestionsPool>();
                        if (request.Length == -1)
                        {
                            dataPage = filteredData.ToList();
                            filteredDataCount = dataPage.Count();
                        }
                        else
                        {
                            dataPage = filteredData.Skip(request.Start).Take(request.Length).ToList();
                            filteredDataCount = dataPage.Count();
                        }
                        int i = 0;
                        dataPage.ForEach(p =>
                        {
                            i++;
                            p.serial_no = i;
                            p.insertion_timestamp_string = Utility.DateFormat(p.insertion_timestamp, "dd-MM-yyyy hh:mm:ss tt");
                        });

                        var response = DataTablesResponse.Create(request, data.Count(), filteredDataCount, dataPage);
                        var dtjson = new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
                        return dtjson;
                        #endregion


                    }
                }
            }
            catch (Exception ex)
            {
                GlobalResult_Type = Constants.MESSAGE_TYPE.EXCEPTION;
                GlobalMessage = ex.Message.Equals(Constants.ERROR_TITLE.CUSTOM_ERROR) ? ex.InnerException.Message : ex.Message;
                GlobalStack_Trace = ex.StackTrace;
                var response = DataTablesResponse.Create(request, DataCount, 0, new List<FilteredQuestionsPool>());
                var dtjson = new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.TestManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), GlobalActivity_Type, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);

                return dtjson;
                //return View(Constants.CUSTOM_ERROR_URL, Logger.DefineCustomError(ex));
            }
            finally
            {
                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.TestManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), GlobalActivity_Type, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);
            }

        }
        [HttpPost]
        public ActionResult Create(TestVM testVM)
        {

            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Submitted");
            try
            {
                //VALIDATION HERE               
                Validation(testVM);
                if (ModelState.IsValid)
                {

                    using (Kts_dataEntities context = new Kts_dataEntities())
                    {
                        //check if question selected are as per config
                        if (CheckQuestionsWithConfig(testVM, context))
                        {
                            throw new Exception("validation");
                        }
                        if (context.User_Tests.Any(s => s.description == testVM.Tests.description))
                        {
                            ModelState.AddModelError("", "Test already exists with Specified Description.");
                            throw new Exception("validation");
                        }
                        #region TEST_ADD 
                        if (testVM.Tests.test_id == 0)
                        {
                            testVM.Tests.insertion_timestamp = Utility.GetServerDateTime(context);
                            testVM.Tests.user_Id = (long)LoggedUser.Id;
                            testVM.Tests.total_questions = testVM.selected_questions.Count;
                            testVM.Tests.total_marks = context.questions.Where(g => testVM.selected_questions.Contains(g.question_id.ToString())).Sum(s => s.marks);
                            //testVM.Tests.test_versions = 1;
                            testVM.Tests.test_creator = string.IsNullOrEmpty(testVM.Tests.test_creator) ? LoggedUser.FirstName + " " + LoggedUser.LastName : testVM.Tests.test_creator;

                            //TRIM SPACES AT START AND END
                            testVM.Tests.description = testVM.Tests.description != null ? testVM.Tests.description.Trim() : null;

                            if (testVM.selected_questions != null && testVM.selected_questions.Count > 0)
                            {
                                #region TEST_QUESTIONS

                                foreach (var question in testVM.selected_questions)
                                {
                                    Test_questions objTestQ = new Test_questions
                                    {
                                        User_Tests = testVM.Tests,
                                        question_id = long.Parse(question)
                                    };

                                    testVM.Tests.Test_questions.Add(objTestQ);

                                }
                                #endregion
                            }
                            if (testVM.subjectsCollection_SUBDIV != null && testVM.subjectsCollection_SUBDIV.Count > 0)
                            {
                                foreach (var subjct in testVM.subjectsCollection_SUBDIV)
                                {
                                    user_test_subjects objTestSubject = new user_test_subjects();
                                    objTestSubject.User_Tests = testVM.Tests;
                                    objTestSubject.subject_id = subjct.subject_id;
                                    objTestSubject.no_of_questions = subjct.SubjectNoOFMcqs;
                                    if (subjct.listDifficultyFilter != null && subjct.listDifficultyFilter.Count > 0)
                                    {
                                        foreach (var difficulty in subjct.listDifficultyFilter)
                                        {
                                            test_subjects_difficulty objSubjectDifficulty = new test_subjects_difficulty();
                                            objSubjectDifficulty.user_test_subjects = objTestSubject;
                                            objSubjectDifficulty.difficulty_code = difficulty.difficulty_code;
                                            objSubjectDifficulty.no_of_questions = difficulty.DifficultyNoOFMcqs;
                                            if (difficulty.listImportanceFilter != null && difficulty.listImportanceFilter.Count > 0)
                                            {
                                                foreach (var importnce in difficulty.listImportanceFilter)
                                                {
                                                    subject_difficulty_importance objSubjectDifficultyImportance = new subject_difficulty_importance
                                                    {
                                                        test_subjects_difficulty = objSubjectDifficulty,
                                                        importance_code = importnce.importance_code,
                                                        no_of_questions = importnce.ImportanceNoOFMcqs
                                                    };
                                                    objSubjectDifficulty.subject_difficulty_importance.Add(objSubjectDifficultyImportance);
                                                }
                                            }
                                            objTestSubject.test_subjects_difficulty.Add(objSubjectDifficulty);
                                        }
                                    }
                                    testVM.Tests.user_test_subjects.Add(objTestSubject);
                                }
                            }
                            if (testVM.Tests.status == "ready")
                            {
                                testVM = CreateRandomVersion(testVM, context);
                            }
                            else
                            {
                                testVM.Tests.status = "draft";
                            }
                            context.User_Tests.Add(testVM.Tests);


                        }
                        #endregion


                        else
                        {
                            throw new HttpException(500, "Invalid Create Test Request");
                        }


                        context.SaveChanges();
                        //return RedirectToAction(indexView);
                        TestIndexVM objTests = new TestIndexVM();
                        objTests.MessageTitle = GlobalResult_Type = "Success";
                        objTests.MessageDescription = GlobalMessage = "New Test: " + testVM.Tests.description + " successfully created.";

                        return View(indexView, objTests);
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
                    testVM = GetSelectedQuestionsList(testVM, context);
                }
                if (!ex.Message.Contains("validation"))
                {

                    testVM.MessageDescription = "An Error has Occurred while Inserting Test";
                }

                GlobalResult_Type = "exception";
                GlobalMessage = ex.Message.Equals(Constants.ERROR_TITLE.CUSTOM_ERROR) ? ex.InnerException.Message : ex.Message;
                GlobalStack_Trace = ex.StackTrace;

                return View(createView, testVM);
            }
            finally
            {
                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.TestManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Request Submitted", Constants.USER_ROLES.test_compiler, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);
            }
        }
        private TestVM CreateRandomVersion(TestVM argTestVM, Kts_dataEntities context)
        {
            int count = 0;
            List<TestQuestionVersion> listTestQuestionVersion = new List<TestQuestionVersion>();
            foreach (var testQuestion in argTestVM.Tests.Test_questions)
            {
                Test_versions testVersion = new Test_versions
                {
                    Test_questions = testQuestion,
                    User_Tests = argTestVM.Tests,
                    version_number = 1
                };
                context.Test_versions.Add(testVersion);
                count++;
                listTestQuestionVersion.Add(new TestQuestionVersion
                {
                    QuestionNo = count,
                    question_id = testQuestion.question_id
                });
            }

            if (argTestVM.Tests.test_versions > 1)
            {
                for (int i = 2; i <= argTestVM.Tests.test_versions; i++)
                {
                    List<TestQuestionVersion> listTestQuestionVersionRand = new List<TestQuestionVersion>();
                    Random rnd = new Random();
                    listTestQuestionVersion.ForEach(s =>
                    {
                        while (!listTestQuestionVersionRand.Any(g => g.question_id == s.question_id))
                        {
                            count = rnd.Next(1, listTestQuestionVersion.Count + 1);
                            if (!listTestQuestionVersionRand.Any(g => g.QuestionNo == count))
                            {
                                listTestQuestionVersionRand.Add(new TestQuestionVersion
                                {
                                    QuestionNo = count,
                                    question_id = s.question_id
                                });
                            }
                        }
                    });
                    listTestQuestionVersionRand.OrderByDescending(g => g.QuestionNo).ToList().ForEach(s =>
                    {
                        Test_versions testVersion = new Test_versions
                        {
                            Test_questions = argTestVM.Tests.Test_questions.FirstOrDefault(g => g.question_id == s.question_id),
                            User_Tests = argTestVM.Tests,
                            version_number = i
                        };
                        context.Test_versions.Add(testVersion);
                    });

                }
            }
            return argTestVM;
        }
        #region VALIDATIONS
        private bool Validation(TestVM argViewModel)
        {
            bool errors = false;

            if (string.IsNullOrEmpty(argViewModel.Tests.description))
            {
                ModelState.AddModelError("", "Test Detail is Required.");
                errors = true;
            }
            else
            {
                if (argViewModel.Tests.description.Length > 128)
                {
                    ModelState.AddModelError("", "Test Detail Length Exceeds available Limit of 128 characters.");
                    errors = true;
                }
                else if (argViewModel.Tests.description.Length < 3)
                {
                    ModelState.AddModelError("", "Test Detail Length must be atleast 3 characters.");
                    errors = true;
                }
                //if (!Regex.IsMatch(argViewModel.Tests.description, "^[a-zA-Z0-9._-]+"))
                //{
                //    ModelState.AddModelError("", "Only Alphanumeric Characters and Underscores Allowed in Test Detail");
                //    errors = true;
                //}
            }
            if (argViewModel.Tests.question_level_id == null || argViewModel.Tests.question_level_id == 0)
            {
                ModelState.AddModelError("", "Please select question level.");
                errors = true;
            }

            if (argViewModel.Tests.status == "ready")
            {
                if (argViewModel.Tests.total_time_minutes == 0)
                {
                    ModelState.AddModelError("", "Please enter time in minutes.");
                    errors = true;
                }

                //if (argViewModel.Tests.total_marks == 0)
                //{
                //    ModelState.AddModelError("", "Please enter marks.");
                //    errors = true;
                //}
                if (argViewModel.Tests.test_versions == 0)
                {
                    ModelState.AddModelError("", "Please enter versions of test to be created.");
                    errors = true;
                }

                if (argViewModel.selected_questions.Count() == 0)
                {
                    ModelState.AddModelError("", "Please select Questions.");
                    errors = true;
                }
                if (argViewModel.TotalTestQuestions != argViewModel.selected_questions.Count())
                {
                    ModelState.AddModelError("", "Total number of questions expected are not equal to the number of questions selected.");
                    errors = true;
                }
                if (argViewModel.subjectsCollection_SUBDIV == null || argViewModel.subjectsCollection_SUBDIV.Count == 0)
                {
                    ModelState.AddModelError("", "Please add subject of the Test.");
                    errors = true;
                }
            }
            //if (!string.IsNullOrEmpty(argViewModel.Tests.first_names))
            //{
            //    if (argViewModel.User.first_names.Length > 128)
            //    {
            //        ModelState.AddModelError("", "First name Length Exceeds available Limit of 128 characters.");
            //        errors = true;
            //    }
            //    if (!Regex.IsMatch(argViewModel.User.first_names, "^[a-zA-Z\\s]+"))
            //    {
            //        ModelState.AddModelError("", "Only Alphabets Allowed in First name");
            //        errors = true;
            //    }
            //}

            //if (string.IsNullOrEmpty(argViewModel.User.last_name))
            //{
            //    ModelState.AddModelError("", "Last Name is Required");
            //    errors = true;
            //}
            //if (!string.IsNullOrEmpty(argViewModel.User.last_name))
            //{
            //    if (argViewModel.User.last_name.Length > 128)
            //    {
            //        ModelState.AddModelError("", "Last name Length Exceeds available Limit of 64 characters.");
            //        errors = true;
            //    }
            //    if (!Regex.IsMatch(argViewModel.User.last_name, "^[a-zA-Z\\s]+"))
            //    {
            //        ModelState.AddModelError("", "Only Alphabets Allowed in Last name");
            //        errors = true;
            //    }
            //}



            //if (string.IsNullOrEmpty(argViewModel.User.status))
            //{
            //    ModelState.AddModelError("", "Please select status.");
            //    errors = true;
            //}
            //if (argViewModel.User.user_Id != 0)
            //{
            //    var error = ModelState.Values.Where(g => g.Errors.Count > 0).FirstOrDefault();
            //    if (error != null && error.Errors[0].ErrorMessage == "The Password field is required.")
            //    {
            //        if (!errors)
            //            ModelState.Clear();
            //    }
            //    else if (argViewModel.User.user_Id == 0)
            //    {
            //        if (!errors)
            //            ModelState.Clear();
            //    }
            //}

            return errors;
        }
        #endregion
        [HttpGet]
        public ActionResult Edit(string id)
        {
            TestVM _testVM = new TestVM();
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Page Called for id:" + id);
            try
            {

                if (string.IsNullOrEmpty(id))
                    throw new HttpException(500, "Invalid Edit Test Request");
                using (Kts_dataEntities context = new Kts_dataEntities())
                {
                    long test_id = long.Parse(id);
                    if (test_id == 0)
                        throw new HttpException(500, "Invalid Edit Test Request");

                    _testVM.Tests = context.User_Tests.Where(s => s.test_id == test_id).FirstOrDefault();
                    if (_testVM.Tests == null)
                        throw new HttpException(500, "Invalid Edit Test Request");
                    int count = 0;
                    var testQuestions = _testVM.Tests.Test_questions;
                    var testSubjects = _testVM.Tests.user_test_subjects;
                    if (testQuestions != null)
                    {
                        // _testVM.level_id = testQuestions.FirstOrDefault().question.question_level_id.ToString();
                        foreach (var test in testQuestions)
                        {
                            count++;
                            _testVM.listSelectedQuestions.Add(new SelectedQuestions
                            {
                                serial_no = count,
                                question_id = test.question_id.Value,
                                level = test.question.question_levels.description,
                                subject = test.question.subject.description,
                                difficulty = test.question.question_difficulty.description,
                                importance = test.question.question_importance.description,
                                question = test.question.description
                            });
                        }

                    }
                    if (testSubjects != null)
                    {
                        _testVM.TotalTestQuestions = testSubjects.Sum(s => s.no_of_questions);
                        foreach (var testSubject in testSubjects)
                        {
                            SubjectFilter objSubjectFilter = new SubjectFilter();
                            objSubjectFilter.user_test_subject_id = testSubject.user_test_subject_id;
                            objSubjectFilter.subject_id = testSubject.subject_id;
                            objSubjectFilter.SubjectNoOFMcqs = testSubject.no_of_questions;
                            foreach (var difficulty in testSubject.test_subjects_difficulty)
                            {
                                DifficultyFilter objDifficultyFilter = new DifficultyFilter();
                                objDifficultyFilter.test_subject_difficulty_id = difficulty.test_subject_difficulty_id;
                                objDifficultyFilter.difficulty_code = difficulty.difficulty_code;
                                objDifficultyFilter.DifficultyNoOFMcqs = difficulty.no_of_questions;
                                foreach (var importance in difficulty.subject_difficulty_importance)
                                {
                                    ImportanceFilter objImportanceFilter = new ImportanceFilter();
                                    objImportanceFilter.subject_difficulty_importance_id = importance.difficulty_importance_id;
                                    objImportanceFilter.importance_code = importance.importance_code;
                                    objImportanceFilter.ImportanceNoOFMcqs = importance.no_of_questions;
                                    objDifficultyFilter.listImportanceFilter.Add(objImportanceFilter);
                                }
                                objSubjectFilter.listDifficultyFilter.Add(objDifficultyFilter);
                            }
                            _testVM.subjectsCollection_SUBDIV.Add(objSubjectFilter);
                        }

                    }

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

                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.TestManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Page Called", Constants.USER_ROLES.test_compiler, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);

            }

            return View(editView, _testVM);
        }
        private bool CheckQuestionsWithConfig(TestVM testVM, Kts_dataEntities context)
        {
            bool errors = false;
            var questionsSelected = context.questions.Where(g => testVM.selected_questions.Contains(g.question_id.ToString()));
            string errorMsg = string.Empty;

            if (testVM.subjectsCollection_SUBDIV != null && testVM.subjectsCollection_SUBDIV.Count > 0)
            {
                foreach (var subjct in testVM.subjectsCollection_SUBDIV)
                {
                    var questionsForSubject = questionsSelected.Where(g => g.subject_id == subjct.subject_id);
                    //if(questionsForSubject.Count()!=subjct.SubjectNoOFMcqs)
                    //{
                    //    errorMsg = errorMsg + "The no of questions for subject "+subjct.su;
                    //}
                    if (subjct.listDifficultyFilter != null && subjct.listDifficultyFilter.Count > 0)
                    {
                        foreach (var difficulty in subjct.listDifficultyFilter)
                        {
                            var questionsForSubjectDifficulty = questionsForSubject.Where(g => g.difficulty_code == difficulty.difficulty_code);
                            if (difficulty.listImportanceFilter != null && difficulty.listImportanceFilter.Count > 0)
                            {
                                foreach (var importnce in difficulty.listImportanceFilter)
                                {
                                    var questionsForSubjectDifficultyImp = questionsForSubjectDifficulty.Where(g => g.importance_code == importnce.importance_code);
                                    if (questionsForSubjectDifficultyImp == null || questionsForSubjectDifficultyImp.Count() == 0)
                                    {
                                        if (importnce.ImportanceNoOFMcqs == 0)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            var subjects = context.subjects.FirstOrDefault(g => g.subject_id == subjct.subject_id);
                                            var difficulties = context.question_difficulty.FirstOrDefault(g => g.difficulty_code == difficulty.difficulty_code);
                                            var importances = context.question_importance.FirstOrDefault(g => g.importance_code == importnce.importance_code);
                                            string subj = subjects.description;
                                            string diff = difficulties.difficulty_code;
                                            string imp = importances.importance_code;
                                            errorMsg = errorMsg + "The num of questions selected for importance type: " + imp + ", of difficulty type: " + diff + " of subject:" + subj + " or not as per selected configuration. \n";
                                        }

                                    }
                                    else if (questionsForSubjectDifficultyImp.Count() != importnce.ImportanceNoOFMcqs)
                                    {

                                        string subj = questionsForSubjectDifficultyImp.FirstOrDefault().subject.description;
                                        string diff = questionsForSubjectDifficultyImp.FirstOrDefault().difficulty_code;
                                        string imp = questionsForSubjectDifficultyImp.FirstOrDefault().importance_code;

                                        errorMsg = errorMsg + "The num of questions selected for importance type: " + imp + ", of difficulty type: " + diff + " of subject:" + subj + " or not as per selected configuration. \n";
                                    }
                                }
                            }

                        }
                    }



                }

            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
                ModelState.AddModelError("", "Questions selected are not according to selected subject-difficulty-importance configuration.");
                errors = true;
            }
            return errors;
        }
        [HttpPost]
        public ActionResult Update(TestVM testVM)
        {
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Submitted for Test: " + testVM.Tests.description);
            //TRIM SPACES AT START AND END
            testVM.Tests.description = testVM.Tests.description != null ? testVM.Tests.description.Trim() : null;
            // ####################### SERVER SIDE VALIDATIONS HERE ###########################
            //---------------------------------------------------------------------------------   

            Validation(testVM);

            try
            {
                if (ModelState.IsValid)
                {
                    using (Kts_dataEntities context = new Kts_dataEntities())
                    {
                        //check if question selected are as per config
                        if (CheckQuestionsWithConfig(testVM, context))
                        {
                            throw new Exception("validation");
                        }


                        var dbObj = context.User_Tests.Where(x => x.test_id == testVM.Tests.test_id).FirstOrDefault();

                        testVM.Tests.user_Id = dbObj.user_Id;
                        testVM.Tests.insertion_timestamp = dbObj.insertion_timestamp;
                        testVM.Tests.total_questions = testVM.selected_questions.Count;
                        testVM.Tests.total_marks = context.questions.Where(g => testVM.selected_questions.Contains(g.question_id.ToString())).Sum(s => s.marks);
                        testVM.Tests.test_creator = string.IsNullOrEmpty(testVM.Tests.test_creator) ? LoggedUser.FirstName + " " + LoggedUser.LastName : testVM.Tests.test_creator;

                        //DateTime? InsertionTimeStamp = Utility.GetServerDateTime(context);
                        if (testVM.Tests.test_id != 0)
                        {
                            #region TEST_MODIFY

                            //OBJECT ASSIGNMENT
                            ObjectExt.CopyFrom(dbObj, testVM.Tests);

                            #endregion

                            #region TestQuestionsMap
                            if (testVM.selected_questions != null && testVM.selected_questions.Count != 0)
                            {
                                //DateTime INSERTION_TIMESTAMP = Utility.GetServerDateTime(context);
                                foreach (var objQuestn in testVM.selected_questions)
                                {
                                    long questnID = long.Parse(objQuestn);
                                    Test_questions test_Question = dbObj.Test_questions.FirstOrDefault(g => g.question_id == questnID);
                                    if (test_Question == null)
                                    {
                                        test_Question = new Test_questions
                                        {
                                            User_Tests = dbObj,
                                            question_id = questnID
                                        };
                                        dbObj.Test_questions.Add(test_Question);
                                    }
                                }
                                var testQuestionsToArchive = dbObj.Test_questions.Where(s => !testVM.selected_questions.Any(g => long.Parse(g) == s.question_id)).ToList();

                                foreach (var itemQuestns in testQuestionsToArchive)
                                {
                                    context.Entry(itemQuestns).State = EntityState.Deleted;
                                }

                            }
                            else
                            {
                                var testQuestionsToArchive = dbObj.Test_questions.ToList();

                                foreach (var itemQuestns in testQuestionsToArchive)
                                {
                                    context.Entry(itemQuestns).State = EntityState.Deleted;
                                }
                            }
                            #endregion

                            #region TestConfiguration
                            if (testVM.subjectsCollection_SUBDIV != null && testVM.subjectsCollection_SUBDIV.Count > 0)
                            {
                                foreach (var subjct in testVM.subjectsCollection_SUBDIV)
                                {
                                    if (subjct.user_test_subject_id == null || subjct.user_test_subject_id == 0)
                                    {
                                        user_test_subjects objTestSubject = new user_test_subjects();
                                        objTestSubject.User_Tests = testVM.Tests;
                                        objTestSubject.subject_id = subjct.subject_id;
                                        objTestSubject.no_of_questions = subjct.SubjectNoOFMcqs;
                                        if (subjct.listDifficultyFilter != null && subjct.listDifficultyFilter.Count > 0)
                                        {
                                            foreach (var difficulty in subjct.listDifficultyFilter)
                                            {
                                                test_subjects_difficulty objSubjectDifficulty = new test_subjects_difficulty();
                                                objSubjectDifficulty.user_test_subjects = objTestSubject;
                                                objSubjectDifficulty.difficulty_code = difficulty.difficulty_code;
                                                objSubjectDifficulty.no_of_questions = difficulty.DifficultyNoOFMcqs;
                                                if (difficulty.listImportanceFilter != null && difficulty.listImportanceFilter.Count > 0)
                                                {
                                                    foreach (var importnce in difficulty.listImportanceFilter)
                                                    {
                                                        subject_difficulty_importance objSubjectDifficultyImportance = new subject_difficulty_importance
                                                        {
                                                            test_subjects_difficulty = objSubjectDifficulty,
                                                            importance_code = importnce.importance_code,
                                                            no_of_questions = importnce.ImportanceNoOFMcqs
                                                        };
                                                        objSubjectDifficulty.subject_difficulty_importance.Add(objSubjectDifficultyImportance);
                                                    }
                                                }
                                                objTestSubject.test_subjects_difficulty.Add(objSubjectDifficulty);
                                            }
                                        }
                                        dbObj.user_test_subjects.Add(objTestSubject);
                                    }
                                    else
                                    {
                                        user_test_subjects objTestSubject = context.user_test_subjects.FirstOrDefault(g => g.user_test_subject_id == subjct.user_test_subject_id);
                                        if (objTestSubject != null)
                                        {
                                            objTestSubject.subject_id = subjct.subject_id;
                                            objTestSubject.no_of_questions = subjct.SubjectNoOFMcqs;
                                            foreach (var difficulty in subjct.listDifficultyFilter)
                                            {
                                                if (difficulty.test_subject_difficulty_id == null && difficulty.test_subject_difficulty_id == 0)
                                                {
                                                    test_subjects_difficulty objSubjectDifficulty = new test_subjects_difficulty();
                                                    objSubjectDifficulty.user_test_subjects = objTestSubject;
                                                    objSubjectDifficulty.difficulty_code = difficulty.difficulty_code;
                                                    objSubjectDifficulty.no_of_questions = difficulty.DifficultyNoOFMcqs;
                                                    if (difficulty.listImportanceFilter != null && difficulty.listImportanceFilter.Count > 0)
                                                    {
                                                        foreach (var importnce in difficulty.listImportanceFilter)
                                                        {
                                                            subject_difficulty_importance objSubjectDifficultyImportance = new subject_difficulty_importance
                                                            {
                                                                test_subjects_difficulty = objSubjectDifficulty,
                                                                importance_code = importnce.importance_code,
                                                                no_of_questions = importnce.ImportanceNoOFMcqs
                                                            };
                                                            objSubjectDifficulty.subject_difficulty_importance.Add(objSubjectDifficultyImportance);
                                                        }
                                                    }
                                                    objTestSubject.test_subjects_difficulty.Add(objSubjectDifficulty);
                                                }
                                                else
                                                {
                                                    test_subjects_difficulty objSubjectDifficulty = context.test_subjects_difficulty.FirstOrDefault(g => g.test_subject_difficulty_id == difficulty.test_subject_difficulty_id);
                                                    if (objSubjectDifficulty != null)
                                                    {
                                                        objSubjectDifficulty.difficulty_code = difficulty.difficulty_code;
                                                        objSubjectDifficulty.no_of_questions = difficulty.DifficultyNoOFMcqs;
                                                        foreach (var importance in difficulty.listImportanceFilter)
                                                        {
                                                            if (importance.subject_difficulty_importance_id == 0 || importance.subject_difficulty_importance_id == null)
                                                            {
                                                                subject_difficulty_importance objSubjectDifficultyImportance = new subject_difficulty_importance
                                                                {
                                                                    test_subjects_difficulty = objSubjectDifficulty,
                                                                    importance_code = importance.importance_code,
                                                                    no_of_questions = importance.ImportanceNoOFMcqs
                                                                };
                                                                objSubjectDifficulty.subject_difficulty_importance.Add(objSubjectDifficultyImportance);
                                                            }
                                                            else
                                                            {
                                                                subject_difficulty_importance objSubjectDifficultyImportance = context.subject_difficulty_importance.FirstOrDefault(g => g.difficulty_importance_id == importance.subject_difficulty_importance_id);
                                                                if (objSubjectDifficultyImportance != null)
                                                                {
                                                                    objSubjectDifficultyImportance.importance_code = importance.importance_code;
                                                                    objSubjectDifficultyImportance.no_of_questions = importance.ImportanceNoOFMcqs;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                                var testSubjectsToArchive = dbObj.user_test_subjects.Where(s => !testVM.subjectsCollection_SUBDIV.Any(g => g.user_test_subject_id == s.user_test_subject_id)).ToList();

                                foreach (var itemSubjects in testSubjectsToArchive)
                                {
                                    foreach (var difficulty in itemSubjects.test_subjects_difficulty.ToList())
                                    {
                                        foreach (var importance in difficulty.subject_difficulty_importance.ToList())
                                        {
                                            context.Entry(importance).State = EntityState.Deleted;
                                            //context.subject_difficulty_importance.Remove(importance);

                                        }
                                        context.Entry(difficulty).State = EntityState.Deleted;
                                        //context.test_subjects_difficulty.Remove(difficulty);

                                    }
                                    context.Entry(itemSubjects).State = EntityState.Deleted;
                                    //context.user_test_subjects.Remove(itemSubjects);
                                }
                            }
                            else
                            {
                                var testSubjectsToArchive = dbObj.user_test_subjects.ToList();
                                //context.user_test_subjects.RemoveRange(testSubjectsToArchive);
                                foreach (var itemSubjects in testSubjectsToArchive)
                                {
                                    foreach (var difficulty in itemSubjects.test_subjects_difficulty.ToList())
                                    {
                                        foreach (var importance in difficulty.subject_difficulty_importance.ToList())
                                        {
                                            context.Entry(importance).State = EntityState.Deleted;
                                            //context.subject_difficulty_importance.Remove(importance);

                                        }
                                        context.Entry(difficulty).State = EntityState.Deleted;
                                        //context.test_subjects_difficulty.Remove(difficulty);

                                    }
                                    context.Entry(itemSubjects).State = EntityState.Deleted;
                                    //context.user_test_subjects.Remove(itemSubjects);
                                }
                            }
                            #endregion

                            if (testVM.Tests.status == "ready")
                            {
                                testVM = CreateRandomVersion(testVM, context);
                            }
                            else
                            {
                                testVM.Tests.status = "draft";
                            }

                        }
                        context.SaveChanges();
                        TestIndexVM objTests = new TestIndexVM();
                        objTests.MessageTitle = GlobalResult_Type = "Success";
                        objTests.MessageDescription = GlobalMessage = "Test: " + dbObj.description + " successfully updated.";
                        return View(indexView, objTests);
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
                    testVM = GetSelectedQuestionsList(testVM, context);
                }
                GlobalResult_Type = "exception";
                GlobalMessage = ex.Message.Equals(Constants.ERROR_TITLE.CUSTOM_ERROR) ? ex.InnerException.Message : ex.Message;
                GlobalStack_Trace = ex.StackTrace;

                return View(editView, testVM);
            }
            finally
            {
                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.TestManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Submitted for test: " + testVM.Tests.description, Constants.USER_ROLES.test_compiler, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);
            }
        }
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewSubjectDetail()
        {
            SubjectFilter objSubjectFilter = new SubjectFilter();
            List<ImportanceFilter> _listImportanceFilter = new List<ImportanceFilter>();
            foreach (SelectListItem item in Lookup.GetImportances())
            {
                _listImportanceFilter.Add(new ImportanceFilter
                {
                    importance_code = item.Value,
                    ImportanceNoOFMcqs = 0
                });
            }
            foreach (SelectListItem item in Lookup.GetDifficulties())
            {
                objSubjectFilter.listDifficultyFilter.Add(new DifficultyFilter
                {
                    difficulty_code = item.Value,
                    DifficultyNoOFMcqs = 0,
                    listImportanceFilter = _listImportanceFilter
                });
            }
            return View("AddSubjectDetail", new List<SubjectFilter>() { objSubjectFilter });
        }
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewDifficultyDetail(string parentprefix)
        {
            ViewData["ContainerPrefix"] = parentprefix;
            DifficultyFilter objDifficultyFilter = new DifficultyFilter();
            foreach (SelectListItem item in Lookup.GetImportances())
            {
                objDifficultyFilter.listImportanceFilter.Add(new ImportanceFilter
                {
                    importance_code = item.Value,
                    ImportanceNoOFMcqs = 0
                });
            }
            return PartialView("AddDifficultyDetail", new List<DifficultyFilter>() { objDifficultyFilter });
        }
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewImportanceDetail(string parentprefix)
        {
            ViewData["ContainerPrefix"] = parentprefix;
            return PartialView("AddImportanceDetail", new List<ImportanceFilter>() { new ImportanceFilter() });
        }
        // GET: Test Versions
        [HttpGet]
        public ActionResult IndexVersions(string id)
        {
            TestVM _testVM = new TestVM();
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Page Called for id:" + id);
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new HttpException(500, "Invalid Test Versions Request");
                using (Kts_dataEntities context = new Kts_dataEntities())
                {
                    long test_id = long.Parse(id);
                    if (test_id == 0)
                        throw new HttpException(500, "Invalid Test Versions Request");

                    _testVM.Tests = context.User_Tests.Where(s => s.test_id == test_id).FirstOrDefault();
                    if (_testVM.Tests == null)
                        throw new HttpException(500, "Invalid Test Versions Request");
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

                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.TestManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Page Called", Constants.USER_ROLES.test_compiler, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);

            }

            return View(indexVersionsView, _testVM);
        }
        [HttpGet]
        public ActionResult ViewTestVersion(string id, int version)
        {
            TestVersionsIndexVM objTestVersionIndexVM = new TestVersionsIndexVM();
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Page Called for id:" + id);
            try
            {
                if (string.IsNullOrEmpty(id) || version == 0)
                    throw new HttpException(500, "Invalid View Version Request");
                using (Kts_dataEntities context = new Kts_dataEntities())
                {
                    long test_id = long.Parse(id);
                    if (test_id == 0)
                        throw new HttpException(500, "Invalid View Version Request");

                    User_Tests dbTest = context.User_Tests.Where(s => s.test_id == test_id).FirstOrDefault();
                    if (dbTest == null)
                        throw new HttpException(500, "Invalid View Version Request");

                    objTestVersionIndexVM.test_id = dbTest.test_id;
                    objTestVersionIndexVM.test_description = dbTest.description;
                    objTestVersionIndexVM.test_versions = dbTest.test_versions;
                    objTestVersionIndexVM.total_questions = dbTest.total_questions;
                    objTestVersionIndexVM.version_number = version;

                    var versionList = dbTest.Test_versions1.Where(v => v.version_number == objTestVersionIndexVM.version_number).OrderBy(s => s.test_version_id).ToList();
                    int count = 0;
                    versionList.ForEach(g =>
                    {
                        count++;
                        objTestVersionIndexVM.listFilteredTestVersions.Add(new FilteredTestVersions
                        {
                            serial_no = count,
                            question = g.Test_questions != null && g.Test_questions.question != null ? g.Test_questions.question.description : ""
                        });
                    });

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

                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.TestManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Page Called", Constants.USER_ROLES.test_compiler, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);

            }

            return View(viewVersionView, objTestVersionIndexVM);
        }
        [HttpGet]
        public ActionResult ViewTestVersionReport(string id, int version)
        {
            try
            {
                long test_id = long.Parse(id);
                if (test_id == 0)
                    throw new HttpException(500, "Invalid View Version Request");

                byte[] report = ReportEngine.GetPaperReport(test_id, version, ReportExtension.PDF);
                if (report != null)
                    return File(report, "application/pdf");
                else
                    return null;
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
                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.TestManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), Constants.USER_ROLES.test_compiler, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);
            }

        }
        [HttpGet]
        public ActionResult ViewTestAllVersionReport(string id)
        {
            try
            {
                long test_id = long.Parse(id);
                if (test_id == 0)
                    throw new HttpException(500, "Invalid View Version Request");

                int test_versions = 0;
                using (Kts_dataEntities context = new Kts_dataEntities())
                {

                    if (test_id == 0)
                        throw new HttpException(500, "Invalid Test Versions Request");

                    User_Tests Tests = context.User_Tests.Where(s => s.test_id == test_id).FirstOrDefault();
                    if (Tests == null)
                        throw new HttpException(500, "Invalid Test Versions Request");
                    else
                    {
                        test_versions = Tests.test_versions;
                    }

                }
                IList<byte[]> pdfs = new List<byte[]>();
                for (int i = 1; i <= test_versions; i++)
                {
                    byte[] report = ReportEngine.GetPaperReport(test_id, i, ReportExtension.PDF);
                    pdfs.Add(report);
                }
                if (pdfs != null && pdfs.Count > 0)
                {
                    byte[] reportAll = ReportEngine.ConcatPdfs(pdfs);
                    if (reportAll != null)
                    {
                        return File(reportAll, "application/pdf");
                    }
                    else
                        return null;
                }
                else
                    return null;
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
                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.TestManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), Constants.USER_ROLES.test_compiler, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);
            }

        }
        [HttpGet]
        public ActionResult ViewTestVersionCorrectAnsReport(string id, int version)
        {
            try
            {
                long test_id = long.Parse(id);
                if (test_id == 0)
                    throw new HttpException(500, "Invalid View Version Request");

                byte[] report = ReportEngine.GetPaperWithAnswersReport(test_id, version, ReportExtension.PDF);
                if (report != null)
                    return File(report, "application/pdf");
                else
                    return null;
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
                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.TestManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), Constants.USER_ROLES.test_compiler, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);
            }

        }
        [HttpGet]
        public ActionResult ViewTestAllVersionCorrectAnsReport(string id)
        {
            try
            {
                long test_id = long.Parse(id);
                if (test_id == 0)
                    throw new HttpException(500, "Invalid View Version Request");

                int test_versions = 0;
                using (Kts_dataEntities context = new Kts_dataEntities())
                {

                    if (test_id == 0)
                        throw new HttpException(500, "Invalid Test Versions Request");

                    User_Tests Tests = context.User_Tests.Where(s => s.test_id == test_id).FirstOrDefault();
                    if (Tests == null)
                        throw new HttpException(500, "Invalid Test Versions Request");
                    else
                    {
                        test_versions = Tests.test_versions;
                    }

                }
                IList<byte[]> pdfs = new List<byte[]>();
                for (int i = 1; i <= test_versions; i++)
                {
                    byte[] report = ReportEngine.GetPaperWithAnswersReport(test_id, i, ReportExtension.PDF);
                    pdfs.Add(report);
                }
                if (pdfs != null && pdfs.Count > 0)
                {
                    byte[] reportAll = ReportEngine.ConcatPdfs(pdfs);
                    if (reportAll != null)
                    {
                        return File(reportAll, "application/pdf");
                    }
                    else
                        return null;
                }
                else
                    return null;
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
                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.TestManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), Constants.USER_ROLES.test_compiler, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);
            }

        }

    }

}