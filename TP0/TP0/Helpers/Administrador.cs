using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public class Administrador : Persona
    {
        public int id;
        public FechasAdmin fAdmin;
        public int MesesQueLleva()
        {
            return fAdmin.HaceCuantosMeses(fechaDeAlta);
        }
    }
}