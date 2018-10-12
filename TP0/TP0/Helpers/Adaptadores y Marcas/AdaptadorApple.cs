using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class AdaptadorApple : DispositivoInteligente
    {
        public AdaptadorApple(string nom, string idnuevo, double kWxHoraNuevo, double mx, double mn) : base(nom, idnuevo, kWxHoraNuevo, mx, mn)
        {
            KWxHora = kWxHoraNuevo;
            Nombre = nom;
            Codigo = idnuevo;
            Max = mx;
            Min = mn;
            Estado = new Apagado(this);
            estadosAnteriores = new List<State>();
            ConsumoAcumulado = 0;
            EsInteligente = true;
            act = new Actuador(Int32.Parse(idnuevo));
        }
        public AdaptadorApple()
        {

        }
    }

}