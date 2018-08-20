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
        public WebClient myWebClient = new WebClient(); //uno por cliente o por consulta?
        public List<Resultado> resultados = new List<Resultado>();
        public double horasTotalesXMes;

        public Recomendacion(Cliente cliente)
        {
            generarRecomendacion(cliente);
        }

        public void generarRecomendacion(Cliente cliente)
        {
            string fileName = SimplexHelper.generarJson(cliente.dispositivosEstandares, cliente.dispositivosInteligentes);
            myWebClient.Headers.Add("Content-Type", "application/json");
            var sURI = "https://dds-simplexapi.herokuapp.com/consultar";
            var json = System.IO.File.ReadAllText(fileName);
            var respuesta = myWebClient.UploadString(sURI, json);
            //el primer elemento de respuesta es las horas totales por mes 
            //horasTotalesXMes = respuesta.Take(1);
            //respuesta.Reverse();
            /*
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            */
            string Json = SimplexHelper.generarJson(cliente.dispositivosEstandares, cliente.dispositivosInteligentes);
            var client = new HttpClient();
            var respuesta2 = client.PostAsync("https://dds-simplexapi.herokuapp.com/consultar", new StringContent(Json, Encoding.UTF8, "application/json"));
            
            
            
            foreach ( DispositivoEstandar d in cliente.dispositivosEstandares)
            {
                //horasXDisp.Add(new Resultado(d.nombre,respuesta.Take(1)));
                horasXDisp.Add(new Resultado(d.nombre, respuesta2.Take(1)));
            }

            foreach (DispositivoInteligente d in cliente.dispositivosInteligentes)
            {
                horasXDisp.Add(new Resultado(d.nombre, respuesta.Take(1)));
            }





        public Cliente cliente;
        public List<double> generaRecomendacion()
        {
            List<double> retorno = new List<double>();
            return retorno;

        }
    }
}