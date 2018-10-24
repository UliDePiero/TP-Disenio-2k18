using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP0.Helpers;
using TP0.Helpers.ORM;
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
            cliente.TransformadorID = 1; //necesita un id si o si
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
    }
}
