using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTS_Testing_System.Classes
{
    public class CustomPrincipalSerializeModel
    {
        public decimal Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string designation { get; set; }
       
        public string department_name { get; set; }
        public decimal department_code { get; set; }
    }
}