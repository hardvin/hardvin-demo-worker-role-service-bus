using System.Web;
using System.Web.Mvc;

namespace hardvin_demo_web_worker_role_service_bus
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
