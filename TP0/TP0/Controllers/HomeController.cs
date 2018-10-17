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
using TP0.Helpers.ORM;


namespace TP0.Controllers
{
    public class HomeController : Controller
    {
        //Cliente clienteActual;
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AgregarDispositivos()
        {
            ViewBag.Message = "Your application description page.";
            //se llenan las listas de todas las opciones de dispositivos para poder agregarlos a los propios del usuario
            List<DispositivoEstandar> opcionesDeDispositivosEstandares = Helpers.Static.DispositivosTotales.GetDispositivoEstandars();
            List<DispositivoInteligente> opcionesDeDispositivosInteligentes = Helpers.Static.DispositivosTotales.GetDispositivoInteligentes();

            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (DispositivoInteligente disp in opcionesDeDispositivosInteligentes)
            {
                selectListItems.Add(new SelectListItem() { Value = disp.Codigo, Text = disp.Nombre });
            }

            foreach (DispositivoEstandar disp in opcionesDeDispositivosEstandares)
            {
                selectListItems.Add(new SelectListItem() { Value = disp.Codigo, Text = disp.Nombre });
            }

            ViewBag.selectListItems = selectListItems;

            return View();
        }
        [HttpPost]
        public ActionResult AgregarDispositivos(SubmitViewModel model)
        {
            //Agrega el nuevo dispositivo al usuario
            string codigo = model.DispositivoSeleccionado;

            Cliente c = new Cliente(User.Identity.GetUserName());
            if (EsInteligente(codigo))
            {
                DispositivoInteligente disp = EncontrarDispositivoInteligente(codigo);
                c.AgregarDispInteligente(disp);
            }
            else
            {
                DispositivoEstandar disp = EncontrarDispositivoEstandard(codigo);
                c.AgregarDispEstandar(disp);
            }

            return RedirectToAction("Index", "Home");
        }
        private bool EsInteligente(string id)
        {
            List<DispositivoInteligente> opcionesDeDispositivosInteligentes = Helpers.Static.DispositivosTotales.GetDispositivoInteligentes();
            return opcionesDeDispositivosInteligentes.Any(disp => disp.Codigo == id);
        }
        private DispositivoInteligente EncontrarDispositivoInteligente(string id)
        {
            List<DispositivoInteligente> opcionesDeDispositivosInteligentes = Helpers.Static.DispositivosTotales.GetDispositivoInteligentes();
            return opcionesDeDispositivosInteligentes.Find(disp => disp.Codigo == id);
        }
        private DispositivoEstandar EncontrarDispositivoEstandard(string id)
        {
            List<DispositivoEstandar> opcionesDeDispositivosEstandares = Helpers.Static.DispositivosTotales.GetDispositivoEstandars();
            return opcionesDeDispositivosEstandares.Find(disp => disp.Codigo == id);
        }

        public ActionResult SimplexView()
        {
            ViewBag.Message = "Your AdministrarDispositivos page.";
            return View();
        }

        public ActionResult AdministrarReglas(SubmitViewModel model)
        {
            ViewBag.Message = "Tus reglas";
            List<Regla> reglas = new List<Regla>();
            List<Dispositivo> dispositivosPropios = new List<Dispositivo>();
            Usuario user;

            using (var db = new DBContext())
            {
                user = db.Usuarios.FirstOrDefault(u => u.Username == User.Identity.Name);
                dispositivosPropios = db.Dispositivos.Where(d => d.UsuarioID == user.UsuarioID).ToList();
                List<String> sl = new List<String>();

                foreach (Dispositivo disp in dispositivosPropios)
                {
                    sl.Add(disp.Nombre);

                }
                ViewBag.dispositivosDelusuario = sl;

                if (ModelState.IsValid)
                {
                    Regla r = new Regla(model.ValorMax, model.ValorMin, model.disSelec.act.ActuadorID);
                }


                }
            return View(dispositivosPropios);


        }


        [HttpGet]
        public ActionResult DispositivosPropios()
        {
            ViewBag.Message = "Tus dispositivos:";

            Cliente clie = new Cliente(User.Identity.Name);
            clie.CargarDisps();

            ViewBag.total = clie.TotalConsumo();
            ViewBag.dispositivos = clie.Dispositivos.Count();
            ViewBag.dispE = clie.DispositivosEncendidos();
            ViewBag.dispA = clie.DispositivosApagados();
            ViewBag.dispAh = clie.DispositivosEnAhorro();
            ViewBag.dispEs = clie.DispositivosEstandares();

            return View(clie.Dispositivos);
        }

        //Metodos para cambiar el estado del dispositivo
        public ActionResult Encender(int id, string estadoActual)
        {
            if (estadoActual != "Encendido")
            {
                DispositivoInteligente DI = new DispositivoInteligente(id);
                DI.Encender();
            }
            return RedirectToAction("DispositivosPropios", "Home");
        }
        public ActionResult Apagar(int id, string estadoActual)
        {
            if (estadoActual != "Apagado")
            {
                DispositivoInteligente DI = new DispositivoInteligente(id);
                DI.Apagar();
            }
            return RedirectToAction("DispositivosPropios", "Home");
        }
        public ActionResult Ahorro(int id, string estadoActual)
        {
            if (estadoActual != "Ahorro")
            {
                DispositivoInteligente DI = new DispositivoInteligente(id);
                DI.AhorrarEnergia();
            }
            return RedirectToAction("DispositivosPropios", "Home");
        }
        public ActionResult ConvertirEnInteligente(int id)
        {
            //No tengo forma de saber la marca
            //DispositivoInteligente DI = new DispositivoEstandar(id).ConvertirEnInteligente();
            return RedirectToAction("DispositivosPropios", "Home");
        }

        [HttpGet]
        public ActionResult Simplex()
        {
            if (User.Identity.IsAuthenticated || Helpers.Static.ClientesImportados.GetClientes() != null)
            {
                List<Dispositivo> disp = new List<Dispositivo>();


                //Trae de la db todos los dispositivos del usuario para ejecutar el simplex
                using (var db = new DBContext())
                {
                    Usuario user = db.Usuarios.FirstOrDefault(u => u.Username == User.Identity.GetUserName());
                    disp = db.Dispositivos.Where(d => d.UsuarioID == user.UsuarioID).ToList();
                }

                //maximos y minimos predeterminados para poder probar la funcionalidad
                foreach (DispositivoEstandar d in disp)
                {
                    d.Max = 100;
                    d.Min = 50;
                }
                foreach (DispositivoInteligente d in disp)
                {
                    d.Max = 200;
                    d.Max = 150;
                }
               string idUsuario = User.Identity.GetUserName();

                //clienteActual = Helpers.Static.ClientesImportados.filtrarCliente(idUsuario);

                Cliente clienteActual = Helpers.Static.ClientesImportados.filtrarCliente(idUsuario);

                string resu = clienteActual.SolicitarRecomendacion();
                //string json = Helpers.Static.Simplex.SimplexHelper.generarJson(clienteActual.dispositivosEstandares, clienteActual.dispositivosInteligentes);
                ViewBag.estadoSimplex = resu;

                return RedirectToAction("Simplex2", "Home", new { mensaje = "Recomendación: " + resu });
                //return View("~/Views/Home/Simplex2.cshtml");

            }
            else ViewBag.estadoSimplex = "nop";
            return RedirectToAction("AdministrarDispositivos", "Home");
           
        }
        [HttpGet]
        public ActionResult Simplex2(string mensaje)
        {
            ViewBag.Message = mensaje;
            return View();
        }
    }
}
