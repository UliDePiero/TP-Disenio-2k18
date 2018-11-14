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
                var cliente2 = new Cliente("Luciano", "Panizza", "Lafinur 3400", "Distancia2_test", "asdqwe123", "12345678", "dni", "12345678");
                db.Usuarios.Add(cliente2);
                db.SaveChanges();

                //Act

                var trafoProx = db.Transformadores.Find(cliente.TransformadorID);
                var trafoProx2 = db.Transformadores.Find(cliente2.TransformadorID);

                //Assert
                Assert.AreEqual(20, trafoProx.TransformadorID);
                Assert.AreEqual(15, trafoProx2.TransformadorID);
            }
        }
        [TestMethod]
        public void AsignarZona_Test()
        {
            using (var db = new DBContext())
            {
                //Arrenge

                var t = new Transformador();
                t.Latitud = -34.575233814928914;
                t.Longitud = -58.41500966716404;
                var t2 = new Transformador();
                t2.Latitud = -34.58248917295323;
                t2.Longitud = -58.45194180211615;


                //Act

                t.asignarZona();
                t2.asignarZona();
                //db.Transformadores.Add(t);
                //db.SaveChanges();

                //Assert
                Assert.AreEqual(8, t2.ZonaID);
                Assert.AreEqual(1, t.ZonaID);
            }
        }
        [TestMethod]
        public void CalcDist_Test()
        {
            using (var db = new DBContext())
            {
                //Arrenge

                var t = new Transformador();
                t.Latitud = -34.575233814928914;
                t.Longitud = -58.41500966716404;

                var zona = new Zona();
                zona.Latitud = -34.571632362375716;
                zona.Longitud = -58.422944329525308;



                //Act

                var dist = t.CalcDistancia(zona);
                //db.Transformadores.Add(t);
                //db.SaveChanges();

                //Assert
                Assert.AreEqual(0.83, dist);
            }
        }

    }
}
