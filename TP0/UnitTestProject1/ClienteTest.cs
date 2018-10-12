using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP0.Helpers;
using TP0.Helpers.ORM;

namespace UnitTestProject1
{
    [TestClass]
    public class ClienteTest
    {
        [TestMethod]
        public void CantidadPuntosUsuario15_test()
        {

            //Arrenge
            var db = DBContext.Instancia();
            var cliente = new Cliente("Luciano", "Panizza", "Medrano951", "paniaton", "asdqwe123", "12345678", "dni", "12345678");
            cliente.TransformadorID = 4; //necesita un id si o si
            db.Usuarios.Add(cliente);
            db.SaveChanges();

            var lamparaHalogena40W = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
            cliente.puntos = 0;
            
            //Act
            cliente.AgregarDispInteligente(lamparaHalogena40W);

            //Assert
            Assert.AreEqual(15, cliente.puntos);
        }
    }
}
