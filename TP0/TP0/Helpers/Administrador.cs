using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Administrador : Usuario
    {

        public Administrador (Usuario u)
        {
            this.EsAdmin = true;
        }
        public Administrador()
        {

        }

        public override int MesesQueLleva()
        {
            return Static.FechasAdmin.diferenciaDeMeses(DateTime.Parse(FechaDeAlta), DateTime.Now);
        }

        public override bool AlgunDispositivoEncendido()
        {
            throw new NotImplementedException();
        }

        public override int DispositivosEncendidos()
        {
            throw new NotImplementedException();
        }

        public override int DispositivosApagados()
        {
            throw new NotImplementedException();
        }

        public override int DispositivosTotales()
        {
            throw new NotImplementedException();
        }

        public override double EstimarFacturacion(DateTime fInicial, DateTime fFinal)
        {
            throw new NotImplementedException();
        }

        public override double KwTotales(DateTime fInicial, DateTime fFinal)
        {
            throw new NotImplementedException();
        }

        public override void AgregarDispInteligente(DispositivoInteligente DI)
        {
            throw new NotImplementedException();
        }

        public override void AdaptarDispositivo(DispositivoEstandar D, string marca)
        {
            throw new NotImplementedException();
        }

        public override string SolicitarRecomendacion()
        {
            throw new NotImplementedException();
        }

        public override void ActualizarCategoria()
        {
            throw new NotImplementedException();
        }

        public override List<double> UbicacionDomicilio()
        {
            throw new NotImplementedException();
        }

        public override double CalcDistancia(double[] punto1, double[] punto2)
        {
            throw new NotImplementedException();
        }
    }
}