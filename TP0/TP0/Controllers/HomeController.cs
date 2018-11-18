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
using System.Data.Entity.Infrastructure;
using MongoDB.Driver;

namespace TP0.Controllers
{
    public class HomeController : Controller
    {
        //VISTAS PUBLICAS
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


        //VISTAS DE ADMIN
        public ActionResult ReportesAdmin()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ReporteHogar()
        {
            ViewBag.consumo = "";
            IEnumerable<Cliente> casas = ClientesImportados.GetClientes();//se carga lista de casas
            List<SelectListItem> hogares = new List<SelectListItem>();
            foreach (Cliente c in casas)
            {
                hogares.Add(new SelectListItem() { Value = c.UsuarioID.ToString(), Text = c.Username });
            }
            ViewBag.IdSeleccionado = hogares;

            return View();
        }
        [HttpPost]
        public ActionResult ReporteHogar(SubmitViewModel model, DateTime FechaInicio, DateTime FechaFin)
        {

            using (var db = new DBContext())
            {
                Usuario usu = db.Usuarios.FirstOrDefault(u => u.UsuarioID == model.IdSeleccionado);
                Reporte reporteModelo = new Reporte("Hogar", usu.UsuarioID.ToString(), 0, FechaInicio, FechaFin);

                IEnumerable<Cliente> casas = ClientesImportados.GetClientes();//se carga lista de casas
                List<SelectListItem> hogares = new List<SelectListItem>();
                foreach (Cliente c in casas)
                {
                    hogares.Add(new SelectListItem() { Value = c.UsuarioID.ToString(), Text = c.Username });
                }
                ViewBag.IdSeleccionado = hogares;

                //if(reporte esta en mongo){find} else{ se crea y se guarda en mongo}

                var reportesEncontrados = Mongo.getReporte("Hogar", usu.UsuarioID, FechaInicio, FechaFin);
                if (reportesEncontrados.Count > 0)
                {
                    var reporte = reportesEncontrados[0];
                    ViewBag.consumo = "Consumo mongo: " + reporte.consumo.ToString() + "Kw";
                }
                else
                {
                    reporteModelo.consumo = usu.KwTotales(FechaInicio, FechaFin);
                    ViewBag.consumo = "Consumo: " + reporteModelo.consumo + "Kw";
                    Mongo.insertarReporte(reporteModelo);
                }
                ViewBag.fechas = FechaInicio.ToShortDateString() + " - " + FechaFin.ToShortDateString();
                ViewBag.nombre = usu.Username;
            }
                return View();
        }
        [HttpGet]
        public ActionResult ReporteDispositivo()
        {
            ViewBag.consumo = "";
            List<SelectListItem> dispositivosSelectList = DispositivosTotales.GetDispositivos();//se carga lista de dispositivos
            ViewBag.IdSeleccionado = dispositivosSelectList;

            return View();
        }
        [HttpPost]
        public ActionResult ReporteDispositivo(SubmitViewModel model, DateTime FechaInicio, DateTime FechaFin)
        {

            using (var db = new DBContext())
            {
                DispositivoEstatico disp = db.DispEstaticos.FirstOrDefault(d=>d.DispositivoID==model.IdSeleccionado);
                Reporte reporteModelo = new Reporte("Dispositivo", disp.DispositivoID.ToString(), 0, FechaInicio, FechaFin);

                List<SelectListItem> dispositivosSelectList = DispositivosTotales.GetDispositivos();//se carga lista de dispositivos
                ViewBag.IdSeleccionado = dispositivosSelectList;

                //if(reporte esta en mongo){find} else{ se crea y se guarda en mongo}
                var reportesEncontrados = Mongo.getReporte("Dispositivo", disp.DispositivoID, FechaInicio, FechaFin);
                if (reportesEncontrados.Count > 0)
                {
                    var reporte = reportesEncontrados[0];
                    ViewBag.consumo = "Consumo mongo: " + reporte.consumo.ToString() + "Kw";
                }
                else
                {
                    reporteModelo.consumo = DispositivosTotales.kwPorDispositivo(disp.DispositivoID);
                    ViewBag.consumo = "Consumo: " + reporteModelo.consumo + "Kw";
                    Mongo.insertarReporte(reporteModelo);
                }
                ViewBag.fechas = FechaInicio.ToShortDateString() + " - " + FechaFin.ToShortDateString();
                ViewBag.nombre = disp.Nombre;
            }
            return View();
        }
        [HttpGet]
        public ActionResult ReporteTransformador()
        {
            ViewBag.consumo = "";
            IEnumerable<Transformador> transformadores = (IEnumerable<Transformador>)TransformadoresImp.transformadoresEnDb();//se carga lista de dispositivos
            List<SelectListItem> dispositivosSelectList = new List<SelectListItem>();
            foreach (Transformador t in transformadores)
            {
                dispositivosSelectList.Add(new SelectListItem() { Value = t.TransformadorID.ToString(), Text = t.TransformadorID.ToString() });
            }
            ViewBag.IdSeleccionado = dispositivosSelectList;

            return View();
        }
        [HttpPost]
        public ActionResult ReporteTransformador(SubmitViewModel model, DateTime FechaInicio, DateTime FechaFin)
        {

            using (var db = new DBContext())
            {
                Transformador trans = db.Transformadores.FirstOrDefault(t => t.TransformadorID == model.IdSeleccionado);
                Reporte reporteModelo = new Reporte("Transformador", trans.TransformadorID.ToString(), 0, FechaInicio, FechaFin);

                IEnumerable<Transformador> transformadores = (IEnumerable<Transformador>)TransformadoresImp.transformadoresEnDb();//se carga lista de dispositivos
                List<SelectListItem> dispositivosSelectList = new List<SelectListItem>();
                foreach (Transformador t in transformadores)
                {
                    dispositivosSelectList.Add(new SelectListItem() { Value = t.TransformadorID.ToString(), Text = t.TransformadorID.ToString() });
                }
                ViewBag.IdSeleccionado = dispositivosSelectList;

                //if(reporte esta en mongo){find} else{ se crea y se guarda en mongo}
                var reportesEncontrados = Mongo.getReporte("Transformador", trans.TransformadorID, FechaInicio, FechaFin);
                if (reportesEncontrados.ToList<Reporte>().Count > 0)
                {
                    var reporte = reportesEncontrados.ToList<Reporte>()[0];
                    ViewBag.consumo = "Consumo mongo: " + reporte.consumo.ToString() + "Kw";
                }
                else
                {
                    reporteModelo.consumo = trans.EnergiaQueEstaSuministrando(FechaInicio, FechaFin);
                    ViewBag.consumo = "Consumo: " + reporteModelo.consumo.ToString() + "Kw";
                    Mongo.insertarReporte(reporteModelo);
                }
                ViewBag.fechas = FechaInicio.ToShortDateString() + " - " + FechaFin.ToShortDateString();
                ViewBag.nombre = trans.TransformadorID.ToString();
            }
            return View();
        }
        public ActionResult AdministrarDispositivosAdmin()
        {
            DispositivosTotales.LlenarDisps();
            IEnumerable<DispositivoEstatico> disps = DispositivosTotales.opcionesDeDispositivos;

            return View(disps);
        }

