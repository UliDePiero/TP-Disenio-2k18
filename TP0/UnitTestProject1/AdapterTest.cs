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
            using (var db = new DBContext())
            { 

                //Arrenge
                var cliente = new Cliente("Luciano", "Panizza", "Medrano951", "PaniAdapt", "asdqwe123", "12345678", "dni", "12345678");
                cliente.TransformadorID = 2; //necesita un id si o si
                db.Usuarios.Add(cliente);

                db.SaveChanges();

                var DBusr1 = db.Usuarios.Find(cliente.UsuarioID);
                var cliente1 = new Cliente(DBusr1.Nombre, DBusr1.Apellido, DBusr1.Domicilio, DBusr1.Username, DBusr1.Contrasenia, DBusr1.Documento, DBusr1.TipoDocumento, DBusr1.Telefono);
                cliente1.UsuarioID = DBusr1.UsuarioID;    

                var tvsamsung = new DispositivoEstandar("televisor LCD de 40 pulgadas", "0014", 0.18, 0, 360, 90);
                tvsamsung.UsuarioID = DBusr1.UsuarioID;
                db.Dispositivos.Add(tvsamsung);
                db.SaveChanges();

                var DBdispEst1 = db.Dispositivos.First(x => x.UsuarioID == cliente1.UsuarioID && x.Codigo == "0014" && x.EsInteligente == false);
                var dispEst1 = new DispositivoEstandar(DBdispEst1.Nombre, DBdispEst1.Codigo, DBdispEst1.KWxHora, DBdispEst1.HorasXDia, DBdispEst1.Max, DBdispEst1.Min);
                dispEst1.UsuarioID = DBdispEst1.UsuarioID;
                dispEst1.DispositivoID = DBdispEst1.DispositivoID;

                cliente1.AdaptarDispositivo(dispEst1, "Samsung");

                var DBdispInt1 = db.Dispositivos.First(x => x.UsuarioID == cliente1.UsuarioID && x.Codigo == "0014" && x.EsInteligente == true);
                var dispInt1 = new DispositivoInteligente(DBdispInt1.Nombre, DBdispInt1.Codigo, DBdispInt1.KWxHora, DBdispInt1.Max, DBdispInt1.Min);
                dispInt1.UsuarioID = DBdispInt1.UsuarioID;
                dispInt1.DispositivoID = DBdispInt1.DispositivoID;
                dispInt1.IDUltimoEstado = DBdispInt1.IDUltimoEstado;
                
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
