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
        public string documento;
        [JsonProperty]
        public string tipoDocumento;
        [JsonProperty]
        public string telefono;
        [JsonProperty]
        public Categoria categoria;
        [JsonProperty]
        public List<DispositivoEstandar> dispositivosEstandares;
        [JsonProperty]
        public List<DispositivoInteligente> dispositivosInteligentes;
        [JsonProperty]
        public int puntos;

        public Cliente(string nombre, string apellido, string domicilio, string usuario, string contrasenia, string doc, string tipo, string tel) 
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.domicilio = domicilio;
            this.usuario = usuario;
            this.contrasenia = contrasenia;
            this.documento = doc;
            this.tipoDocumento = tipo;
            this.telefono = tel;
            this.dispositivosInteligentes = new List<DispositivoInteligente>();
            this.dispositivosEstandares = new List<DispositivoEstandar>();
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
        public double KwTotales(DateTime fInicial, DateTime fFinal)
        {
            return dispositivosEstandares.Sum(d => d.consumoEnPeriodo(fInicial, fFinal))+dispositivosInteligentes.Sum(d=>d.consumoEnPeriodo(fInicial, fFinal));
        }
        public void agregarDispInteligente(DispositivoInteligente DI)
        {
            dispositivosInteligentes.Add(DI);
            puntos += 15;

        }
        public void adaptarDispositivo(DispositivoEstandar D, string marca)
        {
            DispositivoInteligente DI;
            DI=D.convertirEnInteligente(marca);
            dispositivosInteligentes.Add(DI);
            puntos += 10;

        }

        public void ActualizarCategoria()
        {
            
        }
    }
}