        [HttpGet]
        public ActionResult ModificarDispositivoAdmin(int id)
        {
            DispositivoEstatico dispositivoEditado;
            using (var db = new DBContext()) {
                dispositivoEditado = db.DispEstaticos.Find(id);
            }
            return View(dispositivoEditado);
        }
        [HttpPost]
        public ActionResult GuardarCambioDispositivoAdmin(int id)
        {
            using (var db = new DBContext())
            {
                var dispositivoEditado = db.DispEstaticos.Find(id);
                if(TryUpdateModel(dispositivoEditado, "",new string[] { "Nombre","EsInteligente","Codigo","Min","Max","kWxHora"}))
                {
                    try
                    {
                        db.SaveChanges();
                        return RedirectToAction("AdministrarDispositivosAdmin", "Home");
                    }
                    catch (RetryLimitExceededException)
                    {
                        ModelState.AddModelError("", "No se pudieron guardar los cambios");
                    }
                }
            }
            return RedirectToAction("AdministrarDispositivosAdmin", "Home");
        }

        [HttpGet]
        public ActionResult EliminarDispositivoAdmin(int id)
        {
            using (var db = new DBContext())
            {
                var disp = db.DispEstaticos.FirstOrDefault(d => d.DispositivoID == id);
                db.DispEstaticos.Remove(disp);
                db.SaveChanges();
            }
            return RedirectToAction("AdministrarDispositivosAdmin", "Home");
        }

