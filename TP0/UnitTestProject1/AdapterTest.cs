using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP0.Helpers;

namespace UnitTestProject1
{
    [TestClass]
    public class AdapterTest
    {
        [TestMethod]
        public void ConvertirDEaDI_test()
        {
            //Arrenge
            var tvsamsung = new DispositivoEstandar("televisor LCD de 40 pulgadas", "0014", 0.18, 0, 360, 90);


            //Act
            var tvSamIntelingente = tvsamsung.convertirEnInteligente("Samsung");
            tvSamIntelingente.encender();

            //Assert
            Assert.IsInstanceOfType(tvSamIntelingente, typeof(DispositivoInteligente));
            Assert.IsInstanceOfType(tvSamIntelingente.Estado, typeof(Encendido));
        }
    }
}
