using KTS_Entity;
using KTS_Testing_System.Classes;
using KTS_Testing_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KTS_Testing_System.Controllers
{
    [AccessDeniedAuthorizeAttribute]
    public class HomeController : BaseController
    {
        readonly string indexView = "~/Views/Home/Index.cshtml";
        public ActionResult Index()
        {
            HomeModel homeModel = new HomeModel();
            using (Kts_dataEntities context = new Kts_dataEntities())
            {
                homeModel.Users = context.Users.Count();
                homeModel.Tests = context.User_Tests.Where(g=>g.status=="ready").Count();
                homeModel.Subjects = context.subjects.Count();
                homeModel.Questions = context.questions.Count();
                //homeModel.Departments = context.Departments.Count();
            }
            return View(indexView,homeModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}