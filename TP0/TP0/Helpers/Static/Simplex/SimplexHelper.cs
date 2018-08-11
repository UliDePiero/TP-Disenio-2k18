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
            var obj = new Simplex();
            int dispTotales = estandars.Count() + inteligentes.Count();
            obj.vars = new vars(dispTotales);
            List<Double> values1 = new List<Double>();
            //consumo total = 620
            values1.Add(620);
            foreach(DispositivoEstandar de in estandars)
            {
                values1.Add(de.kWxHora);
            }
            foreach(DispositivoInteligente di in inteligentes)
            {
                values1.Add(di.kWxHora);
            }
            
            restriction restriccion1 = new restriction(values1,"<=");
            obj.restrictions.Add(restriccion1);

            return JsonConvert.SerializeObject(obj);
        }
    }
}