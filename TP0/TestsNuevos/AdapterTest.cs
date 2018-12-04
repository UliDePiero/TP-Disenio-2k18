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
                cliente.AgregarALaBase();

                var tvsamsung = new DispositivoEstandar("televisor LCD de 40 pulgadas", "0014", 0.18, 0, 360, 90);
                cliente.AgregarDispEstandar(tvsamsung);

                cliente.AdaptarDispositivo(tvsamsung, "HP");

                var DBdispInt1 = db.Dispositivos.First(x => x.UsuarioID == cliente.UsuarioID && x.Codigo == "0014" && x.EsInteligente == true);

                //Act
                var adaptadorSamsung = new AdaptadorSamsung(DBdispInt1.DispositivoID);
                adaptadorSamsung.Encender();

                var ultimoEstado = db.Estados.Find(adaptadorSamsung.IDUltimoEstado);

                //Assert
                Assert.AreEqual(true, DBdispInt1.EsInteligente);
                Assert.AreEqual("Encendido", ultimoEstado.Desc);
            }
        }
    }
}
