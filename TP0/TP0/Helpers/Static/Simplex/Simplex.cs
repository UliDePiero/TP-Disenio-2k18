using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers.Static.Simplex
{
    public class Simplex
    {
        public List<Double> vars { get; set; }
        public List<restriction> restrictions { get; set; }

        public List<Double> getVars(int cantidad) //se carga la lista de variables (por cada una hay un 1 en la lista)
        {
            List<Double> valores = new List<Double>(50);
            int i = 0;
            while (i < cantidad)
            {
                valores.Add(1);
                i++;
            }
            return valores;
        }

        public List<Double> generarValues(int cantidadDispos, int ubicacionDispositivo, double max1min0, double valorDeReferencia)
        {
            List<Double> valores = new List<Double>(cantidadDispos + 2); //uno por cada dispositivo que haya + el valor de referencia + si es max o min
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