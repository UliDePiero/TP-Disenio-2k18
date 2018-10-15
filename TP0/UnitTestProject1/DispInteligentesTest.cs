using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP0.Helpers;
using TP0.Helpers.ORM;
using System.Linq;
using System.Web;

namespace UnitTestProject1
{
    [TestClass]
    public class DispInteligentesTest
    {
        [TestMethod]
        public void EstaApagado_test()
        {
            using (var db = new DBContext())
            { 
                Cliente cliente = new Cliente("Luciano", "Panizza", "Medrano951", "EstaApagado_test", "asdqwe123", "12345678", "dni", "12345678");
                cliente.TransformadorID = 1; //necesita un id si o si
                db.Usuarios.Add(cliente);
                db.SaveChanges();

                //Arrenge
                var lamparaHalogena40W = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
                lamparaHalogena40W.UsuarioID = cliente.UsuarioID;

                //Act
                cliente.AgregarDispInteligente(lamparaHalogena40W);

                var DI = new DispositivoInteligente(lamparaHalogena40W.DispositivoID);
                var ultEst = db.Estados.Find(DI.IDUltimoEstado);

                //Assert
                Assert.AreEqual("Apagado", ultEst.Desc);
                Assert.IsTrue(DI.EstaApagado());

            }
        }

        [TestMethod]
        public void EstaPrendido_test()
        {
            using (var db = new DBContext())
            {
                Cliente cliente = new Cliente("Luciano", "Panizza", "Medrano951", "EstaPrendido_test", "asdqwe123", "12345678", "dni", "12345678");
                cliente.TransformadorID = 1; //necesita un id si o si
                db.Usuarios.Add(cliente);
                db.SaveChanges();

                //Arrenge
                var lamparaHalogena40W = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
                lamparaHalogena40W.UsuarioID = cliente.UsuarioID;

                //Act
                cliente.AgregarDispInteligente(lamparaHalogena40W);
                lamparaHalogena40W.Encender();

                var DI = new DispositivoInteligente(lamparaHalogena40W.DispositivoID);
                var ultEst = db.Estados.Find(DI.IDUltimoEstado);

                //Assert
                Assert.AreEqual("Encendido", ultEst.Desc);
                Assert.IsTrue(DI.EstaEncendido());
            }
        }

        [TestMethod]
        public void DeAhorroAEncendido_test()
        {
            using (var db = new DBContext())
            {
                //Arrenge
                Cliente cliente = new Cliente("Luciano", "Panizza", "Medrano951", "DeAhorroAEncendido_test", "asdqwe123", "12345678", "dni", "12345678");
                cliente.TransformadorID = 1; //necesita un id si o si
                db.Usuarios.Add(cliente);
                db.SaveChanges();

                var lamparaHalogena40W = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
                lamparaHalogena40W.UsuarioID = cliente.UsuarioID;
                cliente.AgregarDispInteligente(lamparaHalogena40W);

                var DI = new DispositivoInteligente(lamparaHalogena40W.DispositivoID);

                //Act
                DI.AhorrarEnergia();
                DI.Encender();

                var ultEst = db.Estados.Find(DI.IDUltimoEstado);

                //Assert
                Assert.AreEqual("Encendido", ultEst.Desc);
                Assert.IsInstanceOfType(DI.Estado, typeof(Encendido));
            }


        }

        [TestMethod]
        public void ConsumoEnergiaEnUltimas10Hs_test()
        {
            using (var db = new DBContext())
            {

                //Arrenge
                Cliente cliente = new Cliente("Luciano", "Panizza", "Medrano951", "ConsumoEnergiaEnUltimas10Hs_test", "asdqwe123", "12345678", "dni", "12345678");
                cliente.TransformadorID = 1; //necesita un id si o si
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

                //Act
                var result = Aire3500fg.ConsumoEnHoras(10);
                var comp = Aire3500fg.KWxHora * 5; //5 horas

                //Assert
                Assert.AreEqual(comp, result);

            }
        }

        [TestMethod]
        public void ConsumoEnergiaEnPeriodo_test()
        {
            using (var db = new DBContext())
            {

                //Arrenge
                Cliente cliente = new Cliente("Luciano", "Panizza", "Medrano951", "ConsumoEnergiaEnPeriodo_test", "asdqwe123", "12345678", "dni", "12345678");
                cliente.TransformadorID = 1; //necesita un id si o si
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

                //Act
                var result = Aire3500fg.ConsumoEnPeriodo(DateTime.Now.AddHours(-10), DateTime.Now);
                var comp = Aire3500fg.KWxHora * 5; //5 horas

                //Assert
                Assert.AreEqual(comp, result);

            }
   
        }

        [TestMethod]
        public void CambioEstadoNoHaceNada_test()
        {
            //Arrenge
            using (var db = new DBContext())
            {
                var cliente = new Cliente("Luciano", "Panizza", "Medrano951", "CantidadPuntosUsuario15_test", "asdqwe123", "12345678", "dni", "12345678");
                cliente.TransformadorID = 1; //necesita un id si o si
                db.Usuarios.Add(cliente);
                db.SaveChanges();

                var lamparaHalogena40W = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
                lamparaHalogena40W.UsuarioID = cliente.UsuarioID;
                cliente.AgregarDispInteligente(lamparaHalogena40W);
                var idEstAnt = lamparaHalogena40W.IDUltimoEstado;
                //Act
                lamparaHalogena40W.Apagar();


                //Assert
                Assert.AreEqual(idEstAnt, lamparaHalogena40W.IDUltimoEstado);
            }
        }

    }
}
