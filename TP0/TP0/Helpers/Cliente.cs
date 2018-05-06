using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP0.Helpers;

namespace TP0.Helpers
{
    public class Cliente:Persona
    {
        [JsonProperty]
        public Documento documento;
        [JsonProperty]
        public string telefono;
        [JsonProperty]
        public Categoria categoria;
        [JsonProperty]
        public List<Dispositivo> dispositivos;
        public bool AlgunDispositivoEncendido()
        {
            return dispositivos.Any(d => d.EstaEncendido());
        }
        public int DispositivosEncendidos()
        {
            return dispositivos.Count(d=>d.EstaEncendido());
        }
        public int DispositivosApagados()
        {
            return dispositivos.Count(d => !d.EstaEncendido());
        }
        public int DispositivosTotales()
        {
            return dispositivos.Count();
        }
        public int CalcularConsumo()
        {
            return dispositivos.Sum(d=>d.KWxHora());
        }
        public void ActualizarCategoria(){
            
        }
    }
}
