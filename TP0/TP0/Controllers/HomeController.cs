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
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            List<DispositivoEstandar> opcionesDeDispositivosEstandares = new List<DispositivoEstandar>();
            List<DispositivoInteligente> opcionesDeDispositivosInteligentes = new List<DispositivoInteligente>();
            DispositivoInteligente aireAcondicionado3500 = new DispositivoInteligente("aire acondicionado de 3500 frigorias", "0011", 1.613);
            DispositivoInteligente aireAcondicionado2200 = new DispositivoInteligente("aire acondicionado de 2200 frigorias", "0012", 1.013);
            DispositivoEstandar televisor21 = new DispositivoEstandar("televisor de 21 pulgadas", "0012", 0.075, 0);
            DispositivoEstandar televisor29a34 = new DispositivoEstandar("televisor de 29 a 34 pulgadas", "0013", 0.175, 0);
            DispositivoEstandar televisorLCD40 = new DispositivoEstandar("televisor LCD de 40 pulgadas", "0014", 0.18, 0);
            DispositivoInteligente televisorLED24 = new DispositivoInteligente("televisor LED de 24 pulgadas", "0015", 0.04);
            DispositivoInteligente televisorLED32 = new DispositivoInteligente("televisor LED de 32 pulgadas", "0016", 0.055);
            DispositivoInteligente televisorLED40 = new DispositivoInteligente("televisor LED de 40 pulgadas", "0017", 0.08);
            DispositivoInteligente heladeraConFreezer = new DispositivoInteligente("heladera con freezer", "0018", 0.09);
            DispositivoInteligente heladeraSinFreezer = new DispositivoInteligente("heladera sin freezer", "0019", 0.075);
            DispositivoInteligente lavarropasAuto = new DispositivoInteligente("lavarropas automatico de 5kg", "0020", 0.175);
            DispositivoEstandar lavarropasAutoCalentamiento = new DispositivoEstandar("lavarropas automatico de 5kg con calentamiento", "0021", 0.875, 0);
            DispositivoEstandar lavarropasSemiAuto = new DispositivoEstandar("lavarropas semi-automatico de 5kg", "0022", 0.1275, 0);
            DispositivoEstandar ventiladorDePie = new DispositivoEstandar("ventilador de pie", "0023", 0.09, 0);
            DispositivoInteligente ventiladorDeTecho = new DispositivoInteligente("ventilador de techo", "0024", 0.06);
            DispositivoEstandar planchaAVapor = new DispositivoEstandar("plancha a vapor", "0011", 0.75, 0);
            DispositivoEstandar microondasConvencional = new DispositivoEstandar("microondas convencional", "0011", 0.64, 0);
            DispositivoInteligente pcDeEscritorio = new DispositivoInteligente("pc de escritorio", "0011", 0.4);
            DispositivoInteligente lamparaHalogena40W = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04);
            DispositivoInteligente lamparaHalogena60W = new DispositivoInteligente("lampara halogena de 60 W", "0011", 0.06);
            DispositivoInteligente lamparaHalogena100W = new DispositivoInteligente("lampara halogena de 100 W", "0011", 0.015);
            DispositivoInteligente lampara11W = new DispositivoInteligente("lampara de 11 W", "0011", 0.011);
            DispositivoInteligente lampara15W = new DispositivoInteligente("lampara de 15 W", "0011", 0.015);
            DispositivoInteligente lampara20W = new DispositivoInteligente("lampara de 20 W", "0011", 0.02);

            opcionesDeDispositivosInteligentes.Add(aireAcondicionado2200);
            opcionesDeDispositivosInteligentes.Add(aireAcondicionado3500);
            opcionesDeDispositivosInteligentes.Add(televisorLED24);
            opcionesDeDispositivosInteligentes.Add(televisorLED32);
            opcionesDeDispositivosInteligentes.Add(televisorLED40);
            opcionesDeDispositivosInteligentes.Add(heladeraConFreezer);
            opcionesDeDispositivosInteligentes.Add(heladeraSinFreezer);
            opcionesDeDispositivosInteligentes.Add(lavarropasAuto);
            opcionesDeDispositivosInteligentes.Add(ventiladorDeTecho);
            opcionesDeDispositivosInteligentes.Add(pcDeEscritorio);
            opcionesDeDispositivosInteligentes.Add(lampara11W);
            opcionesDeDispositivosInteligentes.Add(lamparaHalogena100W);
            opcionesDeDispositivosInteligentes.Add(lamparaHalogena40W);
            opcionesDeDispositivosInteligentes.Add(lamparaHalogena60W);
            opcionesDeDispositivosInteligentes.Add(lampara15W);
            opcionesDeDispositivosInteligentes.Add(lampara20W);

            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (DispositivoInteligente disp in opcionesDeDispositivosInteligentes)
            {
                selectListItems.Add(new SelectListItem() { Value = disp.id, Text = disp.nombre });
            }

            opcionesDeDispositivosEstandares.Add(televisor21);
            opcionesDeDispositivosEstandares.Add(televisor29a34);
            opcionesDeDispositivosEstandares.Add(televisorLCD40);
            opcionesDeDispositivosEstandares.Add(lavarropasAutoCalentamiento);
            opcionesDeDispositivosEstandares.Add(lavarropasSemiAuto);
            opcionesDeDispositivosEstandares.Add(ventiladorDePie);
            opcionesDeDispositivosEstandares.Add(planchaAVapor);
            opcionesDeDispositivosEstandares.Add(microondasConvencional);

            foreach (DispositivoEstandar disp in opcionesDeDispositivosEstandares)
            {
                selectListItems.Add(new SelectListItem() { Value = disp.id, Text = disp.nombre });
            }

            ViewBag.selectListItems = selectListItems;

            return View();
        }
        [HttpPost]
        public ActionResult About(string id)
        {
            //Aca ponemos cuando selecciona dispositivo
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            
            return View();
        }
    }
}