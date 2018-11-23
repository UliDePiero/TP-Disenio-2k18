using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using TP0.Helpers;
using TP0.Helpers.ORM;
using TP0.Helpers.Static;
using TP0.Models;

namespace TP0.Controllers
{
    public class FileUploadController : Controller
    {
        string path;
        public List<Cliente> userList;
        // GET: FileUpload

        //Pagina del admin
        public ActionResult Index()
        {
            List<SelectListItem> opciones = new List<SelectListItem>();
            opciones.Add(new SelectListItem() { Value = "clientes", Text = "clientes" });
            opciones.Add(new SelectListItem() { Value = "transformadores", Text = "transformadores" });
            opciones.Add(new SelectListItem() { Value = "dispositivos", Text = "dispositivos" });
            ViewBag.DispositivoSeleccionado = opciones;
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, SubmitViewModel model)
        {
            List<SelectListItem> opciones = new List<SelectListItem>();
            opciones.Add(new SelectListItem() { Value = "clientes", Text = "clientes" });
            opciones.Add(new SelectListItem() { Value = "transformadores", Text = "transformadores" });
            opciones.Add(new SelectListItem() { Value = "dispositivos", Text = "dispositivos" });
            ViewBag.DispositivoSeleccionado = opciones;
            string tipo = model.DispositivoSeleccionado;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    //se abre el buscador de archivos y se agarra el path del archivo seleccionado
                    path = Path.Combine(Server.MapPath("~/App_Data/uploads"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                }
                try
                {
                    //se agarra el texto del archivo y se lo convierte a una lista de Cliente, Transformadores o Dispositivos
                    string Json = System.IO.File.ReadAllText(path);
                    switch (tipo)
                    {
                        case "clientes":
                            List<Cliente> userList = JsonConvert.DeserializeObject<List<Cliente>>(Json);
                            ClientesImportados.clientes = userList;
                            break;
                        case "transformadores":
                            //Agarra el json y agrega los que no estan ya en la base
                            TransformadoresImp.CargarNuevosTransformadores(JsonConvert.DeserializeObject<List<Transformador>>(Json));
                            break;
                        case "dispositivos":
                            //Agarra el json y agrega los que no estan ya en la base
                            List<DispositivoEstatico> disps = JsonConvert.DeserializeObject<List<DispositivoEstatico>>(Json);
                            List<DispositivoEstatico> agregar = new List<DispositivoEstatico>();
                            foreach (DispositivoEstatico d in disps)
                                agregar.Add(new DispositivoEstatico(d.Codigo, d.Nombre, d.kWxHora, d.Min, d.Max, d.EsInteligente));
                            DispositivosTotales.AgregarDispEstaticos(disps);
                            break;

                        default:
                            break;
                    }
                    ViewBag.FileStatus = "Archivo cargado correctamente.";
                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "El archivo no es del formato correcto.";
                }
            }
            catch (Exception)
            {

                ViewBag.FileStatus = "Error cargando archivo.";
            }
            return View("Index");
        }

        //Pagina del usuario
        public ActionResult IndexClie()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IndexClie(HttpPostedFileBase file)
        {
            Cliente c = new Cliente(User.Identity.Name);
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        //se abre el buscador de archivos y se agarra el path del archivo seleccionado
                        path = Path.Combine(Server.MapPath("~/App_Data/uploads"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                    }
                    try
                    {
                        //se agarra el texto del archivo y se lo convierte a una lista de dispositivos del cliente
                        string Json = System.IO.File.ReadAllText(path);
                        switch (Path.GetFileName(file.FileName))
                        {
                            case "dispositivosPropios.json":
                                //Agarra el json y agrega los que no estan ya en la base
                                List<DispositivoEstatico> disps = JsonConvert.DeserializeObject<List<DispositivoEstatico>>(Json);
                                c.GetDisps();
                                c.AgregarDispJson(disps);                                
                                break;
                            default:
                                break;
                        }
                        ViewBag.FileStatus = "Archivo cargado correctamente.";
                    }
                    catch (Exception)
                    {
                        ViewBag.FileStatus = "El archivo no es del formato correcto.";
                    }
                }
                catch (Exception)
                {

                    ViewBag.FileStatus = "Error cargando archivo.";
                }

            }
            return View("IndexClie");
        }
    }

}
