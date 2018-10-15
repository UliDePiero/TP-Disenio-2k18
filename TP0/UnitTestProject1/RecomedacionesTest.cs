using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP0.Helpers;
using TP0.Helpers.Simplex;
using TP0.Helpers.ORM;
using System.Linq;
using System.Web;


namespace UnitTestProject1
{
    [TestClass]
    public class RecomedacionesTest
    {
        [TestMethod]
        public void AccionesAutomatica_Test()
        {
            
            using (var db = new DBContext())
            {
                //Arrenge
                var InstanciaRec = Recomendacion.Instancia();
                var cliente = new Cliente("Luciano", "Panizza", "Medrano951", "AccionesAutomaticaON_Test", "asdqwe123", "12345678", "dni", "12345678");
                cliente.TransformadorID = 1; //necesita un id si o si
                db.Usuarios.Add(cliente);
                db.SaveChanges();
                cliente.AccionAutomaticaON();

                var cliente2 = new Cliente("Luciano", "Panizza", "Medrano951", "AccionesAutomaticaOFF_Test", "asdqwe123", "12345678", "dni", "12345678");
                cliente2.TransformadorID = 1; //necesita un id si o si
                db.Usuarios.Add(cliente2);
                db.SaveChanges();

                var tvAire24LEd = new DispositivoInteligente("televisor LED de 24 pulgadas", "0015", 0.04, 365, 90);
                tvAire24LEd.UsuarioID = cliente.UsuarioID;
                db.Dispositivos.Add(tvAire24LEd);
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

                tvAire24LEd.AgregarEstado(new Apagado(tvAire24LEd) { FechaInicial = DateTime.Now.AddHours(-500), FechaFinal = DateTime.Now.AddHours(-370) });
                tvAire24LEd.AgregarEstado(new Encendido(tvAire24LEd) { FechaInicial = DateTime.Now.AddHours(-370) });

                var tvAire24LEd2 = new DispositivoInteligente("televisor LED de 24 pulgadas", "0015", 0.04, 365, 90);
                tvAire24LEd2.UsuarioID = cliente2.UsuarioID;
                db.Dispositivos.Add(tvAire24LEd2);
                db.SaveChanges();

                var microondas2 = new DispositivoEstandar("microondas convencional", "0011", 0.64, 0, 15, 3);
                microondas2.UsuarioID = cliente2.UsuarioID;
                db.Dispositivos.Add(microondas2);
                db.SaveChanges();

                var televisor402 = new DispositivoEstandar("televisor LCD de 40 pulgadas", "0014", 0.18, 0, 360, 90);
                televisor402.UsuarioID = cliente2.UsuarioID;
                db.Dispositivos.Add(televisor402);
                db.SaveChanges();

                var lavarropa2 = new DispositivoEstandar("lavarropas automatico de 5kg con calentamiento", "0021", 0.875, 0, 30, 6);
                lavarropa2.UsuarioID = cliente2.UsuarioID;
                db.Dispositivos.Add(lavarropa2);
                db.SaveChanges();

                tvAire24LEd2.AgregarEstado(new Apagado(tvAire24LEd2) { FechaInicial = DateTime.Now.AddHours(-500), FechaFinal = DateTime.Now.AddHours(-370) });
                tvAire24LEd2.AgregarEstado(new Encendido(tvAire24LEd2) { FechaInicial = DateTime.Now.AddHours(-370) });


                //Act

                InstanciaRec.EjecutarRecomendacion();

                var DI = new DispositivoInteligente(tvAire24LEd.DispositivoID);
                var ultEst = db.Estados.Find(DI.IDUltimoEstado);

                var DI2 = new DispositivoInteligente(tvAire24LEd2.DispositivoID);
                var ultEst2 = db.Estados.Find(DI2.IDUltimoEstado);


                //Assert
                //Assert.AreEqual("[770.0, 15.0, 360.0, 30.0, 365.0]", resultado);        
                Assert.AreEqual("Apagado", ultEst.Desc);
                Assert.IsTrue(DI.EstaApagado());
                Assert.AreEqual("Encendido", ultEst2.Desc);
                Assert.IsTrue(DI2.EstaEncendido());
            }

        }

    }
}
