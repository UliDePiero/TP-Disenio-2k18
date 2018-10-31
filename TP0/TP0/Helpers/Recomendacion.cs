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
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TP0.Helpers
{
    public class Recomendacion
    {
        //public List<Resultado> horasXDisp = new List<Resultado>();
        //public double horasTotalesXMes;
        List<Dispositivo> LDI;
        List<Dispositivo> LDE;
        public String[] NombresDeDisp;
        [NotMapped]
        private static Recomendacion _instancia;
        [NotMapped]
        public WebClient myWebClient = new WebClient(); //uno por consulta? o una sola?
        [NotMapped]
        public ICollection<Usuario> clientes;
        [NotMapped]
        Timer aTimer;
        [NotMapped]
        public SimplexHelper simplex = SimplexHelper.Instancia();

        protected Recomendacion()
        {
            this.CrearTimer();
            clientes = new List<Usuario>();
        }

        public static Recomendacion Instancia()
        {
            if (_instancia == null)
            {
                _instancia = new Recomendacion();
            }
            return _instancia;
        }
        
        public string GenerarRecomendacion(Cliente cliente)
        {
            var disp = cliente.GetDisps();
            LDI = new List<Dispositivo>();
            LDE = new List<Dispositivo>();

            foreach (var d in disp)
            {
                if (d.EsInteligente)
                    LDI.Add(d);
                else
                    LDE.Add(d);
            }
            string json = simplex.generarJson(LDE,LDI);
            myWebClient.Headers.Add("Content-Type", "application/json");
            var sURI = "https://dds-simplexapi.herokuapp.com/consultar";

                return myWebClient.UploadString(sURI, json);
        }

        public void EjecutarRecomendacion()
        {
            using (var db = new DBContext())
            {
                clientes = db.Usuarios.Where(x => x.EsAdmin == false).ToList();
            }
            foreach (var c in clientes)
            {
                
                var cl = new Cliente(c.Username);
                var result = GenerarRecomendacion(cl);

                    double[] doubleV = ParsearString(result);
                    
                    int i=1;

                    if (cl.AccionAutomatica == true)
                    {
                        foreach (var de in LDE)
                        {
                        i++;
                        }
                        foreach (var di in LDI)
                        {
                        var disp = new DispositivoInteligente(di.DispositivoID);
                            if (doubleV[i]* disp.KWxHora < disp.ConsumoEnHoras(720))
                            {
                                disp.Apagar();
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
            clientes.Add(cliente);
        }
    }
}
