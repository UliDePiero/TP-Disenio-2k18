using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using TP0.Helpers;
using TP0.Models;
using Microsoft.AspNet.Identity;

namespace TP0.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DetallesDeUsuario()
        {
            ViewBag.Message = "Your application description page.";
            //se llenan las listas de todas las opciones de dispositivos para poder agregarlos a los propios del usuario
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
        public ActionResult DetallesDeUsuario(SubmitViewModel model)
        {
            string id = model.DispositivoSeleccionado;
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializerSettings.Formatting = Formatting.Indented;
            jsonSerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
            jsonSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());

            List<Cliente> clientes = JsonConvert.DeserializeObject<List<Cliente>>(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "test.json.txt")), jsonSerializerSettings);
            AgregarDispositivo(clientes, id);
            foreach(Cliente cli in clientes)
            {
                foreach(DispositivoInteligente di in cli.dispositivosInteligentes)
                {
                    di.Estado = null;
                    di.estadosAnteriores = null;
                }
            }


            var json = JsonConvert.SerializeObject(clientes, jsonSerializerSettings);
            System.IO.File.WriteAllText(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "test.json.txt"), json.ToString());

            return RedirectToAction("Index", "Home");
        }
        private bool EsInteligente(string id)
        {
            List<DispositivoInteligente> opcionesDeDispositivosInteligentes = Helpers.Static.DispositivosTotales.GetDispositivoInteligentes();
            return opcionesDeDispositivosInteligentes.Any(disp => disp.id == id);
        }
        private DispositivoInteligente EncontrarDispositivoInteligente(string id)
        {
            List<DispositivoInteligente> opcionesDeDispositivosInteligentes = Helpers.Static.DispositivosTotales.GetDispositivoInteligentes();
            return opcionesDeDispositivosInteligentes.Find(disp => disp.id == id);
        }
        private DispositivoEstandar EncontrarDispositivoEstandard(string id)
        {
            List<DispositivoEstandar> opcionesDeDispositivosEstandares = Helpers.Static.DispositivosTotales.GetDispositivoEstandars();
            return opcionesDeDispositivosEstandares.Find(disp => disp.id == id);
        }
        private void AgregarDispositivo(List<Cliente> clientes, string Disp)
        {
            string user = User.Identity.GetUserName();
            foreach (Cliente clie in clientes)
            {
                if (clie.usuario == user)
                {
                    if (EsInteligente(Disp))
                    {
                        clie.dispositivosInteligentes.Add(EncontrarDispositivoInteligente(Disp));
                        return;
                    }
                    else
                    {
                        clie.dispositivosEstandares.Add(EncontrarDispositivoEstandard(Disp));
                        return;
                    }
                }
            }
            //Si el cliente no esta en el json, lo agrega
            Cliente c = new Cliente(User.Identity.Name, "", "", User.Identity.Name, "", "", "", "");
            if (EsInteligente(Disp))
            {
                c.dispositivosInteligentes.Add(EncontrarDispositivoInteligente(Disp));
            }
            else
            {
                c.dispositivosEstandares.Add(EncontrarDispositivoEstandard(Disp));
            }
            clientes.Add(c);
        }

        public ActionResult AdministrarDispositivos()
        {
            ViewBag.Message = "Your AdministrarDispositivos page.";
            
            return View();
        }

        public ActionResult Simplex()
        {
            if (User.Identity.IsAuthenticated || Helpers.Static.ClientesImportados.GetClientes() != null)
            {
                //puse estas listas con todos los dispositivos existentes para probar si funciona. ahora tiene q hacerlo con los dispositivos del cleinte
                List<DispositivoEstandar> de = Helpers.Static.DispositivosTotales.GetDispositivoEstandars();
                List<DispositivoInteligente> di = Helpers.Static.DispositivosTotales.GetDispositivoInteligentes();
                //maximos y minimos predeterminados para poder probar la funcionalidad
                foreach (DispositivoEstandar d in de)
                {
                    d.max = 100;
                    d.min = 50;
                }
                foreach (DispositivoInteligente d in di)
                {
                    d.max = 200;
                    d.max = 150;
                }
                string idUsuario = User.Identity.GetUserName();
                Cliente clienteActual = Helpers.Static.ClientesImportados.filtrarCliente(idUsuario);
                string json = Helpers.Static.Simplex.SimplexHelper.generarJson(clienteActual.dispositivosEstandares, clienteActual.dispositivosInteligentes);
                ViewBag.estadoSimplex = "funca";
            }
            else ViewBag.estadoSimplex = "nop";
            return RedirectToAction("AdministrarDispositivos", "Home");
    }
}
