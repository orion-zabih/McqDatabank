using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTS_Testing_System.ViewModel
{
    public class ResponseMessage
    {
        public string MessageCode { get; set; }
        public string MessageTitle { get; set; }
        public string MessageDescription { get; set; }
        public ResponseMessage()
        {
            MessageCode = "";
            MessageTitle = "";
            MessageDescription = "";
        }
    }
}