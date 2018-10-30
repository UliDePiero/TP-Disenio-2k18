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
using TP0.Helpers.Static;


namespace TP0.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Map()
        {
            List<Transformador> transformadores;
            using (var contexto = new DBContext())
            {
                transformadores = contexto.Transformadores.ToList();
            }
            foreach (Transformador t in transformadores)
                t.ActualizarEnergiaQueEstaSuministrando();
            
            ViewBag.Transformadores = JsonConvert.SerializeObject(transformadores, Formatting.Indented);
            return View();
        }


        //SE SUPONE QUE ESTO DEBERIA TOMAR TODOS LOS DISPOSITIVOS DE DISPOSITIVOS TOTALES
        //ESTA VACIA LA BASE 
       
        public ActionResult AdministrarDispositivosAdmin()
        {
           List<DispositivoEstatico> disps = new List<DispositivoEstatico>();
            DispositivosTotales.LlenarDisps();
            DispositivosTotales.LlenarListaDeDisposEstaticos(disps);

            
          

            return View(disps);
        }



        public ActionResult SimplexView()
        {
            if (User.Identity.IsAuthenticated || Helpers.Static.ClientesImportados.GetClientes() != null)
            {
                Cliente clie = new Cliente(User.Identity.Name);
                clie.CargarDisps();

                ViewBag.simplexResultado = clie.SolicitarRecomendacion();
                return View();
            }
            ViewBag.simplexResultado = "Hubo un error al ejecutar el simplex";
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
        //Los primeros son para cambiar desde disp propios, los segundos son para cambiar desde la pagina del dispositivo
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
        public ActionResult Encender2(int id, string estadoActual)
        {
            if (estadoActual != "Encendido")
            {
                DispositivoInteligente DI = new DispositivoInteligente(id);
                DI.Encender();
            }
            return RedirectToAction("DetallesInteligente", "Home", new { id });
        }
        public ActionResult Apagar2(int id, string estadoActual)
        {
            if (estadoActual != "Apagado")
            {
                DispositivoInteligente DI = new DispositivoInteligente(id);
                DI.Apagar();
            }
            return RedirectToAction("DetallesInteligente", "Home", new { id });
        }
        public ActionResult Ahorro2(int id, string estadoActual)
        {
            if (estadoActual != "Ahorro")
            {
                DispositivoInteligente DI = new DispositivoInteligente(id);
                DI.AhorrarEnergia();
            }
            return RedirectToAction("DetallesInteligente", "Home", new { id });
        }
        public ActionResult BorrarDispositivoClie(int id)
        {
            using (var db = new DBContext())
            {
                var disp = db.Dispositivos.FirstOrDefault(d => d.DispositivoID == id);
                db.Dispositivos.Remove(disp);
                db.SaveChanges();
            }
            return RedirectToAction("DispositivosPropios", "Home");
        }

        public ActionResult DetallesEstandar(int id)
        {
            return View(new DispositivoEstandar(id));
        }
        public ActionResult DetallesInteligente(int id)
        {
            DispositivoInteligente d = new DispositivoInteligente(id);

            return View(d);
        }

        [HttpGet]
        public ActionResult AgregarDispositivoClie()
        {   //se llenan la lista de todas las opciones de dispositivos para poder agregarlos a los propios del usuario
            List<SelectListItem> disps = DispositivosTotales.GetDispositivos();
            if (disps.Count == 0)
                ViewBag.Message = "No hay dispositivos para agregar actualmente.";
            else
                ViewBag.Message = "Seleccione un dispositivo de la lista para agregar.";

            ViewBag.selectListItems = disps;
            return View();
        }
        [HttpPost]
        public ActionResult AgregarDispositivoClie(SubmitViewModel model)
        {
            //Agrega el nuevo dispositivo al usuario
            string codigo = model.DispositivoSeleccionado;

            Cliente c = new Cliente(User.Identity.GetUserName());
            if (DispositivosTotales.EsInteligente(codigo))
            {
                DispositivoInteligente disp = DispositivosTotales.EncontrarDispositivoInteligente(codigo);
                c.AgregarDispInteligente(disp);
            }
            else
            {
                DispositivoEstandar disp = DispositivosTotales.EncontrarDispositivoEstandard(codigo);
                c.AgregarDispEstandar(disp);
            }

            return RedirectToAction("DispositivosPropios", "Home");
        }

        [HttpGet]
        public ActionResult ConvertirEnInteligente(int id)
        {
            List<SelectListItem> marcas = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Samsung", Value = "Samsung" },
                new SelectListItem() { Text = "HP", Value = "HP" },
                new SelectListItem() { Text = "Apple", Value = "Apple" }
            };

            ViewBag.Id = id;
            ViewBag.Message = "Seleccione la marca del adaptador";
            ViewBag.marcas = marcas;
            return View();
        }
        [HttpPost]
        public ActionResult ConvertirEnInteligente(int id, FormCollection form)
        {
            Cliente c = new Cliente(User.Identity.GetUserName());
            c.AdaptarDispositivo(new DispositivoEstandar(id), form["marcas"].ToString());
            return RedirectToAction("DispositivosPropios", "Home");
        }

        [HttpGet]
        public ActionResult ConsultarConsumo()
        {//Metodo para consultar el consumo en un periodo
            ViewBag.consumo = "";
            return View();
        }
        [HttpPost]
        public ActionResult ConsultarConsumo(DateTime FechaInicio, DateTime FechaFin)
        {
            Cliente clie = new Cliente(User.Identity.Name);
            clie.CargarDisps();

            ViewBag.consumo = "Consumo: " + clie.KwTotales(FechaInicio, FechaFin) + " Kw";
            ViewBag.fechas = FechaInicio.ToShortDateString() + " - " + FechaFin.ToShortDateString();
            return View();
        }
    }
}
