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
    public class APITest
    {
        [TestMethod]
        public void WebService_Test()
        {
            using (var db = new DBContext())
            { 
                //Arrenge
                var recomendacion = Recomendacion.Instancia();
                var simplex = SimplexHelper.Instancia();

                var cliente = new Cliente("Luciano", "Panizza", "Medrano951", "WebService_test", "asdqwe123", "12345678", "dni", "12345678");
                //cliente.TransformadorID = 1; //necesita un id si o si
                db.Usuarios.Add(cliente);
                db.SaveChanges();

                var Lampara60W = new DispositivoInteligente("lampara halogena de 60 W", "0011", 0.06, 360, 90);
                Lampara60W.UsuarioID = cliente.UsuarioID;
                db.Dispositivos.Add(Lampara60W);
                db.SaveChanges();

                var microondas = new DispositivoEstandar("microondas convencional", "0011", 0.64, 0, 15, 3);
                microondas.UsuarioID = cliente.UsuarioID;
                db.Dispositivos.Add(microondas);
                db.SaveChanges();

                var televisor40 = new DispositivoEstandar("televisor LCD de 40 pulgadas", "0014", 0.18, 0, 360, 90);
                televisor40.UsuarioID = cliente.UsuarioID;
                db.Dispositivos.Add(televisor40);
                db.SaveChanges();

                var lavarropa = new DispositivoEstandar("lavarropas automatico de 5kg con calentamiento", "0021", 0.875, 0, 30, 6);
                lavarropa.UsuarioID = cliente.UsuarioID;
                db.Dispositivos.Add(lavarropa);
                db.SaveChanges();

                //Act
                var resultado = recomendacion.GenerarRecomendacion(cliente);

                //Assert
                Assert.AreEqual("[765.0, 15.0, 360.0, 30.0, 360.0]", resultado);
            }
        }

        [TestMethod]
        public void GeoLocalizacionUser_Test()
        {
            using (var db = new DBContext())
            {
                //Arrenge
                var cliente = new Cliente("Luciano", "Panizza", "Medrano 951", "GeoLocalizacionUser_Test", "asdqwe123", "12345678", "dni", "12345678");
                db.Usuarios.Add(cliente);
                db.SaveChanges();

                var clienteDB = new Cliente("GeoLocalizacionUser_Test");

                //Act
                var punto = clienteDB.UbicacionDomicilio();

                //Assert
                Assert.AreEqual(-34.5985998, punto[0]);
                Assert.AreEqual(-58.4199217, punto[1]);
            }
        }
    }
}
