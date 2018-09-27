using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class AdaptadorHp : DispositivoInteligente
    {
        public AdaptadorHp(string nom, string idnuevo, double kWxHoraNuevo, double mx, double mn) : base(nom, idnuevo, kWxHoraNuevo, mx,mn)
        {
            KWxHora = kWxHoraNuevo;
            Nombre = nom;
            Codigo = idnuevo;
        }
    }
}
