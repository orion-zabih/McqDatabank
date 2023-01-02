using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static KTS_Testing_System.Classes.Constants;

namespace KTS_Testing_System.ViewModel
{
    public class CustomErrorVM
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public string ReturnURL { get; set; }

        public CustomErrorVM()
        {
            Message = string.Empty;
            ReturnURL = string.Empty;
            Title = string.Empty;
            Type = MESSAGE_TYPE.WARNING;
        }

        public CustomErrorVM(string title, string message, string type, string returnURL)
        {
            Message = message;
            ReturnURL = returnURL;
            Title = title;
            Type = type;
        }
    }
}