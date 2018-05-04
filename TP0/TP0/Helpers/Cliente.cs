using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Cliente:Persona
    {
        public Documento documento;
        public string telefono;
        public enum categoria{ R1, R2, R3, R4, R5, R6, R7, R9, R9 };
        public Dispositivo[] dispositivos;
        public bool AlgunDispositivoEncendido()
        {
            if (dispositivos.Any(d => d.encendido))
            {
                return true;
            }
            else return false;
        }
        public int dispositivosEncendidos()
        {
            int cant = dispositivos.Count(d=>d.encendido);
            return cant;
        }
        public int dispositivosApagados()
        {
            int cant = dispositivos.Count(d => !d.encendido);
            return cant;
        }
        public int dispositivosTotales()
        {
            return dispositivos.Count();
        }
        public int calcularConsumo()
        {
            return dispositivos.Sum(d=>d.kWxHora);
        }
        public void actualizarCategoria(){
            
        }
    }
}
