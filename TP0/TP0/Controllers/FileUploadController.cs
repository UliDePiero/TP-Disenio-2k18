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
    public class FileUploadController : Controller
    {
        string path;
        public List<Cliente> userList;
        // GET: FileUpload

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
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
                        //se agarra el texto del archivo y se lo convierte a una lista de Cliente, Transformadores o Dispositivos
                        string Json = System.IO.File.ReadAllText(path);
                        switch (Path.GetFileName(file.FileName))
                        {
                            case "usuarios.json":
                                List<Cliente> userList = JsonConvert.DeserializeObject<List<Cliente>>(Json);
                                Helpers.Static.ClientesImportados.clientes = userList;
                                break;
                            case "transformadores.json":
                                List<Transformador> transformadoresActivos = JsonConvert.DeserializeObject<List<Transformador>>(Json);
                                Helpers.Static.TransformadoresImp.transformadores = transformadoresActivos;
                                break;
                            case "dispositivosEstandares.json":
                                List<DispositivoEstandar> dispositivosEstandares = JsonConvert.DeserializeObject<List<DispositivoEstandar>>(Json);
                                break;
                            case "dispositivosInteligentes.json":
                                List<DispositivoInteligente> dispositivosInteligentes = JsonConvert.DeserializeObject<List<DispositivoInteligente>>(Json);
                                break;
                            default:
                                break;
                        }
                        ViewBag.FileStatus = "Archivo cargado correctamente.";
                    }
                    catch(Exception)
                    {
                        ViewBag.FileStatus = "El archivo no es del formato correcto.";
                    }
                }
                catch (Exception)
                {

                    ViewBag.FileStatus = "Error cargando archivo.";
                }

            }
            return View("Index");
        }
    }

}
