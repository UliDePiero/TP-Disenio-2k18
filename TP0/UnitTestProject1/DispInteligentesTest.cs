using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP0.Helpers;

namespace UnitTestProject1
{
    [TestClass]
    public class DispInteligentesTest
    {
        [TestMethod]
        public void EstaApagado_test()
        {
            //Arrenge
            var lamparaHalogena40W = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);

            //Act
            var result = lamparaHalogena40W.estaApagado();

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EstaPrendido_test()
        {
            //Arrenge
            var lamparaHalogena40W = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
            lamparaHalogena40W.encender();

            //Act
            var result = lamparaHalogena40W.estaEncendido();

            //Assert
            Assert.IsTrue(result); 
        }

        [TestMethod]
        public void DeAhorroAEncendido_test()
        {
            //Arrenge
            var lamparaHalogena40W = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
            lamparaHalogena40W.ahorrarEnergia();

            //Act
            lamparaHalogena40W.encender();

            //Assert
            Assert.IsInstanceOfType(lamparaHalogena40W.Estado, typeof(Encendido));
        }

        [TestMethod]
        public void ConsumoEnergiaEnUltimas10Hs_test()
        {
            //Arrenge
            var lamparaHalogena40W = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
            lamparaHalogena40W.estadosAnteriores = new List<State> { new Apagado(lamparaHalogena40W ) { FechaInicial = DateTime.Now.AddHours(-20), FechaFinal = DateTime.Now.AddHours(-15) } , new Encendido(lamparaHalogena40W) { FechaInicial = DateTime.Now.AddHours(-15), FechaFinal = DateTime.Now.AddHours(-7) }, new Apagado(lamparaHalogena40W) { FechaInicial = DateTime.Now.AddHours(-7), FechaFinal = DateTime.Now.AddHours(-2) }, new Encendido(lamparaHalogena40W) { FechaInicial = DateTime.Now.AddHours(-2)} };
            
            //Act
            var result = lamparaHalogena40W.consumoEnHoras(10);
            var comp = lamparaHalogena40W.kWxHora * 5; //5 horas

            //Assert
            Assert.AreEqual(comp, result);
        }

        [TestMethod]
        public void ConsumoEnergiaEnPeriodo_test()
        {
            //Arrenge
            var lamparaHalogena40W = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
            lamparaHalogena40W.estadosAnteriores = new List<State> { new Apagado(lamparaHalogena40W) { FechaInicial = DateTime.Now.AddHours(-20), FechaFinal = DateTime.Now.AddHours(-15) }, new Encendido(lamparaHalogena40W) { FechaInicial = DateTime.Now.AddHours(-15), FechaFinal = DateTime.Now.AddHours(-7) }, new Apagado(lamparaHalogena40W) { FechaInicial = DateTime.Now.AddHours(-7), FechaFinal = DateTime.Now.AddHours(-2) }, new Encendido(lamparaHalogena40W) { FechaInicial = DateTime.Now.AddHours(-2) } };


            //Act
            var result = lamparaHalogena40W.consumoEnPeriodo(DateTime.Now.AddHours(-20),DateTime.Now.AddHours(-7));
            var comp = lamparaHalogena40W.kWxHora * 8; //8 horas
            //Assert
            Assert.AreEqual(comp, result);
        }

        [TestMethod]
        public void CambioEstadoNoHaceNada_test()
        {
            //Arrenge
            var lamparaHalogena40W = new DispositivoInteligente("lampara halogena de 40 W", "0011", 0.04, 360, 90);
            var fecha = DateTime.Now;
            lamparaHalogena40W.Estado.FechaInicial = fecha;

            //Act
            lamparaHalogena40W.apagar();

            //Assert
            Assert.AreEqual(fecha, lamparaHalogena40W.Estado.FechaInicial);
        }

    }
}
