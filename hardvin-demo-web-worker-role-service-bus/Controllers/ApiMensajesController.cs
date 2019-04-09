using Microsoft.AspNet.SignalR;
using System.Web.Http;

namespace hardvin_demo_web_worker_role_service_bus.Controllers
{
    public class ApiMensajesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.ReporteGenerado("ReporteGenerado");
            return this.Ok("Se realizó la notificacion");
        }
    }
}
