using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP0.Helpers;

namespace UnitTestProject1
{
    [TestClass]
    public class WebServiceTest
    {
        [TestMethod]
        public void WebService_Test()
        {
            //Arrenge
            var cliente = new Cliente("Luciano", "Panizza", "Medrano951", "paniaton", "asdqwe123", "12345678", "dni", "12345678");
            var recomendacion = new Recomendacion();

            cliente.dispositivosInteligentes.Add(new DispositivoInteligente("lampara halogena de 60 W", "0011", 0.06, 360, 90));
            cliente.dispositivosEstandares.Add(new DispositivoEstandar("televisor LCD de 40 pulgadas", "0014", 0.18, 0, 360, 90));
            cliente.dispositivosEstandares.Add(new DispositivoEstandar("lavarropas automatico de 5kg con calentamiento", "0021", 0.875, 0, 30, 6));
            
            //Act
            var resultado = recomendacion.generarRecomendacion(cliente);

            //Assert
            Assert.AreEqual("[750.0, 360.0, 30.0, 360.0]", resultado);
        }
    }
}
