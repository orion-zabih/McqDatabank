using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTS_Testing_System.Classes
{
    public class Constants
    {
        public const string CUSTOM_ERROR_URL = "~/Views/Error/CustomError.cshtml";
        public static class USER_ROLES
        {
            public const string admin = "admin";
            public const string data_entry = "data entry";
            public const string test_compiler = "test compiler";
            public const string library_management = "library management";
        }
        public static class SYSTEM_MODULES
        {
            public const string Authentication = "Authentication";
            public const string Home = "system";
            public const string Dashboard = "dashboard";            
            public const string UserManagement = "User Management";
            public const string TestManagement = "Test Management";
            public const string QuestionManagement = "Question Management";
            public const string LibraryManagement = "Library Management";

        }
        public static class MESSAGE_TYPE
        {
            public const string INFO = "info";
            public const string ERROR = "error";
            public const string SUCCESS = "success";
            public const string WARNING = "warning";
            public const string EXCEPTION = "exception";
        }
        public static class AUDIT_MESSAGE
        {
            public const string GET_REQUEST = "get method: requested";
            public const string GET_REQUEST_ACCESS_DENIED = "get request: access denied";
            public const string GET_INVALID_REQUEST = "get request: invalid";
            public const string GET_REQUEST_FAIL = "get request: failed";
            public const string GET_REQUEST_SUCCESS = "get request: success";

            public const string POST_REQUEST = "post method: requested";
            public const string POST_REQUEST_ACCESS_DENIED = "post request: access denied";
            public const string POST_INVALID_REQUEST = "post request: invalid";
            public const string POST_REQUEST_FAIL = "post request: failed";
            public const string POST_REQUEST_SUCCESS = "post request: success";


            public const string AJAX_GET_REQUEST = "ajax get method: requested";
            public const string AJAX_GET_REQUEST_ACCESS_DENIED = "ajax get request: access denied";
            public const string AJAX_GET_INVALID_REQUEST = "ajax get request: invalid";
            public const string AJAX_GET_REQUEST_FAIL = "ajax get request: failed";
            public const string AJAX_GET_REQUEST_SUCCESS = "ajax get request: success";

            public const string AJAX_POST_REQUEST = "ajax post method: requested";
            public const string AJAX_POST_REQUEST_ACCESS_DENIED = "ajax post request: access denied";
            public const string AJAX_POST_INVALID_REQUEST = "ajax post request: invalid";
            public const string AJAX_POST_REQUEST_FAIL = "ajax post request: failed";
            public const string AJAX_POST_REQUEST_SUCCESS = "ajax post request: success";
        }

        public static class ERROR_TITLE
        {
            public const string GENERAL_ERROR = "General Error";
            public const string INVALID_REQUEST = "Invalid Request";
            public const string FUNCTIONALITY_DEACTIVATED = "Functionality Deactivated !";
            public const string CUSTOM_ERROR = "General Error !";
        }
        public static class ERROR_MESSAGE
        {
            public const string INVALID_REQUEST_MSG = "Unable to process the request. Please send a valid request.";
            public const string CONTACT_ADMINISTRATOR = "Please contact system administrator.";
            public const string FUNCTIONALITY_DEACTIVATED_MSG = "Sorry! You cannot perform this action as this is disabled by admin.";
            public const string RECORD_NOT_FOUND = "Record Not Found!";

        }
        public const string PROJECT_TITLE_INITIALS = "KMDB";
        public const string PROJECT_TITLE = "KMC MCQ's Data Bank";
    }
}