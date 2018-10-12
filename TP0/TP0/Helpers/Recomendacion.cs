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
using TP0.Helpers.ORM;

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
        public DBContext db = DBContext.Instancia(); 

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

        public string GenerarRecomendacion(Cliente cliente)//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            /*string json = simplex.generarJson(cliente.DispositivosEstandares, cliente.DispositivosInteligentes);
            myWebClient.Headers.Add("Content-Type", "application/json");
            var sURI = "https://dds-simplexapi.herokuapp.com/consultar";

            string respuesta = myWebClient.UploadString(sURI, json);*/

            return "respuesta";
            
        }

        public void EjecutarRecomendacion()
        {
            foreach (Cliente c in clientes)
            { 
                var result = GenerarRecomendacion(c);


                double[] doubleV = ParsearString(result);
                int i = 1;
                
                if (c.accionAutomatica==true)
                {
                    foreach (DispositivoEstandar de in c.Dispositivos)
                    {
                        i++;
                    }
                    foreach (DispositivoInteligente di in c.Dispositivos)
                    {
                        if (doubleV[i] < di.ConsumoEnHoras(720))
                        {
                            di.Apagar();
                        }
                        i++;
                    }
                }
            }
        }

        public double[] ParsearString(string str)
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
            this.EjecutarRecomendacion();
        }

        public void NuevoCliente(Cliente cliente)
        {
            clientes.Add(cliente); //donde se guarda esto?
        }
    }
}
