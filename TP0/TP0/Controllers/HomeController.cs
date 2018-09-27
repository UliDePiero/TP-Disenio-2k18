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
        Cliente clienteActual;
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
        public ActionResult DetallesDeUsuario(SubmitViewModel model)
        {
            string codigo = model.DispositivoSeleccionado;

            //Agrega el nuevo dispositivo al usuario
            using (var db = new DBContext())
            {
                foreach (Usuario user in db.Usuarios)
                {
                    if (user.Username == User.Identity.GetUserName())
                    {
                        if (EsInteligente(codigo))
                        {
                            DispositivoInteligente disp = EncontrarDispositivoInteligente(codigo);
                            disp.UsuarioID = user.UsuarioID;
                            db.Dispositivos.Add(disp);
                            break;
                        }
                        else
                        {
                            DispositivoEstandar disp = EncontrarDispositivoEstandard(codigo);
                            disp.UsuarioID = user.UsuarioID;
                            db.Dispositivos.Add(disp);
                            break;
                        }
                    }
                }
                db.SaveChanges();
            }

            return RedirectToAction("DetallesDeUsuario", "Home");
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

        public ActionResult AdministrarDispositivos()
        {
            ViewBag.Message = "Your AdministrarDispositivos page.";
            return View();
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

        public ActionResult mostrarDispositivosTotales(string mensaje)
        {
            string username = User.Identity.GetUserName();
            int resu = 0;

            //Trae los dispositivos del cliente y los cuenta
            using (var db = new DBContext())
            {
                Usuario user = db.Usuarios.FirstOrDefault(u => u.Username == username);
                resu = db.Dispositivos.Where(d => d.UsuarioID == user.UsuarioID).ToList().Count();
            }
            //clienteActual = Helpers.Static.ClientesImportados.filtrarCliente(idUsuario);
            //string json = Helpers.Static.Simplex.SimplexHelper.generarJson(clienteActual.dispositivosEstandares, clienteActual.dispositivosInteligentes);
            ViewBag.estadoSimplex = resu;

            return RedirectToAction("Simplex2", "Home", new { mensaje = "Dispositivos en total: " + resu });
            
        }
        public ActionResult mostrarDispositivosEncendidos(string mensaje)
        {
            string idUsuario = User.Identity.GetUserName();
            clienteActual = Helpers.Static.ClientesImportados.filtrarCliente(idUsuario);
            int resu = clienteActual.DispositivosEncendidos();

            return RedirectToAction("Simplex2", "Home", new { mensaje = "Dispositivos Inteligentes encendidos: " + resu });
        }
        public ActionResult mostrarDispositivosApagados(string mensaje)
        {
            string idUsuario = User.Identity.GetUserName();
            clienteActual = Helpers.Static.ClientesImportados.filtrarCliente(idUsuario);
            int resu = clienteActual.DispositivosApagados(); ;

            return RedirectToAction("Simplex2", "Home", new { mensaje = "Dispositivos Inteligentes apagados: " + resu });
        }

    }
}
