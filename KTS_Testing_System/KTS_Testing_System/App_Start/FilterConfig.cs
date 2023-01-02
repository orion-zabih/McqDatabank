using KTS_Testing_System.Filter;
using System.Web;
using System.Web.Mvc;

namespace KTS_Testing_System
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LoggingFilterAttribute());
        }
    }
}
