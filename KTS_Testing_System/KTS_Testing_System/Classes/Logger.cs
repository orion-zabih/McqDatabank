using KTS_Testing_System.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using static KTS_Testing_System.Classes.Constants;

namespace KTS_Testing_System.Classes
{
    public class Logger
    {
        private static Stream logFile;
        private static bool isInitialized = false;

        //public static bool LogToDatabase(string Module_Code, string ActivityType, string Request_Type, string Action, string Record_Id, string Log_Detail ,int? User_Code, int? Election_code, )
        //LogToDatabase(pr, receive_application, post, registration, 1, "detail if applicable", 1, 1)

        //LogToDatabase(pr, modify_party, get, EditParty, 1, "detail if applicable", 1, 1)
        //LogToDatabase(pr, modify_party, post, EditParty, 1, "success", 1, 1)
        //LogToDatabase(pr, modify_party, post, EditParty, 1, "fail", 1, 1)

        //LogToDatabase(pr, legal_review, get, PartyReview, 10, "detail if applicable", 1, 1)
        //LogToDatabase(pr, approver, get, PartyReview, 10, "detail if applicable", 1, 1)

        //LogToDatabase(pva, add_polling_venue, post, Edit, 57, "detail if applicable", 21, 1)
        //LogToDatabase(pva, add_polling_venue, post, Edit, 58, "n/a", 47, 1)
        //LogToDatabase(pva, quality_assurance, get, Edit, 57, "detail if applicable", 21, 1)
        //LogToDatabase(pva, quality_assurance, post, Edit, 57, "success", 21, 1)
        //LogToDatabase(pva, quality_assurance, post, Edit, 58, "failure", 21, 1)
        //LogToDatabase(pva, supervisor_review, get, Edit, 57, "n/a", 32, 1)
        //LogToDatabase(pva, supervisor_review, post, Edit, 57, "success", 21, 1)

        public static void CreateAuditLog(string ModuleCode, string Controller, string Action, string ActivityType, string ResultType, string Request_Type, object Record_Id, string Log_Detail, string Stack_Trace, string Username)
        {
            string ReqType = "";

            if (ResultType == "start")
            {
                if (Request_Type.ToUpper() == "GET")
                {
                    ReqType = AUDIT_MESSAGE.GET_REQUEST;
                    ////Log_Detail = "Start of " + Utility.ToTitlecase(ReqType) + " " + Action + " method";
                }
                else if (Request_Type.ToUpper() == "POST")
                {
                    ReqType = AUDIT_MESSAGE.POST_REQUEST;
                    //////Log_Detail = "Start of " + Utility.ToTitlecase(ReqType) + " " + Action + " method";
                }
            }
            else if (ResultType == "success")
            {
                if (Request_Type.ToUpper() == "GET")
                {
                    ReqType = AUDIT_MESSAGE.GET_REQUEST_SUCCESS;
                    //////Log_Detail = "Successful End of " + Utility.ToTitlecase(ReqType) + " " + Action + " method";
                }
                else if (Request_Type.ToUpper() == "POST")
                {
                    ReqType = AUDIT_MESSAGE.POST_REQUEST_SUCCESS;
                    //////Log_Detail = "Successful End of " + Utility.ToTitlecase(ReqType) + " " + Action + " method";
                }
            }
            else if (ResultType == "validation")
            {
                if (Request_Type.ToUpper() == "GET")
                {
                    ReqType = AUDIT_MESSAGE.GET_REQUEST_FAIL;
                    Log_Detail = "validation failed at " + Utility.ToTitlecase(ReqType) + " " + Action + " method";
                }
                else if (Request_Type.ToUpper() == "POST")
                {
                    ReqType = AUDIT_MESSAGE.POST_REQUEST_FAIL;
                    Log_Detail = "validation failed at " + Utility.ToTitlecase(ReqType) + " " + Action + " method";
                }
            }
            else if (ResultType == "exception")
            {
                if (Request_Type.ToUpper() == "GET")
                {

                    if (Log_Detail.Equals(ERROR_TITLE.INVALID_REQUEST))
                    {
                        Log_Detail = "";
                        ReqType = AUDIT_MESSAGE.GET_INVALID_REQUEST;
                        ////Log_Detail = Mess"Invalid Request at " + Utility.ToTitlecase(ReqType) + " " + Action + " method"; ;
                    }
                    else
                    {
                        ReqType = AUDIT_MESSAGE.GET_REQUEST_FAIL;
                        //////Log_Detail = "Exception at " + Utility.ToTitlecase(ReqType) + " " + Action + " method"; //AUDIT_MESSAGE.GET_REQUEST_FAIL;
                    }
                }
                else if (Request_Type.ToUpper() == "POST")
                {

                    if (Log_Detail.Equals(ERROR_TITLE.INVALID_REQUEST))
                    {
                        Log_Detail = "";
                        ReqType = AUDIT_MESSAGE.POST_INVALID_REQUEST;
                        ////////Log_Detail = "Invalid Request at " + Utility.ToTitlecase(ReqType) + " " + Action + " method"; ;
                    }
                    else
                    {
                        ReqType = AUDIT_MESSAGE.POST_REQUEST_FAIL;
                        ////////Log_Detail = "Exception at " + Utility.ToTitlecase(ReqType) + " " + Action + " method"; //AUDIT_MESSAGE.POST_REQUEST_FAIL;
                    }
                }

                //WRITE TO FILE
                Logger.Write(Controller, Action, Log_Detail, Stack_Trace);
            }



            //Logger.LogToDatabase(ModuleCode, ActivityType, ReqType, Action, (Record_Id == null ? "" : Record_Id.ToString()), Log_Detail, Username, Election_code);
        }

