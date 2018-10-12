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
        [NotMapped]
        public Actuador act;

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
            act = new Actuador(Int32.Parse(idnuevo));
        }
        public DispositivoInteligente()
        {

        }

        public override State GetEstado()
        {
            return Estado;
        }
        public override bool EstaEncendido()
        {
            return Estado is Encendido;
        }
        public override bool EstaApagado()
        {
            return Estado is Apagado ;
        }
        public override void Encender()
        {
            Estado.Encender();
        }
        public override void Apagar()
        {
            using (var db = DBContext.Instancia())
            {
                Estado = db.Estados.First(e => e.StateID == IDUltimoEstado);
                Estado.Apagar();
            }
                
        }
        public override void AhorrarEnergia()
        {
            Estado.AhorrarEnergia();
        }
        public override double ConsumoEnHoras(double horas)
        {
            DateTime fFinal = DateTime.Now;
            DateTime fInicial = fFinal.AddHours(-horas);
            double hs = Static.FechasAdmin.ConsumoHsTotalPeriodo(fInicial, fFinal, estadosAnteriores);
            return hs * KWxHora;
        }

       public override double ConsumoEnPeriodo(DateTime fInicial, DateTime fFinal)
       { 
           double hs = Static.FechasAdmin.ConsumoHsTotalPeriodo(fInicial, fFinal, estadosAnteriores);
           return hs * KWxHora;
       }

       public override void AgregarEstado(State est)
       {
            est.FechaFinal = new DateTime(3000, 1, 1);

            estadosAnteriores.Add(est);

            using (var db = DBContext.Instancia())
            {
                db.Estados.Add(est); //Agrega el nuevo estado a la db
                db.SaveChanges();
                var ultimoEstado = db.Estados.First(e => DispositivoID == e.DispositivoID && e.FechaFinal == new DateTime(3000, 1, 1));
                var d = db.Dispositivos.First(di => di.DispositivoID == DispositivoID);
                d.IDUltimoEstado = ultimoEstado.StateID;
                db.SaveChanges();
            }
       }

        public override double Consumo()
        {
            throw new NotImplementedException();
        }

        public override DispositivoInteligente ConvertirEnInteligente(string marca)
        {
            throw new NotImplementedException();
        }
    }
}
