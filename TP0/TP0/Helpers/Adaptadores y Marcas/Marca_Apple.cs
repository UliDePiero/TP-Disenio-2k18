using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP0.Helpers.ORM;

namespace TP0.Helpers
{
    public class Marca_Apple
    {
        public void ActualizarUltimoEstadoAPPLE(DispositivoInteligente DI)
        {
            using (var db = new DBContext())
            {
                var ultimoEstado = db.Estados.Find(DI.IDUltimoEstado);
                switch (ultimoEstado.Desc)
                {
                    case "Apagado":
                        DI.Estado = new Apagado(DI);
                        DI.Estado.StateID = ultimoEstado.StateID;
                        break;
                    case "Encendido":
                        DI.Estado = new Encendido(DI);
                        DI.Estado.StateID = ultimoEstado.StateID;
                        break;
                    case "Ahorro":
                        DI.Estado = new Ahorro(DI);
                        DI.Estado.StateID = ultimoEstado.StateID;
                        break;
                    default:
                        throw new Exception("Estado no reconocido");
                }
            }
        }

        public State GetEstadoAPPLE(DispositivoInteligente DI)
        {
            return DI.Estado;
        }
        public List<State> GetEstadosAPPLE(DispositivoInteligente DI)
        {
            //Retorna los estados del dispositivo
            using (var db = new DBContext())
            {
                return db.Estados.Where(e => e.DispositivoID == DI.DispositivoID).ToList();
            }
        }

        public bool EstaEncendidoAPPLE(DispositivoInteligente DI)
        {
            return DI.Estado is Encendido;
        }
        public bool EstaApagadoAPPLE(DispositivoInteligente DI)
        {
            return DI.Estado is Apagado;
        }
        public bool EnAhorroAPPLE(DispositivoInteligente DI)
        {
            return DI.Estado is Ahorro;
        }

        public void EncenderAPPLE(DispositivoInteligente DI)
        {
            DI.Estado.Encender(DI);
        }
        public void ApagarAPPLE(DispositivoInteligente DI)
        {
            DI.Estado.Apagar(DI);
        }
        public void AhorrarEnergiaAPPLE(DispositivoInteligente DI)
        {
            DI.Estado.AhorrarEnergia(DI);
        }

        public void ActualizarConsumoAcumuladoAPPLE(string fechaAlta, DispositivoInteligente DI)
        {
            DI.ConsumoAcumulado = DI.ConsumoEnPeriodo(DateTime.Parse(fechaAlta), DateTime.Now);
        }
        public double ConsumoAPPLE(DispositivoInteligente DI)
        {
            double acumuladoKw = 0;
            DI.ConsumoAcumulado = 0;
            double tiempoTotal = 0;

            DI.estadosAnteriores = DI.GetEstados();
            foreach (State s in DI.estadosAnteriores)
            {
                double c = 0;
                if (s.FechaFinal == new DateTime(3000, 1, 1)) //Si el estado no termino, se usa la fecha de ahora como la final
                    s.FechaFinal = DateTime.Now;

                switch (s.Desc)
                {
                    case "Encendido":
                        c = (s.FechaFinal - s.FechaInicial).Minutes;
                        tiempoTotal += c;
                        break;
                    case "Ahorro":
                        c = (s.FechaFinal - s.FechaInicial).Minutes / 2;
                        tiempoTotal += c;
                        break;
                    case "Apagado":
                        tiempoTotal = (s.FechaFinal - s.FechaInicial).Minutes;
                        break;
                }
                DI.ConsumoAcumulado += c;
                acumuladoKw += c * DI.KWxHora / 60;
            }
            DI.ConsumoPromedio = acumuladoKw / tiempoTotal;
            return acumuladoKw;
        }
        public double ConsumoActualAPPLE(DispositivoInteligente DI)
        {
            DI.ActualizarUltimoEstado();
            switch (DI.Estado.Desc)
            {
                case "Encendido":
                    return DI.KWxHora;
                case "Ahorro":
                    return DI.KWxHora / 2;
                default:
                    return 0;
            }
        }
        public double ConsumoEnHorasAPPLE(double horas, DispositivoInteligente DI)
        {
            using (var db = new DBContext())
            {
                DI.estadosAnteriores = db.Estados.Where(e => e.DispositivoID == DI.DispositivoID).ToList();
            }
            DateTime fFinal = DateTime.Now;
            DateTime fInicial = fFinal.AddHours(-horas);
            double hs = Static.FechasAdmin.ConsumoHsTotalPeriodo(fInicial, fFinal, DI.estadosAnteriores);
            return hs * DI.KWxHora;
        }
        public double ConsumoEnPeriodoAPPLE(DateTime fInicial, DateTime fFinal, DispositivoInteligente DI)
        {

            if (fFinal > DateTime.Now)
            {
                fFinal = DateTime.Now;
            }

            using (var db = new DBContext())
            {
                DI.estadosAnteriores = db.Estados.Where(e => e.DispositivoID == DI.DispositivoID).ToList();
            }
            double hs = Static.FechasAdmin.ConsumoHsTotalPeriodo(fInicial, fFinal, DI.estadosAnteriores);
            return Math.Round(hs * DI.KWxHora, 3);
        }

        public void AgregarEstadoAPPLE(State est, DispositivoInteligente DI)
        {
            DI.Estado = est; //dejar sirve para los cambios de estado cuando el disp esta en memoria
                             //asi evitar recurrir a la base
            using (var db = new DBContext())
            {
                db.Estados.Add(est); //Agrega el nuevo estado a la db
                db.SaveChanges();
                DI.IDUltimoEstado = est.StateID;
                var DIact = db.Dispositivos.Find(DI.DispositivoID);
                DIact.IDUltimoEstado = est.StateID;
                db.SaveChanges();
            }
        }

    }
}