        //public static bool LogToDatabase(string Module_Code, string ActivityType, string Request_Type, string Action, string Record_Id, string Log_Detail, string Username, decimal? Election_code)
        //{
        //    var context = new Entities();
        //    try
        //    {
        //        if (Log_Detail != null && Log_Detail.Length > 1020)
        //        {
        //            Log_Detail = Log_Detail.Substring(0, 1020);
        //        }
        //        if (Action != null && Action.Length > 64)
        //        {
        //            Action = Action.Substring(0, 63);
        //        }

        //        AUDIT_LOGS auditLogRow = new AUDIT_LOGS()
        //        {
        //            ELECTION_EVENT_ID = Election_code,
        //            APPLICATION_NAME = "Management MIS",
        //            MODULE = Module_Code + "," + ActivityType,
        //            METHOD = Action,
        //            MESSAGE = Log_Detail,
        //            USERNAME = Username,
        //            SERVER_IP = Utility.GetLocalIPAddress(NetworkInterfaceType.Ethernet),
        //            CLIENT_IP = Utility.GetUserIP(),
        //            INSERTION_TIMESTAMP = Utility.GetServerDateTime(ref context)

        //        };
        //        context.AUDIT_LOGS.Add(auditLogRow);
        //        context.SaveChanges();
        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        Write("Logger", "LogToDatabase", "Error Writing Log to Database", ex.StackTrace);
        //        return false;
        //    }
        //    finally
        //    {
        //        if (context != null)
        //            context.Dispose();
        //    }

        //}

        //OLD CODE AUDIT LOG
        //public static bool LogToDatabase(string Action, string Log_Detail, string Request_Type, int? User_Code, int? Election_code, string Module_Code)
        //{
        //    try
        //    {
        //        if (Log_Detail.Length > 512)
        //        {
        //            Log_Detail = Log_Detail.Substring(0, 511);
        //        }
        //        if (Action.Length > 64)
        //        {
        //            Action = Action.Substring(0, 63);
        //        }

