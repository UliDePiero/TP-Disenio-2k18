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
    public class ZonaTest
    {

        [TestMethod]
        public void ConsumoZona_Test()
        {
            using (var db = new DBContext())
            {
                Cliente cliente = new Cliente("Luciano", "Panizza", "Medrano951", "ConsumoZona_test", "asdqwe123", "12345678", "dni", "12345678");
                db.Usuarios.Add(cliente);
                db.SaveChanges();

                var Aire3500fg = new DispositivoInteligente("aire acondicionado de 3500 frigorias", "0011", 1.613, 360, 90);
                Aire3500fg.UsuarioID = cliente.UsuarioID;
                db.Dispositivos.Add(Aire3500fg);
                db.SaveChanges();

                var est1 = new Apagado(Aire3500fg) { FechaInicial = DateTime.Now.AddHours(-20), FechaFinal = DateTime.Now.AddHours(-15) };
                var est2 = new Encendido(Aire3500fg) { FechaInicial = DateTime.Now.AddHours(-15), FechaFinal = DateTime.Now.AddHours(-7) };
                var est3 = new Apagado(Aire3500fg) { FechaInicial = DateTime.Now.AddHours(-7), FechaFinal = DateTime.Now.AddHours(-2) };
                var est4 = new Encendido(Aire3500fg) { FechaInicial = DateTime.Now.AddHours(-2) };

                Aire3500fg.AgregarEstado(est1);
                Aire3500fg.AgregarEstado(est2);
                Aire3500fg.AgregarEstado(est3);
                Aire3500fg.AgregarEstado(est4);

                Cliente cliente2 = new Cliente("Luciano", "Panizza", "Pringles 853", "ConsumoZona2_test", "asdqwe123", "12345678", "dni", "12345678");
                db.Usuarios.Add(cliente2);
                db.SaveChanges();

                var Aire3500fg2 = new DispositivoInteligente("aire acondicionado de 3500 frigorias", "0011", 1.613, 360, 90);
                Aire3500fg2.UsuarioID = cliente2.UsuarioID;
                db.Dispositivos.Add(Aire3500fg2);
                db.SaveChanges();

                var est12 = new Apagado(Aire3500fg2) { FechaInicial = DateTime.Now.AddHours(-20), FechaFinal = DateTime.Now.AddHours(-15) };
                var est22 = new Encendido(Aire3500fg2) { FechaInicial = DateTime.Now.AddHours(-15), FechaFinal = DateTime.Now.AddHours(-7) };
                var est32 = new Apagado(Aire3500fg2) { FechaInicial = DateTime.Now.AddHours(-7), FechaFinal = DateTime.Now.AddHours(-2) };
                var est42 = new Encendido(Aire3500fg2) { FechaInicial = DateTime.Now.AddHours(-2) };

                Aire3500fg2.AgregarEstado(est12);
                Aire3500fg2.AgregarEstado(est22);
                Aire3500fg2.AgregarEstado(est32);
                Aire3500fg2.AgregarEstado(est42);

                //Act
                var zona = db.Zonas.Find(1);
                var zonadb = new Zona(zona.ZonaID, zona.Latitud, zona.Longitud, zona.Radio);

                //Assert
                Assert.AreEqual(Aire3500fg.KWxHora*5*2, zonadb.ConsumoTotal(DateTime.Now.AddHours(-10), DateTime.Now));
            }
        }

    }
}
