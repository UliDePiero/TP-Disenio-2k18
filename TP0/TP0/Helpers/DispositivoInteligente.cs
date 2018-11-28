using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TP0.Helpers.ORM;
using TP0.Helpers.Static;

namespace TP0.Helpers
{
    public class DispositivoInteligente : Dispositivo
    {
        [NotMapped]
        public State Estado;
        [NotMapped]
        [JsonProperty]
        public ICollection<State> estadosAnteriores;
        [NotMapped]
        public List<Actuador> actuadores;

        public DispositivoInteligente()
        {

        }

        //cons para crear nuevos
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
            Estado = null;
            actuadores = new List<Actuador>();
            //act = new Actuador(DispositivoID);
        }

        public DispositivoInteligente(int DIID)//para buscar en la DB + instanciar
        {
            using (var context = new DBContext())
            {
                var Disp = context.Dispositivos.Find(DIID);
                KWxHora = Disp.KWxHora;
                Nombre = Disp.Nombre;
                Codigo = Disp.Codigo;
                Max = Disp.Max;
                Min = Disp.Min;
                estadosAnteriores = new List<State>();
                ConsumoAcumulado = 0;
                EsInteligente = true;
                IDUltimoEstado = Disp.IDUltimoEstado;
                this.ActualizarUltimoEstado();
                UsuarioID = Disp.UsuarioID;
                DispositivoID = Disp.DispositivoID;
                actuadores = new List<Actuador>();
                //act = new Actuador(DispositivoID);
                ActualizarConsumoAcumulado(new Cliente(UsuarioID).FechaDeAlta);
            }
        }

        public override void ActualizarUltimoEstado()
        {
            using (var db = new DBContext())
            {
                var ultimoEstado = db.Estados.Find(IDUltimoEstado);
                switch (ultimoEstado.Desc)
                {
                    case "Apagado":
                        Estado = new Apagado(this);
                        Estado.StateID = ultimoEstado.StateID;
                        break;
                    case "Encendido":
                        Estado = new Encendido(this);
                        Estado.StateID = ultimoEstado.StateID;
                        break;
                    case "Ahorro":
                        Estado = new Ahorro(this);
                        Estado.StateID = ultimoEstado.StateID;
                        break;
                    default:
                        throw new Exception("Estado no reconocido");
                }
            }
        }

        public override State GetEstado()
        {
            return Estado;
        }
        public override List<State> GetEstados()
        {
            //Retorna los estados del dispositivo
            using (var db = new DBContext())
            {
                return db.Estados.Where(e => e.DispositivoID == DispositivoID).ToList();
            }
        }

        public override bool EstaEncendido()
        {
            return Estado is Encendido;
        }
        public override bool EstaApagado()
        {
            return Estado is Apagado;
        }
        public override bool EnAhorro()
        {
            return Estado is Ahorro;
        }

        public override void Encender()
        {
            Estado.Encender(this);
        }
        public override void Apagar()
        {
            Estado.Apagar(this);
        }
        public override void AhorrarEnergia()
        {
            Estado.AhorrarEnergia(this);
        }

        public override void ActualizarConsumoAcumulado(string fechaAlta)
        {
            ConsumoAcumulado = ConsumoEnPeriodo(DateTime.Parse(fechaAlta), DateTime.Now);
        }
        public override double Consumo()
        {
            //Retorna el consumo que genero el dispositivo
            double tiempoTotal = 0;
            double c = 0;

            estadosAnteriores = GetEstados();
            foreach (State s in estadosAnteriores)
            {
                if (s.FechaFinal == new DateTime(3000, 1, 1)) //Si el estado no termino, se usa la fecha de ahora como la final
                    s.FechaFinal = DateTime.Now;

                switch (s.Desc)
                {
                    case "Encendido":
                        c += (s.FechaFinal - s.FechaInicial).TotalHours;
                        tiempoTotal += c;
                        break;
                    case "Ahorro":
                        c += (s.FechaFinal - s.FechaInicial).TotalHours / 2;
                        tiempoTotal += c;
                        break;
                    case "Apagado":
                        tiempoTotal += (s.FechaFinal - s.FechaInicial).TotalHours;
                        break;
                }
            }
            ConsumoAcumulado = Math.Round(c * KWxHora, 3);
            ConsumoPromedio = Math.Round(ConsumoAcumulado / tiempoTotal, 3);
            return ConsumoAcumulado;
        }
        public override double ConsumoActual()
        {
            ActualizarUltimoEstado();
            switch (Estado.Desc)
            {
                case "Encendido":
                    return KWxHora;
                case "Ahorro":
                    return KWxHora / 2;
                default:
                    return 0;
            }
        }
        public override double ConsumoEnHoras(double horas)
        {
            estadosAnteriores = GetEstados();
            DateTime fFinal = DateTime.Now;
            DateTime fInicial = fFinal.AddHours(-horas);
            double hs = FechasAdmin.HsConsumidasTotalPeriodo(fInicial, fFinal, estadosAnteriores);
            return Math.Round(hs * KWxHora, 3);
        }
        public override double ConsumoEnPeriodo(DateTime fInicial, DateTime fFinal)
        {
            if (fFinal > DateTime.Now)
                fFinal = DateTime.Now;
            estadosAnteriores = GetEstados();
            double hs = FechasAdmin.HsConsumidasTotalPeriodo(fInicial, fFinal, estadosAnteriores);
            return Math.Round(hs * KWxHora, 3);
        }

