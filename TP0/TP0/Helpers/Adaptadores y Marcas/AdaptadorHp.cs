using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class AdaptadorHp : DispositivoInteligente
    {
        public AdaptadorHp(string nom, string idnuevo, double kWxHoraNuevo) : base(nom, idnuevo, kWxHoraNuevo)
        {
            kWxHora = kWxHoraNuevo;
            nombre = nom;
            id = idnuevo;
        }

        /*
        public AdaptadorHp(string nom, string idnuevo, Cliente cliente) : base(nom, idnuevo, cliente)
        {
        }
        */
    }
}
