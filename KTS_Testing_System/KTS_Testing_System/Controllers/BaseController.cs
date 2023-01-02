
using KTS_Testing_System.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KTS_Testing_System.Controllers
{
    [ExpirePageActionFilterAttribute]
    public class BaseController : Controller
    {
        // GET: Base
        protected virtual CustomPrincipal LoggedUser
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
    }
}