        public override void AgregarEstado(State est)
        {
            Estado = est; //dejar sirve para los cambios de estado cuando el disp esta en memoria
                          //asi evitar recurrir a la base
            using (var db = new DBContext())
            {
                db.Estados.Add(est); //Agrega el nuevo estado a la db
                db.SaveChanges();
                IDUltimoEstado = est.StateID;
                var DIact = db.Dispositivos.Find(DispositivoID);
                DIact.IDUltimoEstado = est.StateID;
                db.SaveChanges();
            }
        }
        public void AgregarActuadores()
        {
            //Al crear el disp se agregan los actuadores de este
            using (var db = new DBContext())
            {
                db.Actuadores.Add(new ActuadorHumedad(DispositivoID));
                db.Actuadores.Add(new ActuadorLuz(DispositivoID));
                db.Actuadores.Add(new ActuadorMovimiento(DispositivoID));
                db.Actuadores.Add(new ActuadorTemperatura(DispositivoID));
                db.SaveChanges();
            }
        }

        public override DispositivoInteligente ConvertirEnInteligente(string marca)
        {
            throw new NotImplementedException();
        }

        public void AsignarActuadorHumedad()
        {
            actuadores.Add(new ActuadorHumedad(DispositivoID));
        }
        public void AsignarActuadorMovimiento()
        {
            actuadores.Add(new ActuadorMovimiento(DispositivoID));
        }
        public void AsignarActuadorTemperatura()
        {
            actuadores.Add(new ActuadorTemperatura(DispositivoID));
        }
        public void AsignarActuadorLuz()
        {
            actuadores.Add(new ActuadorLuz(DispositivoID));
        }

        public void CargarActuador()
        {
            actuadores.Clear();
            using (var db = new DBContext())
                foreach (Actuador a in db.Actuadores)
                    if (a.DispositivoID == DispositivoID)
                        actuadores.Add(a);
        }
        public List<Regla> GetReglas()
        {
            List<Regla> reglas = new List<Regla>();
            using (var db = new DBContext())
            {
                foreach (Regla r in db.Reglas)
                    foreach (Actuador a in actuadores)
                        if (r.ActuadorID == a.ActuadorID)
                            reglas.Add(r);
            }
            return reglas;
        }
        public List<Sensor> GetSensores()
        {
            List<Sensor> sensores = new List<Sensor>();
            using (var db = new DBContext())
            {
                foreach (Sensor s in db.Sensores)
                    if (s.UsuarioID == UsuarioID)
                        sensores.Add(s);
            }
            foreach (Sensor s in sensores)
            {
                s.CargarReglas();
                s.Medir(20, DateTime.Now); //Se hardcodea el valor de la medicion
            }

            return sensores;
        }
        public void AgregarRegla(Regla r)
        {
            switch (r.Tipo)
            {
                case "Humedad":
                    foreach (Actuador a in actuadores)
                        if (a is ActuadorHumedad)
                            r.ActuadorID = a.ActuadorID;
                    break;
                case "Luz":
                    foreach (Actuador a in actuadores)
                        if (a is ActuadorLuz)
                            r.ActuadorID = a.ActuadorID;
                    break;
                case "Movimiento":
                    foreach (Actuador a in actuadores)
                        if (a is ActuadorMovimiento)
                            r.ActuadorID = a.ActuadorID;
                    break;
                default:
                    foreach (Actuador a in actuadores)
                        if (a is ActuadorTemperatura)
                            r.ActuadorID = a.ActuadorID;
                    break;
            }
            using (var db = new DBContext())
            {
                foreach (Sensor s in db.Sensores)
                    if (s.UsuarioID == UsuarioID && s.Desc == r.Tipo)
                    {
                        r.SensorID = s.SensorID;
                        break;
                    }
                db.Reglas.Add(r);
                db.SaveChanges();
            }
        }
    }
}