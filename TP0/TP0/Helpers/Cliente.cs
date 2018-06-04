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
        public void ActualizarCategoria()
        {
            
        }
    }
}
