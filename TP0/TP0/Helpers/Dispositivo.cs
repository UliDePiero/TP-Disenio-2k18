using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Dispositivo
    {
        public string nombre;
        public int kWxHora;
        public bool encendido;

        public bool EstaEncendido()
        {
            return encendido;
        }
        public int KWxHora()
        {
            return kWxHora;
        }
    }
}