using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP0.Helpers.Static.Simplex;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Text;

namespace TP0.Helpers
{
    public class Recomendacion
    {
        //public List<Resultado> horasXDisp = new List<Resultado>();
        //public double horasTotalesXMes;

        public WebClient myWebClient = new WebClient(); //uno por cliente o por consulta?
        public Schedule planEjecucion;
        public bool accionAutomatica;
        public Cliente cliente;

        public Recomendacion(Cliente c)
        {
            cliente = c;
            this.planEjecucion = new Schedule(this);
        }

        public string generarRecomendacion()
        //public double[] generarRecomendacion(Cliente cliente)
        {
            string json = SimplexHelper.generarJson(cliente.dispositivosEstandares, cliente.dispositivosInteligentes);
            myWebClient.Headers.Add("Content-Type", "application/json");
            var sURI = "https://dds-simplexapi.herokuapp.com/consultar";

            string respuesta = myWebClient.UploadString(sURI, json);

            return respuesta;
            
        }

        public void ejecutarRecomendacion()
        {
            var result = generarRecomendacion();


            double[] doubleV = parsearString(result);
            int i = 1;

            if (accionAutomatica==true)
            {
                foreach (DispositivoEstandar de in cliente.dispositivosEstandares)
                {
                    i++;
                }
                foreach (DispositivoInteligente di in cliente.dispositivosInteligentes)
                {
                    if (doubleV[i] < di.consumoEnHoras(720))
                    {
                        di.apagar();
                    }
                    i++;
                }
            }
        }

        public double[] parsearString(string str)
        {
            str = str.Replace("[", "");
            str = str.Replace("]", "");
            str = str.Replace(".0", "");
            string[] respuestaArrayString = str.Split(',');
            double[] respuestaArrayDouble = new double[respuestaArrayString.Length];
            for (int i = 0; i < respuestaArrayString.Length; i++)
            {
                respuestaArrayDouble[i] = Convert.ToDouble(respuestaArrayString[i]);
            }
            return respuestaArrayDouble;
        }
    }
}
