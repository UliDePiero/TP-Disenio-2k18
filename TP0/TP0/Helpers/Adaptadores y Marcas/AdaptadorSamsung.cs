using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class AdaptadorSamsung : DispositivoInteligente
    {
        
        public AdaptadorSamsung(string nom, string idnuevo, double kWxHoraNuevo, double mx, double mn) : base(nom, idnuevo, kWxHoraNuevo, mx, mn)
        {
            kWxHora = kWxHoraNuevo;
            nombre = nom;
            id = idnuevo;

        }
    }
}