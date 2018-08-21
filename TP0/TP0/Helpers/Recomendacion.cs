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
        public Schedule planEjeucion;
        public bool accionAutomatica;

        public Recomendacion()
        {
            //generarRecomendacion(cliente);
            this.planEjeucion = new Schedule(this);
        }

        public string generarRecomendacion(Cliente cliente)
        //public double[] generarRecomendacion(Cliente cliente)
        {
            string fileName = SimplexHelper.generarJson(cliente.dispositivosEstandares, cliente.dispositivosInteligentes);
            myWebClient.Headers.Add("Content-Type", "application/json");
            var sURI = "https://dds-simplexapi.herokuapp.com/consultar";
            var json = System.IO.File.ReadAllText(fileName);
            var respuesta = myWebClient.UploadString(sURI, json);
            /*
            string[] respuestaArrayString = respuesta.Split(',');
            double[] respuestaArrayDouble = new double[respuestaArrayString.Length];
            for (int i = 0; i < respuestaArrayString.Length; i++)
            {
                respuestaArrayDouble[i] = Convert.ToDouble(respuestaArrayString[i]);
            }
            return respuestaArrayDouble;
            */
            return respuesta;
            
        }
    }
}