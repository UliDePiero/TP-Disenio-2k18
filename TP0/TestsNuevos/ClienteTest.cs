﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP0.Helpers;
using TP0.Helpers.ORM;
using TP0.Helpers.Simplex;
using System.Linq;
using System.Web;

namespace TestsNuevos
{
    [TestClass]
    public class ClienteTest
    {
        [TestMethod]
        public void CantidadPuntosUsuario15_test()
        {

            //Arrenge
            using (var db = new DBContext())
            {
                var cliente = new Cliente("Luciano", "Panizza", "Medrano951", "CantidadPuntosUsuario15_test", "asdqwe123", "12345678", "dni", "12345678");
                //cliente.TransformadorID = 1; //necesita un id si o si
                db.Usuarios.Add(cliente);
                db.SaveChanges();

                var lamparaHalogena40W = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
                lamparaHalogena40W.UsuarioID = cliente.UsuarioID;
                var lamparaHalogena40W2 = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
                lamparaHalogena40W2.UsuarioID = cliente.UsuarioID;


                //Act
                cliente.AgregarDispInteligente(lamparaHalogena40W);
                cliente.AgregarDispInteligente(lamparaHalogena40W2);

                var clDB = new Cliente("CantidadPuntosUsuario15_test");

                //Assert
                Assert.AreEqual(30, clDB.PuntosAcum);
                Assert.AreEqual(30, cliente.PuntosAcum);
            }
        }
        [TestMethod]
        public void RecomendacionesDelCliente()
        {
            using (var db = new DBContext())
            {
                //Arrenge

                var cliente = new Cliente("Luciano", "Panizza", "Medrano951", "RecomendacionesDelCliente_test", "asdqwe123", "12345678", "dni", "12345678");
                //cliente.TransformadorID = 1; //necesita un id si o si
                db.Usuarios.Add(cliente);
                db.SaveChanges();

                var Lampara60W = new DispositivoInteligente("lampara halogena de 60 W", "0011", 0.06, 360, 90);
                cliente.AgregarDispInteligente(Lampara60W);

                var microondas = new DispositivoEstandar("microondas convencional", "0011", 0.64, 0, 15, 3);
                cliente.AgregarDispEstandar(microondas);

                var televisor40 = new DispositivoEstandar("televisor LCD de 40 pulgadas", "0014", 0.18, 0, 360, 90);
                cliente.AgregarDispEstandar(televisor40);

                var lavarropa = new DispositivoEstandar("lavarropas automatico de 5kg con calentamiento", "0021", 0.875, 0, 30, 6);
                cliente.AgregarDispEstandar(lavarropa);

                //Act
                var RecomendacionXDispositivos = cliente.SolicitarRecomendacion();

                //Assert

                //Assert.AreEqual("Valor independiente", RecomendacionXDispositivos[0].NombreDispositivo);
                Assert.AreEqual("lampara halogena de 60 W", RecomendacionXDispositivos[1].NombreDispositivo);
                Assert.AreEqual("microondas convencional", RecomendacionXDispositivos[2].NombreDispositivo);
                Assert.AreEqual("televisor LCD de 40 pulgadas", RecomendacionXDispositivos[3].NombreDispositivo);
                Assert.AreEqual("lavarropas automatico de 5kg con calentamiento", RecomendacionXDispositivos[4].NombreDispositivo);

                //Assert.AreEqual(765, RecomendacionXDispositivos[0].KWxHoraPuedeConsumir);
                Assert.AreEqual(15, RecomendacionXDispositivos[1].KWxHoraPuedeConsumir);
                Assert.AreEqual(360, RecomendacionXDispositivos[2].KWxHoraPuedeConsumir);
                Assert.AreEqual(30, RecomendacionXDispositivos[3].KWxHoraPuedeConsumir);
                Assert.AreEqual(360, RecomendacionXDispositivos[4].KWxHoraPuedeConsumir);
                
            }
        }
    }
}