        [HttpGet]
        public ActionResult AgregarDispositivoAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AgregarDispositivoAdmin([Bind(Include = "Codigo,Nombre,kWxHora,Min,Max,EsInteligente")] DispositivoEstatico nuevo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    nuevo.AgregarALaBase();
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "No se pudo Crear");
            }
            return RedirectToAction("AdministrarDispositivosAdmin", "Home");
        }

        public ActionResult AdministrarCasasAdmin()
        {
            IEnumerable<Cliente> casas = ClientesImportados.GetClientes();
            foreach (Cliente c in casas)
                c.CargarDisps();

            return View(casas);
        }

        public ActionResult DetallesCliente(int id)
        {
            Cliente c = new Cliente(id);
            c.CargarDisps();
            ViewBag.Dispositivos = c.Dispositivos;
            return View(c);
        }
        public ActionResult Encender3(int id, string estadoActual, int Uid)
        {
            if (estadoActual != "Encendido")
            {
                DispositivoInteligente DI = new DispositivoInteligente(id);
                DI.Encender();
            }
            return RedirectToAction("DetallesCliente", "Home", new { id = Uid });
        }
        public ActionResult Apagar3(int id, string estadoActual, int Uid)
        {
            if (estadoActual != "Apagado")
            {
                DispositivoInteligente DI = new DispositivoInteligente(id);
                DI.Apagar();
            }
            return RedirectToAction("DetallesCliente", "Home", new { id = Uid });
        }
        public ActionResult Ahorro3(int id, string estadoActual, int Uid)
        {
            if (estadoActual != "Ahorro")
            {
                DispositivoInteligente DI = new DispositivoInteligente(id);
                DI.AhorrarEnergia();
            }
            return RedirectToAction("DetallesCliente", "Home", new { id = Uid });
        }

        [HttpGet]
        public ActionResult ConsultarConsumoAdmin(int id)
        {//Metodo para consultar el consumo en un periodo
            ViewBag.consumo = "";
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult ConsultarConsumoAdmin(DateTime FechaInicio, DateTime FechaFin, int id)
        {
            Cliente clie = new Cliente(id);
            clie.CargarDisps();


            using (var db = new DBContext())
            {
                Usuario usu = db.Usuarios.FirstOrDefault(u => u.UsuarioID == clie.UsuarioID);
                Reporte reporteModelo = new Reporte("Hogar", usu.UsuarioID.ToString(), 0, FechaInicio, FechaFin);

                //if(reporte esta en mongo){find} else{ se crea y se guarda en mongo}

                var reportesEncontrados = Mongo.getReporte("Hogar", usu.UsuarioID, FechaInicio, FechaFin);
                if (reportesEncontrados.Count > 0)
                {
                    var reporte = reportesEncontrados[0];
                    ViewBag.consumo = "Consumo mongo: " + reporte.consumo.ToString() + "Kw";
                }
                else
                {
                    reporteModelo.consumo = usu.KwTotales(FechaInicio, FechaFin);
                    ViewBag.consumo = "Consumo: " + reporteModelo.consumo + "Kw";
                    Mongo.insertarReporte(reporteModelo);
                }
                ViewBag.fechas = FechaInicio.ToShortDateString() + " - " + FechaFin.ToShortDateString();
                ViewBag.nombre = usu.Username;
            }

            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public ActionResult SimplexView(int id)
        {
            Cliente clie = new Cliente(id);
            clie.CargarDisps();

            ViewBag.simplexResultado = clie.SolicitarRecomendacion();
            return View();
        }



        //VISTAS DE CLIENTE

        [HttpGet]
        public ActionResult DispositivosPropios()
        {
            ViewBag.Message = "Tus dispositivos:";

            Cliente clie = new Cliente(User.Identity.Name);
            clie.CargarDisps();
            return View(clie);
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
        public ActionResult CrearRegla(int id)
        {
            ViewBag.id = id;
            ViewBag.Tipo = SensoresEstaticos.GetTipos();
            ViewBag.Descripcion = SensoresEstaticos.GetDescripcion();
            return View();
        }
        [HttpPost]
        public ActionResult CrearRegla([Bind(Include = "ValorMax,ValorMin,Tipo,Descripcion")] Regla reglaNueva, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DispositivoInteligente di = new DispositivoInteligente(id);
                    di.CargarActuador();
                    di.AgregarRegla(reglaNueva);
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "No se pudo Crear");
            }
            return RedirectToAction("DispositivosPropios", "Home");
        }
        public ActionResult BorrarRegla(int rID, int dID)
        {
            using (var db = new DBContext())
            {
                var r = db.Reglas.FirstOrDefault(re => re.ReglaID == rID);
                db.Reglas.Remove(r);
                db.SaveChanges();
            }
            return RedirectToAction("DetallesInteligente", "Home", new { id = dID });
        }

        [HttpGet]
        public ActionResult AgregarDispositivoClie()
        {   //se llenan la lista de todas las opciones de dispositivos para poder agregarlos a los propios del usuario
            List<SelectListItem> disps = DispositivosTotales.GetDispositivos();
            if (disps.Count == 0)
                ViewBag.Message = "No hay dispositivos para agregar actualmente";
            else
                ViewBag.Message = "Seleccione un dispositivo de la lista para agregar";

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
            
            using (var db = new DBContext())
            {
                Usuario usu = db.Usuarios.FirstOrDefault(u => u.UsuarioID == clie.UsuarioID);
                Reporte reporteModelo = new Reporte("Hogar", usu.UsuarioID.ToString(), 0, FechaInicio, FechaFin);

                //if(reporte esta en mongo){find} else{ se crea y se guarda en mongo}

                var reportesEncontrados = Mongo.getReporte("Hogar", usu.UsuarioID, FechaInicio, FechaFin);
                if (reportesEncontrados.Count > 0)
                {
                    var reporte = reportesEncontrados[0];
                    ViewBag.consumo = "Consumo mongo: " + reporte.consumo.ToString() + "Kw";
                }
                else
                {
                    reporteModelo.consumo = usu.KwTotales(FechaInicio, FechaFin);
                    ViewBag.consumo = "Consumo: " + reporteModelo.consumo + "Kw";
                    Mongo.insertarReporte(reporteModelo);
                }
                ViewBag.fechas = FechaInicio.ToShortDateString() + " - " + FechaFin.ToShortDateString();
                ViewBag.nombre = usu.Username;
            }
            return View();
        }

        [HttpGet]
        public ActionResult SimplexView()
        {
            if (User.Identity.IsAuthenticated || ClientesImportados.GetClientes() != null)
            {
                Cliente clie = new Cliente(User.Identity.Name);
                clie.CargarDisps();
                ViewBag.recomendaciones = clie.SolicitarRecomendacion().ToList();
                return View();
            }
            ViewBag.simplexResultado = "Hubo un error al ejecutar el simplex";
            return View();
        }
    }
}
