using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using TP0.Helpers;
using TP0.Models;

namespace TP0.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DetalleDeUsuario()
        {
            ViewBag.Message = "Your application description page.";
            
            List<DispositivoEstandar> opcionesDeDispositivosEstandares = Helpers.Static.DispositivosTotales.GetDispositivoEstandars();
            List<DispositivoInteligente> opcionesDeDispositivosInteligentes = Helpers.Static.DispositivosTotales.GetDispositivoInteligentes();
            
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (DispositivoInteligente disp in opcionesDeDispositivosInteligentes)
            {
                selectListItems.Add(new SelectListItem() { Value = disp.id, Text = disp.nombre });
            }

            foreach (DispositivoEstandar disp in opcionesDeDispositivosEstandares)
            {
                selectListItems.Add(new SelectListItem() { Value = disp.id, Text = disp.nombre });
            }

            ViewBag.selectListItems = selectListItems;

            return View();
        }
        [HttpPost]
        public ActionResult DetalleDeUsuario(SubmitViewModel model)
        {
            string id = model.DispositivoSeleccionado;
            //Aca le pasamos el id de dispositivo nuevo del usuario
            return RedirectToAction("DetalleDeUsuario", "Home");
        }

        public ActionResult AdministrarDispositivos()
        {
            ViewBag.Message = "Your AdministrarDispositivos page.";
            
            return View();
        }

        public ActionResult Simplex()
        {
            //puse estas listas con todos los dispositivos existentes para probar si funciona. ahora tiene q hacerlo con los dispositivos del cleinte
            List<DispositivoEstandar> de = Helpers.Static.DispositivosTotales.GetDispositivoEstandars();
            List<DispositivoInteligente> di = Helpers.Static.DispositivosTotales.GetDispositivoInteligentes();
            string json = Helpers.Static.Simplex.SimplexHelper.generarJson(de, di);
            return RedirectToAction("AdministrarDispositivos", "Home");
        }
    }
}