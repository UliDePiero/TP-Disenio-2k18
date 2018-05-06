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
                        path = Path.Combine(Server.MapPath("~/App_Data/uploads"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                    }
                    try
                    {

                        string Json = System.IO.File.ReadAllText(path);
                        List<Usuario> userList = JsonConvert.DeserializeObject<List<Usuario>>(Json);
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


    /* if (file != null && file.ContentLength > 0)
     {
         string fileName = Path.GetFileName(file.FileName);
         string f = Server.MapPath(fileName);
         string Json = System.IO.File.ReadAllText(f);
         List<Cliente> userList = JsonConvert.DeserializeObject<List<Cliente>>(Json);
         //return View(userList);
     }
     // redirect back to the index action to show the form once again
         return RedirectToAction("Index");
 */
}