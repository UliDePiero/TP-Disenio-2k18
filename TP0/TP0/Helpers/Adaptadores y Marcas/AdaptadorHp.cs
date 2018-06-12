using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class AdaptadorHp : DispositivoInteligente
    {
        public AdaptadorHp(string nom, string idnuevo, double kWxHoraNuevo, Cliente cliente) : base(nom, idnuevo, kWxHoraNuevo, cliente)
        {
            kWxHora = kWxHoraNuevo;
            nombre = nom;
            id = idnuevo;
            cliente.adaptarDispositivo(this);
        }

        /*
        public AdaptadorHp(string nom, string idnuevo, Cliente cliente) : base(nom, idnuevo, cliente)
        {
        }
        */
    }
}