        //        using (var context = new Entities())
        //        {
        //            AUDIT_LOGS auditLogRow = new AUDIT_LOGS();
        //            //{
        //            //    User_code = User_Code,
        //            //    Election_code = Election_code,
        //            //    Activity_type = Action,
        //            //    Request_type = Request_Type,
        //            //    Log_details = Log_Detail == null ? "" : Log_Detail,
        //            //    Module_code = Module_Code == null ? "" : Module_Code,
        //            //    System_ip = Utility.GetRemoteIPAddress()
        //            //};
        //            context.AUDIT_LOGS.Add(auditLogRow);
        //            context.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Write("Logger", "LogToDatabase", "Error Writing Log to Database", ex.StackTrace);
        //        return false;
        //    }


        //}


        public static CustomErrorVM DefineCustomError(Exception ex)
        {
            string MsgType = MESSAGE_TYPE.ERROR;
            string ErrorTitle = ex.Message;
            string ErrorDescription = ERROR_MESSAGE.CONTACT_ADMINISTRATOR;

            if (ErrorTitle == ERROR_TITLE.INVALID_REQUEST)
            {
                ErrorDescription = ERROR_MESSAGE.INVALID_REQUEST_MSG;
            }
            else if (ErrorTitle == ERROR_TITLE.FUNCTIONALITY_DEACTIVATED)
            {
                ErrorDescription = ERROR_MESSAGE.FUNCTIONALITY_DEACTIVATED_MSG;
                MsgType = MESSAGE_TYPE.INFO;
            }
            else if (ErrorTitle == ERROR_TITLE.CUSTOM_ERROR)
            {
                ErrorDescription = ex.InnerException.Message;
                MsgType = MESSAGE_TYPE.INFO;
            }
            else
            {
                ErrorTitle = ERROR_TITLE.GENERAL_ERROR;
                ErrorDescription = ERROR_MESSAGE.CONTACT_ADMINISTRATOR;
            }
            return new CustomErrorVM(ErrorTitle, ErrorDescription, MsgType, "");
        }

        public static void closeFile()
        {
            if (isInitialized)
            {
                Trace.Close();
                logFile.Dispose();
                isInitialized = false;
            }
        }

        public static void createFile()
        {
            try
            {
                string appPath = HttpContext.Current.Request.ApplicationPath;
                string physicalPath = HttpContext.Current.Request.MapPath(appPath);
                DirectoryInfo dir = new DirectoryInfo(physicalPath + "\\Log");
                if (!dir.Exists)
                {
                    dir.Create();
                }
                //DirectoryInfo dir = new DirectoryInfo(physicalPath + "\\Log");
                if (!dir.Exists)
                {
                    dir.Create();
                }
                dir = null;

                if (File.Exists(physicalPath + "\\Log\\" + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year + ".log"))
                {
                    logFile = File.Open(physicalPath + "\\Log\\" + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year + ".log", FileMode.Append);
                }
                else
                {
                    logFile = File.Create(physicalPath + "\\Log\\" + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year + ".log");
                }

                TextWriterTraceListener myTextListener = new TextWriterTraceListener(logFile);

                Trace.Listeners.Add(myTextListener);
                isInitialized = true;
            }
            catch (Exception)
            {
                isInitialized = false;
            }

        }

        public static void Write(string argController, string argAction, string argErrorMsg, string argStackTrace)
        {
            if (!isInitialized)
                createFile();

            Trace.WriteLine("");
            Trace.WriteLine("--------BEGIN ERROR LOG ON: " + System.DateTime.Now.ToString() + "-------------------------------------------------");
            Trace.WriteLine("--------CONTROLLER/ACTION: " + argController + "/" + argAction);
            Trace.WriteLine("--------MESSAGE: " + argErrorMsg);
            Trace.WriteLine("--------STACK TRACE: " + argStackTrace);
            Trace.WriteLine("--------END OF ERROR LOG-------------------------------------------------------------------------");
            Trace.Flush();
            closeFile();
        }
    }
}