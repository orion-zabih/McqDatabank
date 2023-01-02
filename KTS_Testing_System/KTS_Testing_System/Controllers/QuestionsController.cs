using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using KTS_Entity;
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
    [AccessDeniedAuthorizeAttribute(Roles = Constants.USER_ROLES.data_entry)]
    public class QuestionsController : BaseController
    {
        string GlobalResult_Type = "success";
        string GlobalActivity_Type = "";
        string GlobalRequest_Type = "";
        string GlobalMessage = "";
        string GlobalStack_Trace = "";
        readonly string indexView = "~/Views/Questions/Index.cshtml";
        readonly string createView = "~/Views/Questions/Create.cshtml";
        readonly string editView = "~/Views/Questions/Edit.cshtml";
        public void SetAuditTrailVariables(string argActivityType, string argRequestType)
        {
            //SETTING VALUES FOR AUDIT TRAIL
            GlobalActivity_Type = argActivityType;
            GlobalRequest_Type = argRequestType;
            GlobalMessage = "";
            GlobalStack_Trace = "";
            GlobalResult_Type = "success";
        }
        // GET: Questions
        public ActionResult Index(int? Page)
        {
            using (Kts_dataEntities context = new Kts_dataEntities())
            {
                QuestionsIndexVM objQuestions = new QuestionsIndexVM();
                return View(indexView, objQuestions);
            }

        }
        private string TruncateLongString(string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            return str.Substring(0, Math.Min(str.Length, maxLength));
        }
        [HttpPost]
        public ActionResult GetIndex(IDataTablesRequest request, string id)
        {
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod);
            try
            {
                using (Kts_dataEntities context = new Kts_dataEntities())
                {

                    IQueryable<question> query = context.questions.Where(g=>g.user_Id==LoggedUser.Id);

                    var data = query.Select(g => new
                    {
                        question_id = g.question_id,
                        description = g.description,
                        subject_name = g.subject!=null?g.subject.description:"",
                        difficulty_code = g.difficulty_code,
                        importance_code = g.importance_code,
                        marks = g.marks,
                        user=g.User!=null? g.User.first_names+" "+ g.User.last_name:"",
                        question_level = g.question_levels != null ? g.question_levels.description : "",
                        insertion_timestamp=g.insertion_timestamp

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
                    List<FilteredQuestions> filteredQuestionsList = new List<FilteredQuestions>();
                    int filteredDataCount = 0;
                    // Apply filters for searching
                    if (!string.IsNullOrEmpty(request.Search.Value))
                    {
                        var sch_string = request.Search.Value.ToLower();


                        var filteredData = data.Where(_item =>
                                                     _item.description.ToLower().Contains(sch_string)
                                                    || _item.user.ToLower().Contains(sch_string)
                                                    || (!string.IsNullOrEmpty(_item.subject_name) ?_item.subject_name:"").ToLower().Contains(sch_string)
                                                    || (!string.IsNullOrEmpty(_item.difficulty_code) ? _item.difficulty_code : "").ToLower().Contains(sch_string)
                                                    || (!string.IsNullOrEmpty(_item.importance_code) ? _item.importance_code : "").ToLower().Contains(sch_string)
                                                    || _item.marks.ToString().Contains(sch_string)
                                                    || (!string.IsNullOrEmpty(_item.question_level) ? _item.question_level : "").ToLower().Contains(sch_string)
                                                    //|| (_item.insertion_timestamp!=null ? Utility.DateFormat(_item.insertion_timestamp, "dd-MM-yyyy hh:mm:ss tt") : "").ToLower().Contains(sch_string)
                                                    );
                        var dataPage = filteredData.Skip(request.Start).Take(request.Length).ToList();




                        filteredDataCount = filteredData.Count();
                        int count = 0;
                        dataPage.ForEach(g =>
                        {
                            count++;
                            filteredQuestionsList.Add(new FilteredQuestions
                            {
                                serial_no = count,
                                question_id = g.question_id,
                                description = TruncateLongString(g.description,50),
                                subject_name = g.subject_name,
                                difficulty_code = g.difficulty_code,
                                importance_code = g.importance_code,
                                marks = g.marks,
                                question_level = g.question_level,
                                user=Utility.ToTitlecase(g.user),
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
                        var dataPage = filteredData.Skip(request.Start).Take(request.Length).ToList();
                        int count = 0;
                        dataPage.ForEach(g =>
                        {
                            count++;
                            filteredQuestionsList.Add(new FilteredQuestions
                            {
                                serial_no = count,
                                question_id = g.question_id,
                                description = TruncateLongString(g.description, 50),
                                subject_name = g.subject_name,
                                difficulty_code = g.difficulty_code,
                                importance_code = g.importance_code,
                                marks = g.marks,
                                question_level = g.question_level,
                                user = Utility.ToTitlecase(g.user),
                                insertion_timestamp = Utility.DateFormat(g.insertion_timestamp, "dd-MM-yyyy hh:mm:ss tt")
                            });
                        });



                        //var response = DataTablesResponse.Create(request, data.Count(), filteredDataCount, filteredData);
                        //var dtjson = new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
                        //return dtjson;

                        #endregion
                    }
                    var response = DataTablesResponse.Create(request, data.Count(), filteredDataCount, filteredQuestionsList);
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

                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.QuestionManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), Constants.USER_ROLES.admin, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);

            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Page Called");
            QuestionsVM _questionsVM = new QuestionsVM();
            try
            {

                using (Kts_dataEntities context = new Kts_dataEntities())
                // Kts_dataEntities context = new Kts_dataEntities();
                {
                    _questionsVM.Question = new question();
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

                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.QuestionManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Page Called", Constants.USER_ROLES.data_entry, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);

            }
            return View(createView, _questionsVM);
        }
        public ActionResult AddAnswersForQuestion()
        {
            return View("AddAnswersForQuestion", new List<AnswersForQuestion>() { new AnswersForQuestion() });
        }
        [HttpPost]
        public ActionResult Create(QuestionsVM questionsVM)
        {

            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Submitted");
            try
            {
                //VALIDATION HERE               
                Validation(questionsVM);
                if (ModelState.IsValid)
                {
                    using (Kts_dataEntities context = new Kts_dataEntities())
                    {
                        //
                        if (context.questions.Any(s => s.description == questionsVM.Question.description))
                        {
                            ModelState.AddModelError("", "A question already exists with same text.");
                            throw new Exception("validation");
                        }
                        #region QUESTION_ADD 
                        if (questionsVM.Question.question_id== 0)
                        {
                            questionsVM.Question.insertion_timestamp = Utility.GetServerDateTime(context);
                            questionsVM.Question.user_Id = (long)LoggedUser.Id;
                            //TRIM SPACES AT START AND END
                            questionsVM.Question.description = questionsVM.Question.description != null ? questionsVM.Question.description.Trim() : null;
                            if (questionsVM.AnswersForQuestion_DIV != null && questionsVM.AnswersForQuestion_DIV.Count > 0)
                            {
                                #region USER_ROLES

                                foreach (var objAnswer in questionsVM.AnswersForQuestion_DIV)
                                {
                                   
                                    questionsVM.Question.answers.Add(new answer {
                                        question= questionsVM.Question,
                                        description=objAnswer.description,
                                        correct_p=(string.IsNullOrEmpty(objAnswer.correct_p)?"false":objAnswer.correct_p).ToLower()=="true"?true:false
                                    });

                                }
                                #endregion
                            }
                            context.questions.Add(questionsVM.Question);
                        }
                        #endregion


                        else
                        {
                            throw new HttpException(500, "Invalid Create Question Request");
                        }


                        context.SaveChanges();
                        //return RedirectToAction(indexView);
                        QuestionsIndexVM objQuestionss = new QuestionsIndexVM();
                        objQuestionss.MessageTitle = GlobalResult_Type = "Success";
                        objQuestionss.MessageDescription = GlobalMessage = "New question: " + questionsVM.Question.description + " successfully created.";
                        
                        return View(indexView, objQuestionss);
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

                    questionsVM.MessageDescription = "An Error has Occurred while Inserting Question";
                }
                GlobalResult_Type = "exception";
                GlobalMessage = ex.Message.Equals(Constants.ERROR_TITLE.CUSTOM_ERROR) ? ex.InnerException.Message : ex.Message;
                GlobalStack_Trace = ex.StackTrace;

                return View(createView, questionsVM);
            }
            finally
            {
                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.QuestionManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Request Submitted", Constants.USER_ROLES.data_entry, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);
            }
        }
        #region VALIDATIONS
        private bool Validation(QuestionsVM argViewModel)
        {
            bool errors = false;

            if (string.IsNullOrEmpty(argViewModel.Question.description))
            {
                ModelState.AddModelError("", "Question Detail is Required.");
                errors = true;
            }
            else
            {
                if (argViewModel.Question.description.Length > 256)
                {
                    ModelState.AddModelError("", "Question Detail Length Exceeds available Limit of 256 characters.");
                    errors = true;
                }
                else if (argViewModel.Question.description.Length < 3)
                {
                    ModelState.AddModelError("", "Question Detail Length must be atleast 3 characters.");
                    errors = true;
                }
                if (!Regex.IsMatch(argViewModel.Question.description, "^[a-zA-Z0-9._-]+"))
                {
                    ModelState.AddModelError("", "Only Alphanumeric Characters and Underscores Allowed in Question Detail");
                    errors = true;
                }
            }
            //if (argViewModel.User.user_Id == 0)
            //{
            //    if (string.IsNullOrEmpty(argViewModel.User.password))
            //    {
            //        ModelState.AddModelError("", "Password is Required.");
            //        errors = true;
            //    }
            //    else if (!Regex.IsMatch(argViewModel.User.password, @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$"))
            //    {
            //        ModelState.AddModelError("", "Invalid Password\n   PASSWORD POLICY \n" +
            //"Passwords will contain at least: \n" +
            //"1 upper case letter \n" +
            //"1 lower case letter \n" +
            //"1 number or special character \n" +
            //"8 characters in length \n" +
            //"Password maximum length should not be arbitrarily limited");
            //        errors = true;
            //    }
            //}


            if (argViewModel.Question.question_level_id==null || argViewModel.Question.question_level_id==0)
            {                
                ModelState.AddModelError("", "Please select Level");
                errors = true;                
            }
            if (argViewModel.Question.subject_id == 0)
            {
                ModelState.AddModelError("", "Please select Level");
                errors = true;
            }
            if (string.IsNullOrEmpty(argViewModel.Question.difficulty_code))
            {
                ModelState.AddModelError("", "Please select Difficulty");
                errors = true;
            }

            if (string.IsNullOrEmpty(argViewModel.Question.importance_code))
            {
                ModelState.AddModelError("", "Please select Importance");
                errors = true;
            }
            if (argViewModel.Question.marks==0)
            {
                ModelState.AddModelError("", "Please enter marks");
                errors = true;
            }
            if (argViewModel.AnswersForQuestion_DIV==null || argViewModel.AnswersForQuestion_DIV.Count<2)
            {
                ModelState.AddModelError("", "Please enter atleast 2 options for the question.");
                errors = true;
            }
            else if (argViewModel.AnswersForQuestion_DIV.Count > 6)
            {
                ModelState.AddModelError("", "Maximum six options can be added to one question.");
                errors = true;
            }
            else
            {
                if(argViewModel.AnswersForQuestion_DIV.FirstOrDefault(s=>s.correct_p=="true")==null)
                {
                    ModelState.AddModelError("", "Please select one of the options as correct.");
                    errors = true;
                }
                List<string> answerList = new List<string>();
                int count = 0;
                argViewModel.AnswersForQuestion_DIV.ForEach(a => {
                    count++;
                    if (string.IsNullOrWhiteSpace(a.description))
                    {
                        ModelState.AddModelError("", "Option No."+count+" is empty.");
                        errors = true;
                    }
                    else if(answerList.Contains(a.description))
                    {
                        ModelState.AddModelError("", "Option No." + count + " already exists in the options list for the question.");
                        errors = true;
                    }
                    else if (a.description.Length>256)
                    {
                        ModelState.AddModelError("", "Option No." + count + " Length Exceeds available Limit of 256 characters.");
                        errors = true;
                    }
                    answerList.Add(a.description);


                });
            }


            

            return errors;
        }
        #endregion
        [HttpGet]
        public ActionResult Edit(string id)
        {
            QuestionsVM questionsVM = new QuestionsVM();
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Page Called for id:" + id);
            try
            {

                if (string.IsNullOrEmpty(id))
                    throw new HttpException(500, "Invalid Edit Question Request");
                using (Kts_dataEntities context = new Kts_dataEntities())
                {
                    long question_id = long.Parse(id);
                    if (question_id == 0)
                        throw new HttpException(500, "Invalid Edit Question Request");

                    questionsVM.Question = context.questions.Where(s => s.question_id == question_id).FirstOrDefault();
                    if (questionsVM.Question == null)
                        throw new HttpException(500, "Invalid Edit Question Request");

                    var answerss=questionsVM.Question.answers.ToList();
                    answerss.ForEach(g => {
                        questionsVM.AnswersForQuestion_DIV.Add(new AnswersForQuestion
                        {
                            answer_id=g.answer_id,
                            question_id=question_id,
                            description=g.description,
                            correct_p=g.correct_p?"true":"false"
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

                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.QuestionManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Page Called", Constants.USER_ROLES.data_entry, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);

            }

            return View(editView, questionsVM);
        }
        [HttpPost]
        public ActionResult Update(QuestionsVM questionVM)
        {
            SetAuditTrailVariables(null, this.ControllerContext.HttpContext.Request.HttpMethod + ":Submitted for Question: " + questionVM.Question.description);



            //TRIM SPACES AT START AND END
            questionVM.Question.description = questionVM.Question.description != null ? questionVM.Question.description.Trim() : null;
            // ####################### SERVER SIDE VALIDATIONS HERE ###########################
            //---------------------------------------------------------------------------------   

            Validation(questionVM);

            try
            {
                if (ModelState.IsValid)
                {
                    using (Kts_dataEntities context = new Kts_dataEntities())
                    {
                        var dbObj = context.questions.Where(x => x.question_id == questionVM.Question.question_id).FirstOrDefault();
                        
                        questionVM.Question.user_Id = dbObj.user_Id;
                        questionVM.Question.insertion_timestamp = dbObj.insertion_timestamp;
                        //DateTime? InsertionTimeStamp = Utility.GetServerDateTime(context);
                        if (questionVM.Question.question_id != 0)
                        {
                            #region QUESTION_MODIFY

                            //OBJECT ASSIGNMENT
                            ObjectExt.CopyFrom(dbObj, questionVM.Question);

                            #endregion

                            #region AnswersMap
                                if (questionVM.AnswersForQuestion_DIV != null && questionVM.AnswersForQuestion_DIV.Count != 0)
                                {
                                //DateTime INSERTION_TIMESTAMP = Utility.GetServerDateTime(context);
                                    foreach (var objAnswer in questionVM.AnswersForQuestion_DIV)
                                    {
                                        if (objAnswer.answer_id==0)
                                        {
                                            context.answers.Add(new answer
                                            {
                                                question=dbObj,
                                                description=objAnswer.description,
                                                correct_p = (string.IsNullOrEmpty(objAnswer.correct_p) ? "false" : objAnswer.correct_p).ToLower() == "true" ? true : false
                                            });
                                        }
                                        else 
                                        {
                                            answer dbObjAnswer = context.answers.FirstOrDefault(g=>g.answer_id==objAnswer.answer_id);

                                        //if (dbObjAnswer != null)
                                        //{
                                        //answer newObjAnswer = new answer{
                                        //    answer_id=dbObjAnswer.answer_id,
                                        //    question=dbObjAnswer.question,
                                        //    description = objAnswer.description,
                                        //    correct_p = (string.IsNullOrEmpty(objAnswer.correct_p) ? "false" : objAnswer.correct_p).ToLower() == "true" ? true : false
                                        //};
                                        dbObjAnswer.description = objAnswer.description;
                                        dbObjAnswer.correct_p = (string.IsNullOrEmpty(objAnswer.correct_p) ? "false" : objAnswer.correct_p).ToLower() == "true" ? true : false;
                                               // context.Entry(dbObjAnswer).CurrentValues.SetValues(newObjAnswer);
                                           // }

                                        }

                                    }
                                    var answersToArchive = dbObj.answers.Where(s => !questionVM.AnswersForQuestion_DIV.Any(g => g.answer_id == s.answer_id)).ToList();

                                    foreach (var itemAnsrs in answersToArchive)
                                    {
                                        context.Entry(itemAnsrs).State = EntityState.Deleted;
                                    }

                                }
                                else
                                {
                                    var answersToArchive = dbObj.answers.ToList();

                                    foreach (var itemAnsrs in answersToArchive)
                                    {
                                        context.Entry(itemAnsrs).State = EntityState.Deleted;
                                    }
                                }
                                                   
                            #endregion
                        }



                        context.SaveChanges();
                        QuestionsIndexVM objQuestions = new QuestionsIndexVM();
                        objQuestions.MessageTitle = GlobalResult_Type = "Success";
                        objQuestions.MessageDescription = GlobalMessage = "Question: " + dbObj.description + " successfully updated.";
                        return View(indexView, objQuestions);
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

                return View(editView, questionVM);
            }
            finally
            {
                Logger.CreateAuditLog(Constants.SYSTEM_MODULES.QuestionManagement, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString() + ":Submitted for question: " + questionVM.Question.description, Constants.USER_ROLES.data_entry, GlobalResult_Type, GlobalRequest_Type, null, GlobalMessage, GlobalStack_Trace, (LoggedUser != null) ? LoggedUser.Id.ToString() : null);
            }
        }

    }
}