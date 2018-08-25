using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP0.Helpers;

namespace UnitTestProject1
{
    [TestClass]
    public class RecomedacionesTest
    {
        [TestMethod]
        public void AccionesAutomaticaON_Test()
        {
            //Arrenge
            var cliente = new Cliente("Luciano", "Panizza", "Medrano951", "paniaton", "asdqwe123", "12345678", "dni", "12345678");

            var recomendacion = new Recomendacion(cliente);

            var disp1 = new DispositivoInteligente("televisor LED de 24 pulgadas", "0015", 0.04, 360, 90);
            var disp2 = new DispositivoInteligente("televisor LED de 32 pulgadas", "0016", 0.055, 360, 90);

            disp1.estadosAnteriores = new List<State> { new Apagado(disp1) { FechaInicial = DateTime.Now.AddHours(-500), FechaFinal = DateTime.Now.AddHours(-361) }, new Encendido(disp1) { FechaInicial = DateTime.Now.AddHours(-361) } };
            disp2.Estado = new Encendido(disp2); 

            cliente.dispositivosEstandares.Add(new DispositivoEstandar("lavarropas automatico de 5kg con calentamiento", "0021", 0.875, 0, 30, 6));
            cliente.dispositivosEstandares.Add(new DispositivoEstandar("lavarropas semi-automatico de 5kg", "0022", 0.1275, 0, 30, 6));
            cliente.dispositivosEstandares.Add(new DispositivoEstandar("ventilador de pie", "0023", 0.09, 0, 360, 120));
            cliente.dispositivosInteligentes.Add(new DispositivoInteligente("lampara halogena de 60 W", "0011", 0.06, 360, 90));
            cliente.dispositivosInteligentes.Add(disp2);
            cliente.dispositivosInteligentes.Add(disp1);
            

            //Act

            recomendacion.ejecutarRecomendacion();
            

            //Assert
            Assert.IsInstanceOfType(cliente.dispositivosInteligentes[2].Estado , typeof(Apagado));

        }

        [TestMethod]
        public void AccionesAutomaticaOFF_Test()
        {
            //Arrenge
            var cliente = new Cliente("Luciano", "Panizza", "Medrano951", "paniaton", "asdqwe123", "12345678", "dni", "12345678");

            var recomendacion = new Recomendacion(cliente);
            recomendacion.accionAutomatica = false;
            var disp = new DispositivoInteligente("televisor LED de 24 pulgadas", "0015", 0.04, 360, 90);
            disp.estadosAnteriores = new List<State> { new Apagado(disp) { FechaInicial = DateTime.Now.AddHours(-500), FechaFinal = DateTime.Now.AddHours(-361) } };
            disp.agregarEstado(new Encendido(disp) { FechaInicial = DateTime.Now.AddHours(-361) });

            cliente.dispositivosEstandares.Add(new DispositivoEstandar("lavarropas automatico de 5kg con calentamiento", "0021", 0.875, 0, 30, 6)) ;
            cliente.dispositivosEstandares.Add(new DispositivoEstandar("lavarropas semi-automatico de 5kg", "0022", 0.1275, 0, 30, 6)) ;
            cliente.dispositivosEstandares.Add(new DispositivoEstandar("ventilador de pie", "0023", 0.09, 0, 360, 120));
            cliente.dispositivosInteligentes.Add(new DispositivoInteligente("lampara halogena de 60 W", "0011", 0.06, 360, 90));
            cliente.dispositivosInteligentes.Add(new DispositivoInteligente("televisor LED de 32 pulgadas", "0016", 0.055, 360, 90));
            cliente.dispositivosInteligentes.Add(disp);

            //Act

            recomendacion.ejecutarRecomendacion();


            //Assert
            Assert.IsInstanceOfType(cliente.dispositivosInteligentes[2].Estado, typeof(Encendido));

        }
    }
}
