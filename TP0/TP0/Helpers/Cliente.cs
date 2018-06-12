using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP0.Helpers;

namespace TP0.Helpers
{
    public class Cliente : Usuario
    {

        [JsonProperty]
        public double documento;
        [JsonProperty]
        public string tipoDocumento;
        [JsonProperty]
        public double telefono;
        [JsonProperty]
        public Categoria categoria;
        [JsonProperty]
        public List<DispositivoEstandar> dispositivosEstandares;
        [JsonProperty]
        public List<DispositivoInteligente> dispositivosInteligentes;
        [JsonProperty]
        public int puntos;

        public Cliente(Usuario u, double doc, string tipo, double tel):base(u)
        {
            this.nombre = u.nombre;
            this.apellido = u.apellido;
            this.domicilio = u.domicilio;
            this.usuario = u.usuario;
            this.contrasenia = u.contrasenia;

            this.documento = doc;
            this.tipoDocumento = tipo;
            this.telefono = tel;
                  
        }
         


        public bool AlgunDispositivoEncendido()
        {
            return dispositivosInteligentes.Any(d => d.estaEncendido());
        }
        public int DispositivosEncendidos()
        {
            return dispositivosInteligentes.Count(d => d.estaEncendido());
        }
        public int DispositivosApagados()
        {
            return dispositivosInteligentes.Count(d => !d.estaEncendido());
        }
        public int DispositivosTotales()
        {
            return dispositivosEstandares.Count()+ dispositivosInteligentes.Count();
        }
        //falta esto
        public double EstimarFacturacion(DateTime fInicial, DateTime fFinal)
        {
            return categoria.CalcularTarifa(KwTotales(fInicial, fFinal));
        }
        double KwTotales(DateTime fInicial, DateTime fFinal)
        {
            return dispositivosEstandares.Sum(d => d.consumoEnPeriodo(fInicial, fFinal))+dispositivosInteligentes.Sum(d=>d.consumoEnPeriodo(fInicial, fFinal));
        }
        public void agregarDispInteligente(DispositivoInteligente DI)
        {
            dispositivosInteligentes.Add(DI);
            puntos += 15;

        }

        public void adaptarDispositivo(DispositivoInteligente DI)
        {
            dispositivosInteligentes.Add(DI);
            puntos += 10;

        }

        public void ActualizarCategoria()
        {
            
        }
    }
}
