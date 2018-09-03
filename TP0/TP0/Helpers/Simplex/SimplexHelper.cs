using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Microsoft.SqlServer.Server;
using System.IO;
using TP0.Helpers.Static.Simplex;

namespace TP0.Helpers.Simplex
{
    public class SimplexHelper
    {
        public List<double> vars { get; set; }
        public List<restriction> restrictions { get; set; }
        [JsonIgnore]
        private static SimplexHelper _instancia;

        public static SimplexHelper Instancia()
        {
            if (_instancia == null)
            {
                _instancia = new SimplexHelper();
            }
            return _instancia;
        }

        public string generarJson(List<DispositivoEstandar> estandars, List<DispositivoInteligente> inteligentes)
        {
            vars = new List<double>();//obj es el objeto q se pasa a json
            int dispTotales = estandars.Count() + inteligentes.Count();
            List<double> v = getVars(dispTotales); //se hace la primera fila del archivo json
            vars = v;
            List<double> values1 = new List<double>(); //esta es la primera fila de valores de la parte de "restricciones"
            values1.Add(612); //consumo total = 620 // Lo subi a 620000 porque sino no anda por la cantidad de dispositivos
            foreach (DispositivoEstandar de in estandars)
            {
                values1.Add(de.kWxHora); //se llena la lista con los kwxh de los dispositivos estandars
            }
            foreach(DispositivoInteligente di in inteligentes)
            {
                values1.Add(di.kWxHora);//se llena la lista con los kwxh de los dispositivos inteligentes
            }
            
            restriction restriccion1 = new restriction(values1,"<="); //primera restriccion
            restrictions = new List<restriction>();
            restrictions.Add(restriccion1);

            int contador = 1;
            foreach (DispositivoEstandar de in estandars)
            {
                if (de.max > 0)
                {
                    List<double> vv = generarValues(dispTotales, contador, 1, de.max); //hay que agregarle a cada dispositivo max y min y numeros de referencia
                    restriction r = new restriction(vv);
                    restrictions.Add(r);
                    //se crea nueva restriccion para el maximo
                }
                if (de.min > 0)
                {
                    List<double> vv = generarValues(dispTotales, contador, 0, de.min); //hay que agregarle a cada dispositivo max y min y numeros de referencia
                    restriction r = new restriction(vv);
                    restrictions.Add(r);
                    //se crea nueva restriccion para el minimo
                }
                contador++;
            }
            foreach (DispositivoInteligente di in inteligentes) //lo mismo q la anterior pero para inteligentes
            {
                if (di.max > 0)
                {
                    List<double> vv = generarValues(dispTotales, contador, 1, di.max);
                    restriction r = new restriction(vv);
                    restrictions.Add(r);
                }
                if (di.min > 0)
                {
                    List<double> vv = generarValues(dispTotales, contador, 0, di.min);
                    restriction r = new restriction(vv);
                    restrictions.Add(r);
                }
                contador++;
            }
            string jsonData = JsonConvert.SerializeObject(this);

            return jsonData.Replace("operador", "operator");
        }

        public List<double> getVars(int cantidad) //se carga la lista de variables (por cada una hay un 1 en la lista)
        {
            List<double> valores = new List<double>(50);
            int i = 0;
            while (i < cantidad)
            {
                valores.Add(1);
                i++;
            }
            return valores;
        }

        public List<double> generarValues(int cantidadDispos, int ubicacionDispositivo, double max1min0, double valorDeReferencia)
        {
            List<double> valores = new List<double>(cantidadDispos + 2); //uno por cada dispositivo que haya + el valor de referencia + si es max o min
            valores.Add(valorDeReferencia); // que es el primero
            int i = 1;
            for (; i <= cantidadDispos; i++)
            {//si la regla es para el dispositivo en ubicacionDispositivo, se agrega un 1 en esa posicion
                if (i != ubicacionDispositivo)
                {
                    valores.Add(0);
                }
                else
                {
                    valores.Add(1);
                }
            }
            valores.Add(max1min0); // este ultimo campo se saca desde la restriccion
            return valores; //esta lista es del tipo "values":[90,0,0,1....]
        }
    }
}
