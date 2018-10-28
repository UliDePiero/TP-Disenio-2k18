using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP0;
using TP0.Helpers;
using TP0.Helpers.ORM;
using System.Linq;
using System.Web;

namespace TestsNuevos
{
    [TestClass]
    public class AdapterTest
    {
        [TestMethod]
        public void ConvertirDEaDIyCambioState_test()
        {
            using (var db = new DBContext())
            { 

                //Arrenge
                var cliente = new Cliente("Luciano", "Panizza", "Medrano951", "ConvertirDEaDIyCambioState", "asdqwe123", "12345678", "dni", "12345678");
                //cliente.TransformadorID = 1; //necesita un id si o si
                cliente.TransformadorID = 4;
                db.Usuarios.Add(cliente);
                db.SaveChanges();

                var cliente1 = new Cliente(cliente.Username);

                var tvsamsung = new DispositivoEstandar("televisor LCD de 40 pulgadas", "0014", 0.18, 0, 360, 90);
                tvsamsung.UsuarioID = cliente1.UsuarioID;
                db.Dispositivos.Add(tvsamsung);
                db.SaveChanges();

                var dispEst1 = new DispositivoEstandar(tvsamsung.DispositivoID);

                cliente1.AdaptarDispositivo(dispEst1, "Samsung");

                var DBdispInt1 = db.Dispositivos.First(x => x.UsuarioID == cliente1.UsuarioID && x.Codigo == "0014" && x.EsInteligente == true);
                var dispInt1 = new DispositivoInteligente(DBdispInt1.DispositivoID);
                
                //Act

                dispInt1.Encender();

                var ultimoEstado = db.Estados.Find(dispInt1.IDUltimoEstado);

                //Assert
                Assert.AreEqual(true, DBdispInt1.EsInteligente);
                Assert.AreEqual("Encendido", ultimoEstado.Desc);
            }
        }
    }
}
