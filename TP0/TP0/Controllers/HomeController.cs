using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
//using System.Web.Script.Serialization; // for serialize and deserialize
using TP0.Helpers;

namespace TP0.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            
            if (file != null && file.ContentLength > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string f = Server.MapPath(fileName);
                string Json = System.IO.File.ReadAllText(f);
                List<Cliente> userList = JsonConvert.DeserializeObject<List<Cliente>>(Json);
                //return View(userList);
            }
            // redirect back to the index action to show the form once again
                return RedirectToAction("Index");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}