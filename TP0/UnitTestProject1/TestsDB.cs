using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP0.Helpers;
using TP0.Helpers.ORM;

namespace UnitTestProject1
{
    [TestClass]
    public class TestsDB
    {
        [TestMethod]
        public void Tests1()
        {
            /*Caso de prueba 1: Crear 1 usuario nuevo. Persistirlo. 
             *Recuperarlo, modificar la geolocalización y grabarlo. 
             *Recuperarlo y evaluar que el cambio se haya realizado.*/

            //Arrenge
            var db = DBContext.Instancia();
            
            Console.Write("Creando usuario: Juanito");
            var user = new Cliente("Juan", "Cuevas", "Medrano 951", "Juanito", "clavoUnClavito123", "44838254", "DNI", "2342342");

            //Act
            db.Usuarios.Add(user);
            db.SaveChanges();

            var usr = db.Usuarios.First(u => u.Username == "Juanito");
            var punto1 = usr.UbicacionDomicilio();
            Console.WriteLine("La localizacion de {0} es: {1}", punto1[0], punto1[1]);
            usr.Domicilio = "Juncal 3861";
            db.SaveChanges();

            var usr2 = db.Usuarios.First(u => u.Username == "Juanito");
            var punto2 = usr.UbicacionDomicilio();

            //Console.WriteLine("La nueva localizacion de {0} es: {1}", usr.Username, usr.geolocalizacion);
            Console.WriteLine("La localizacion de {0} es: {1}", punto2[0], punto2[1]);

            Assert.AreNotEqual(punto1, punto2);
        }
    }
}
