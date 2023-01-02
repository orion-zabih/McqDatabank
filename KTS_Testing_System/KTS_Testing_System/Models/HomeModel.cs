
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KTS_Testing_System.Models
{
    public class HomeModel
    {
        public int Questions { get; set; }
        public int Tests { get; set; }
        public int Users { get; set; }
        public int Subjects { get; set; }
        public int Departments { get; set; }
        public HomeModel()
        {
            Questions = 0;
            Tests = 0;
            Users = 0;
            Departments = 0;
        }

    }
}