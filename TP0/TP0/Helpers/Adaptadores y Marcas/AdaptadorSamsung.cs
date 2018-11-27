using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class AdaptadorSamsung : DispositivoInteligente
    {
        public AdaptadorSamsung(string nom, string idnuevo, double kWxHoraNuevo, double mx, double mn)
        {
            KWxHora = kWxHoraNuevo;
            Nombre = nom;
            Codigo = idnuevo;
            Max = mx;
            Min = mn;
            estadosAnteriores = new List<State>();
            ConsumoAcumulado = 0;
            EsInteligente = true;
            //act = new Actuador(Int32.Parse(idnuevo));
        }
        public AdaptadorSamsung()
        {

        }
        //private Marca_Samsung samsung;
        Marca_Samsung samsung = new Marca_Samsung();

        public override void ActualizarUltimoEstado()
        {
            samsung.ActualizarUltimoEstadoSAMSUNG(this);
        }
        public override State GetEstado()
        {
            return samsung.GetEstadoSAMSUNG(this);
        }
        public override List<State> GetEstados()
        {
            return samsung.GetEstadosSAMSUNG(this);
        }
        public override bool EstaEncendido()
        {
            return samsung.EstaEncendidoSAMSUNG(this);
        }
        public override bool EstaApagado()
        {
            return samsung.EstaApagadoSAMSUNG(this);
        }
        public override bool EnAhorro()
        {
            return samsung.EnAhorroSAMSUNG(this);
        }
        public override void Encender()
        {
            samsung.EncenderSAMSUNG(this);
        }
        public override void Apagar()
        {
            samsung.ApagarSAMSUNG(this);
        }
        public override void AhorrarEnergia()
        {
            samsung.AhorrarEnergiaSAMSUNG(this);
        }
        public override void ActualizarConsumoAcumulado(string FechaAlta)
        {
            samsung.ActualizarConsumoAcumuladoSAMSUNG(FechaAlta, this);
        }
        public override double Consumo()
        {
            return samsung.ConsumoSAMSUNG(this);
        }
        public override double ConsumoActual()
        {
            return samsung.ConsumoActualSAMSUNG(this);
        }
        public override double ConsumoEnHoras(double horas)
        {
            return samsung.ConsumoEnHorasSAMSUNG(horas, this);
        }
        public override double ConsumoEnPeriodo(DateTime fInicial, DateTime fFinal)
        {
            return samsung.ConsumoEnPeriodoSAMSUNG(fInicial, fFinal, this);
        }
        public override void AgregarEstado(State est)
        {
           samsung.AgregarEstadoSAMSUNG(est, this);
        }
    }
}