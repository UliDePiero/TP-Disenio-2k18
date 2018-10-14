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
    public class ClienteTest
    {
        [TestMethod]
        public void CantidadPuntosUsuario15_test()
        {

            //Arrenge
            using (var db = new DBContext())
            { 
                var cliente = new Cliente("Luciano", "Panizza", "Medrano951", "paniaton", "asdqwe123", "12345678", "dni", "12345678");
            cliente.TransformadorID = 2; //necesita un id si o si
            db.Usuarios.Add(cliente);
            db.SaveChanges();

            var lamparaHalogena40W = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
            lamparaHalogena40W.UsuarioID = cliente.UsuarioID;

            cliente.AgregarDispInteligente(lamparaHalogena40W);

            db.Dispositivos.First(x => x.UsuarioID == cliente.UsuarioID && x.Codigo == "0011" && x.EsInteligente == true);

            cliente.puntos = 0;
            
            //Act
            cliente.AgregarDispInteligente(lamparaHalogena40W);

            //Assert
            Assert.AreEqual(15, cliente.puntos);
            }
        }
    }
}
