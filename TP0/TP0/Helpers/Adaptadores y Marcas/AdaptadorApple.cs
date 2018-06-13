using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class AdaptadorApple : DispositivoInteligente
    {
        public AdaptadorApple(string nom, string idnuevo, double kWxHoraNuevo) : base(nom, idnuevo, kWxHoraNuevo)
        {
            kWxHora = kWxHoraNuevo;
            nombre = nom;
            id = idnuevo;
        }
        
    }

}