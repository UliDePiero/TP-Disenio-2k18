using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TP0.Helpers.ORM;

namespace TP0.Helpers
{
    public class DispositivoInteligente : Dispositivo
    {
        [NotMapped]
        public State Estado;
        [NotMapped]
        [JsonProperty]
        public List<State> estadosAnteriores;
        [NotMapped]
        public double Min;
        [NotMapped]
        public double Max;


        /*public DispositivoInteligente(string nom, string idnuevo, double kWxHoraNuevo)
        {
            kWxHora = kWxHoraNuevo;
            nombre = nom;
            id = idnuevo;
        }*/
        
        public DispositivoInteligente(string nom, string idnuevo, double kWxHoraNuevo, double mx, double mn)
        {
            KWxHora = kWxHoraNuevo;
            Nombre = nom;
            Codigo = idnuevo;
            Max = mx;
            Min = mn;
            estadosAnteriores = new List<State>();
            ConsumoAcumulado = 0;
            EsInteligente = true;
        }
        public DispositivoInteligente()
        {

        }


        public bool EstaEncendido()
        {
            return Estado is Encendido;
        }
        public bool EstaApagado()
        {
            return Estado is Apagado ;
        }
        public void Encender()
        {
            Estado.Encender();
        }
        public void Apagar()
        {
            Estado.Apagar();
        }
        public void AhorrarEnergia()
        {
            Estado.AhorrarEnergia();
        }
        public double ConsumoEnHoras(double horas)
        {
            DateTime fFinal = DateTime.Now;
            DateTime fInicial = fFinal.AddHours(-horas);
            double hs = Static.FechasAdmin.ConsumoHsTotalPeriodo(fInicial, fFinal, estadosAnteriores);
            return hs * KWxHora;
        }

       public double ConsumoEnPeriodo(DateTime fInicial, DateTime fFinal)
       { 
           double hs = Static.FechasAdmin.ConsumoHsTotalPeriodo(fInicial, fFinal, estadosAnteriores);
           return hs * KWxHora;
       }

       public void AgregarEstado(State est)
       {
            Estado = est;
            Estado.FechaFinal = new DateTime(3000, 1, 1); 
            estadosAnteriores.Add(Estado);

            using (var db = new DBContext())
            {
                db.Estados.Add(Estado); //Agrega el nuevo estado a la db
                db.SaveChanges();
            }
       }
    }
}
