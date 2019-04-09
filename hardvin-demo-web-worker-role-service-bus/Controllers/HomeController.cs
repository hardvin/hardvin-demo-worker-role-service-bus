using hardvin_demo_web_worker_role_service_bus.Models;
using hardvin_demo_web_worker_role_service_bus.Servicios;
using Microsoft.AspNet.SignalR;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Linq;
using System.Web.Mvc;

namespace hardvin_demo_web_worker_role_service_bus.Controllers
{
    public class HomeController : Controller
    {
        private DemoModelContainer db = new DemoModelContainer();
        private QueueConnector clienteservicebus = new QueueConnector();
        public ActionResult Index()
        {
            return View();
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

        public ActionResult SinWorkerRole()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult SinWorkerRole(int? id)
        {
            // Simulemos que está lento el motor :)
            System.Threading.Thread.Sleep(20000);

            Reporte reporte = new Reporte { Nombre = $"Reporte-{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}", FechaGeneracion = DateTime.Now };
            db.Reportes.Add(reporte);
            db.SaveChanges();

            // retorno los reportes generados.
            return RedirectToAction("ListaReportes");
        }

        public ActionResult ConWorkerRole()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ConWorkerRole(int? id)
        {
            // Se ejecuta la cola:
            clienteservicebus.Initialize();
            var message = new BrokeredMessage(id);
            message.Label = "Ejecutar Reporte";

            clienteservicebus.ClienteCola.SendAsync(message);
            ViewBag.Mensaje = "Se ha enviado la solicitud para procesar el informe, una vez finalizada la tarea, será informado.";

            return View();
        }


        public ActionResult ListaReportes()
        {

            var listareportes = db.Reportes.AsNoTracking().OrderBy(m => m.FechaGeneracion).ToList();

            // retorno los reportes generados.
            return View("ListaReportes", listareportes);
        }
    }
}