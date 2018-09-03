using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP0.Helpers.Static.Simplex;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Timers;
using TP0.Helpers.Simplex;

namespace TP0.Helpers
{
    public class Recomendacion
    {
        //public List<Resultado> horasXDisp = new List<Resultado>();
        //public double horasTotalesXMes;
        private static Recomendacion _instancia;
        public WebClient myWebClient = new WebClient(); //uno por consulta? o una sola?
        public List<Cliente> clientes;
        Timer aTimer;
        public SimplexHelper simplex = SimplexHelper.Instancia();

        protected Recomendacion()
        {
            this.CrearTimer();
            clientes = new List<Cliente>();
        }

        public static Recomendacion Instancia()
        {
            if (_instancia == null)
            {
                _instancia = new Recomendacion();
            }
            return _instancia;
        }

        public string generarRecomendacion(Cliente cliente)
        {
            string json = simplex.generarJson(cliente.dispositivosEstandares, cliente.dispositivosInteligentes);
            myWebClient.Headers.Add("Content-Type", "application/json");
            var sURI = "https://dds-simplexapi.herokuapp.com/consultar";

            string respuesta = myWebClient.UploadString(sURI, json);

            return respuesta;
            
        }

        public void ejecutarRecomendacion()
        {
            foreach (Cliente c in clientes)
            { 
                var result = generarRecomendacion(c);


                double[] doubleV = parsearString(result);
                int i = 1;
                
                if (c.accionAutomatica==true)
                {
                    foreach (DispositivoEstandar de in c.dispositivosEstandares)
                    {
                        i++;
                    }
                    foreach (DispositivoInteligente di in c.dispositivosInteligentes)
                    {
                        if (doubleV[i] < di.consumoEnHoras(720))
                        {
                            di.apagar();
                        }
                        i++;
                    }
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

        public void CrearTimer()
        {
            aTimer = new Timer();
            aTimer.Interval = 24 * 60 * 60 * 1000;
            aTimer.Elapsed += HandleTimerElapsed;
            aTimer.Start();
        }
        public void HandleTimerElapsed(object sender, ElapsedEventArgs e)
        {   
            this.ejecutarRecomendacion();
        }

        public void nuevoCliente(Cliente cliente)
        {
            clientes.Add(cliente);
        }
    }
}
