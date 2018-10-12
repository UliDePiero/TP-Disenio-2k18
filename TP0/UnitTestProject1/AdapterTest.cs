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
    public class AdapterTest
    {
        [TestMethod]
        public void ConvertirDEaDIyCambioState_test()
        {
            var db = DBContext.Instancia();
            //Arrenge

            var cliente = new Cliente("Luciano", "Panizza", "Medrano951", "PaniAdapt", "asdqwe123", "12345678", "dni", "12345678");
            cliente.TransformadorID = 1; //necesita un id si o si
            db.Usuarios.Add(cliente);

            db.SaveChanges();

            var usr1 = db.Usuarios.First(x => x.Username == "PaniAdapt");

            var tvsamsung = new DispositivoEstandar("televisor LCD de 40 pulgadas", "0014", 0.18, 0, 360, 90);
            tvsamsung.UsuarioID = usr1.UsuarioID;
            db.Dispositivos.Add(tvsamsung);
            db.SaveChanges();
            
            var dispEstandar = db.Dispositivos.First(x => x.UsuarioID == usr1.UsuarioID && x.Codigo == "0014" && x.EsInteligente == false);
            DispositivoEstandar dispEst = new DispositivoEstandar();

            //Act
            usr1.AdaptarDispositivo(dispEstandar, "Samsung");
            var dispInt = db.Dispositivos.First(x => x.UsuarioID == usr1.UsuarioID && x.Codigo == "0014" && x.EsInteligente==true);
            dispInt.AgregarEstado(new Apagado(dispInt));

            //dispInt.Encender();

            var ultimoEstado = db.Estados.First(e => e.StateID == dispInt.IDUltimoEstado); 

            //Assert
            Assert.AreEqual(true,dispInt.EsInteligente);
            Assert.AreEqual("Apagado", ultimoEstado.Desc);
        }
    }
}
