using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TP0.Helpers.Static.Simplex
{
    public static class SimplexHelper
    {
        public static string generarJson(List<DispositivoEstandar> estandars, List<DispositivoInteligente> inteligentes)
        {
            var obj = new Simplex();//obj es el objeto q se pasa a json
            int dispTotales = estandars.Count() + inteligentes.Count();
            List<Double> v = obj.getVars(dispTotales); //se hace la primera fila del archivo json
            obj.vars = v;
            List<Double> values1 = new List<Double>(); //esta es la primera fila de valores de la parte de "restricciones"
            values1.Add(620); //consumo total = 620
            foreach (DispositivoEstandar de in estandars)
            {
                values1.Add(de.kWxHora); //se llena la lista con los kwxh de los dispositivos estandars
            }
            foreach(DispositivoInteligente di in inteligentes)
            {
                values1.Add(di.kWxHora);//se llena la lista con los kwxh de los dispositivos inteligentes
            }
            
            restriction restriccion1 = new restriction(values1,"<="); //primera restriccion
            obj.restrictions = new List<restriction>();
            obj.restrictions.Add(restriccion1);

            int contador = 1;
            foreach (DispositivoEstandar de in estandars)
            {
                if (de.max > 0)
                {
                    List<Double> vv = obj.generarValues(dispTotales, contador, 1, de.max); //hay que agregarle a cada dispositivo max y min y numeros de referencia
                    restriction r = new restriction(vv);
                    obj.restrictions.Add(r);
                    //se crea nueva restriccion para el maximo
                }
                if (de.min > 0)
                {
                    List<Double> vv = obj.generarValues(dispTotales, contador, 0, de.min); //hay que agregarle a cada dispositivo max y min y numeros de referencia
                    restriction r = new restriction(vv);
                    obj.restrictions.Add(r);
                    //se crea nueva restriccion para el minimo
                }
                contador++;
            }
            foreach (DispositivoInteligente di in inteligentes) //lo mismo q la anterior pero para inteligentes
            {
                if (di.max > 0)
                {
                    List<Double> vv = obj.generarValues(dispTotales, contador, 1, di.max);
                    restriction r = new restriction(vv);
                    obj.restrictions.Add(r);
                }
                if (di.min > 0)
                {
                    List<Double> vv = obj.generarValues(dispTotales, contador, 0, di.min);
                    restriction r = new restriction(vv);
                    obj.restrictions.Add(r);
                }
                contador++;
            }
            
            return JsonConvert.SerializeObject(obj); //se devuelve un string con formato Json
        }
    }
}