using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class AdaptadorSamsung : DispositivoInteligente
    {
        
        public AdaptadorSamsung(string nom, string idnuevo, double kWxHoraNuevo, Cliente cliente) : base(nom, idnuevo, kWxHoraNuevo, cliente)
        {
            kWxHora = kWxHoraNuevo;
            nombre = nom;
            id = idnuevo;
            cliente.adaptarDispositivo(this);
        }
    }
}