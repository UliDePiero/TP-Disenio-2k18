using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP0.Helpers;
using TP0.Helpers.Simplex;
using TP0.Helpers.ORM;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace TestsNuevos
{
    [TestClass]
    public class TrafoTest
    {

        [TestMethod]
        public void ConectarTrafoProx_Test()
        {
            using (var db = new DBContext())
            { 
                //Arrenge
                
                var cliente = new Cliente("Luciano", "Panizza", "Medrano 951", "Distancia_test", "asdqwe123", "12345678", "dni", "12345678");
                db.Usuarios.Add(cliente);
                db.SaveChanges();

                //Act

                var trafoProx = db.Transformadores.Find(cliente.TransformadorID);

                //Assert
                Assert.AreEqual(2, trafoProx.TransformadorID);
            }
        }

    }
}
