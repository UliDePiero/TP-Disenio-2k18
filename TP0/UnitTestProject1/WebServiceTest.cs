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
            var recomendacion = new Recomendacion(cliente);

            cliente.dispositivosInteligentes.Add(new DispositivoInteligente("lampara halogena de 60 W", "0011", 0.06, 360, 90));
            cliente.dispositivosEstandares.Add(new DispositivoEstandar("televisor LCD de 40 pulgadas", "0014", 0.18, 0, 360, 90));
            cliente.dispositivosEstandares.Add(new DispositivoEstandar("lavarropas automatico de 5kg con calentamiento", "0021", 0.875, 0, 30, 6));
            
            //Act

            var resultado = recomendacion.generarRecomendacion();
            double[] doubleV = recomendacion.parsearString(resultado);

            //Assert
            Assert.AreEqual(750.0, doubleV[0]);
        }

        [TestMethod]
        public void WebService_Test1()
        {
            //Arrenge
            var cliente = new Cliente("Ariel", "Ejemplo", "Medrano951", "ariel", "aaaaa", "12345678", "dni", "12345678");
            var recomendacion = new Recomendacion(cliente);
            /*1 TV LED 40”   2. 1 lámpara de 11 W    3. 1 lavarropas Semi-automático de 5 kg  4. 1 PC de escritorio    5. 1 aire Acondicionado de 2200    frigorías
              6. 1 microondas convencional             7. 1 plancha a vapor            8. 1 ventilador de techo*/

            cliente.dispositivosInteligentes.Add(new DispositivoInteligente("televisor LED de 40 pulgadas", "0017", 0.08, 360, 90));
            cliente.dispositivosInteligentes.Add(new DispositivoInteligente("lampara de 11 W", "0011", 0.011, 360, 90));
            cliente.dispositivosEstandares.Add(new DispositivoEstandar("lavarropas semi-automatico de 5kg", "0022", 0.1275, 0, 30, 6));
            cliente.dispositivosInteligentes.Add(new DispositivoInteligente("pc de escritorio", "0011", 0.4, 360, 90));
            cliente.dispositivosInteligentes.Add(new DispositivoInteligente("aire acondicionado de 2200 frigorias", "0012", 1.013, 360, 90));
            cliente.dispositivosEstandares.Add(new DispositivoEstandar("microondas convencional", "0011", 0.64, 0, 15, 3));
            cliente.dispositivosEstandares.Add(new DispositivoEstandar("plancha a vapor", "0011", 0.75, 0, 30, 3));
            cliente.dispositivosInteligentes.Add(new DispositivoInteligente("ventilador de techo", "0024", 0.06, 360, 120));

            
            //Act
            var resultado = recomendacion.generarRecomendacion();

            //Assert
            Assert.AreEqual("[1875.0, 30.0, 15.0, 30.0, 360.0, 360.0, 360.0, 360.0, 360.0]", resultado);
        }
        
    }
}
