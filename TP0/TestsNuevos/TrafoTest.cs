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
                
                var cliente = new Cliente("Luciano", "Panizza", "Medrano 951", "ConectarTrafoPro", "asdqwe123", "12345678", "dni", "12345678");
                cliente.AgregarALaBase();

                var lamparaHalogena40W = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
                lamparaHalogena40W.UsuarioID = cliente.UsuarioID;
                cliente.AgregarDispInteligente(lamparaHalogena40W);
                lamparaHalogena40W.Encender();

                var cliente2 = new Cliente("Luciano", "Panizza", "Lafinur 3400", "ConectarTrafoPro2", "asdqwe123", "12345678", "dni", "12345678");
                cliente2.AgregarALaBase();

                var lamparaHalogena40W2 = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
                lamparaHalogena40W2.UsuarioID = cliente2.UsuarioID;
                cliente2.AgregarDispInteligente(lamparaHalogena40W2);
                lamparaHalogena40W2.Encender();

                var cliente3 = new Cliente("Luciano", "Panizza", "Av de mayo 130", "ConectarTrafoPro3", "asdqwe123", "12345678", "dni", "12345678");
                cliente3.AgregarALaBase();

                var lamparaHalogena40W3 = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
                lamparaHalogena40W3.UsuarioID = cliente3.UsuarioID;
                cliente3.AgregarDispInteligente(lamparaHalogena40W3);
                lamparaHalogena40W3.Encender();

                var cliente4 = new Cliente("Luciano", "Panizza", "Av rivadavia 8000", "ConectarTrafoPro4", "asdqwe123", "12345678", "dni", "12345678");
                cliente4.AgregarALaBase();

                var lamparaHalogena40W4 = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
                lamparaHalogena40W4.UsuarioID = cliente4.UsuarioID;
                cliente4.AgregarDispInteligente(lamparaHalogena40W4);
                lamparaHalogena40W4.Encender();




                //Act
                /*
                var trafoProx = db.Transformadores.Find(cliente.TransformadorID);
                var trafoProx2 = db.Transformadores.Find(cliente2.TransformadorID);

                //Assert
                Assert.AreEqual(cliente.TransformadorID, trafoProx.TransformadorID);
                Assert.AreEqual(cliente2.TransformadorID, trafoProx2.TransformadorID);
                */
            }
        }
        [TestMethod]
        public void ReConectarTrafoProx_Test()
        {
            using (var db = new DBContext())
            {
                //Arrenge

                var t = new Transformador();
                t.Latitud = -34.575233814928914;
                t.Longitud = -58.41500966716404;
                t.asignarZona();
                db.Transformadores.Add(t);

                var t2 = new Transformador();
                t2.Latitud = -34.58248917295323;
                t2.Longitud = -58.45194180211615;
                t2.asignarZona();
                db.Transformadores.Add(t2);

                var t3 = new Transformador();
                t3.Latitud = -34.58220308678515;
                t3.Longitud = -58.42022895812988;
                t3.asignarZona();
                db.Transformadores.Add(t3);

                var t4 = new Transformador();
                t4.Latitud = -34.56774230669942;
                t4.Longitud = -58.43784063029881;
                t4.asignarZona();
                db.Transformadores.Add(t4);

                db.SaveChanges();

            
            
                foreach (var cli in db.Usuarios)
                {
                    //cli.ConectarseAlTrafoMasProx();
                    if (!cli.EsAdmin)
                    {
                        cli.ConectarseAlTrafoMasProx(); 
                    }
                }
                db.SaveChanges();
            }

            //Act
            /*
            var trafoProx = db.Transformadores.Find(cliente.TransformadorID);
            var trafoProx2 = db.Transformadores.Find(cliente2.TransformadorID);

            //Assert
            Assert.AreEqual(cliente.TransformadorID, trafoProx.TransformadorID);
            Assert.AreEqual(cliente2.TransformadorID, trafoProx2.TransformadorID);
            */
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
