using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace hardvin_demo_web_worker_role_service_bus
{
    public class NotificationHub : Hub
    {
        public void ReporteGenerado(string mensaje)
        {
            Clients.All.ReporteGenerado(mensaje);
        }
    }
}