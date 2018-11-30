using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP0.Helpers.ORM;

namespace TP0.Helpers
{
    public class DispositivoEstandar : Dispositivo
    {

        public DispositivoEstandar()
        {

        }
        //cons para crear nuevos
        public DispositivoEstandar(string nom, string idnuevo, double kWxH, double hxdia, double mx, double mn)
        {
            Codigo = idnuevo;
            Nombre = nom;
            KWxHora = kWxH;
            HorasXDia = hxdia;
            Max = mx;
            Min = mn;
            EsInteligente = false;
            FechaAlta = DateTime.Now;
        }
        public DispositivoEstandar(int DEID)//para buscar en la DB + instanciar
        {
            using (var context = new DBContext())
            {
                var Disp = context.Dispositivos.Find(DEID);
                KWxHora = Disp.KWxHora;
                Nombre = Disp.Nombre;
                Codigo = Disp.Codigo;
                Max = Disp.Max;
                Min = Disp.Min;
                ConsumoAcumulado = 0;
                EsInteligente = false;
                UsuarioID = Disp.UsuarioID;
                DispositivoID = Disp.DispositivoID;
                FechaAlta = Disp.FechaAlta;
                //act = new Actuador(DispositivoID);
                ActualizarConsumoAcumulado(new Cliente(UsuarioID).FechaDeAlta);
            }
        }

        public override DispositivoInteligente ConvertirEnInteligente(string marca)
        {
            DispositivoInteligente convertido = null;
            switch (marca)
            {
                case "Samsung":
                    //AdaptadorSamsug convertido = new AdaptadorSamsung(...)
                    convertido = new AdaptadorSamsung("Adaptador Samsung para " + Nombre, Codigo, KWxHora, Max, Min);
                    break;
                case "HP":
                    convertido = new AdaptadorHp("Adaptador HP para " + Nombre, Codigo, KWxHora, Max, Min);
                    break;
                case "Apple":
                    convertido = new AdaptadorApple("Adaptador Apple para " + Nombre, Codigo, KWxHora, Max, Min);
                    break;
            }
            convertido.UsuarioID = UsuarioID;

            return convertido;
        }

        public override void ActualizarConsumoAcumulado(string fechaAlta)
        {
            ConsumoAcumulado = ConsumoEnPeriodo(DateTime.Parse(fechaAlta), DateTime.Now);
        }
        public override double Consumo()
        {
            //Se asume que los disp estandares estan siempre prendidos
            //ConsumoAcumulado = HorasXDia * KWxHora;
            double horasActivo = (DateTime.Now - FechaAlta).TotalHours;
            return Math.Round(horasActivo * KWxHora, 3);
        }
        public override double ConsumoActual()
        {
            return KWxHora;
        }
        public override double ConsumoEnPeriodo(DateTime fInicial, DateTime fFinal)
        {
            if (fFinal > DateTime.Now)
            {
                fFinal = DateTime.Now;
            }

            double horasActivo = (fFinal - fInicial).TotalHours;
            return Math.Round(horasActivo * KWxHora, 3);
        }
        public override double ConsumoEnHoras(double horas)
        {
            return ConsumoEnPeriodo(DateTime.Now.AddHours(-horas), DateTime.Now);
        }

        public override bool EstaEncendido()
        {
            return false;
        }
        public override bool EstaApagado()
        {
            return false;
        }
        public override bool EnAhorro()
        {
            return false;
        }

        public override void Encender()
        {
            throw new NotImplementedException();
        }
        public override void Apagar()
        {
            throw new NotImplementedException();
        }
        public override void AhorrarEnergia()
        {
            throw new NotImplementedException();
        }

        public override void ActualizarUltimoEstado()
        {
            //No hace nada porque no tiene estado
        }
        public override void AgregarEstado(State est)
        {
            throw new NotImplementedException();
        }
        public override State GetEstado()
        {
            throw new NotImplementedException();
        }
        public override List<State> GetEstados()
        {
            //Retorna una lista vacia porque no tiene estados
            return new List<State>();
        }
    }
}