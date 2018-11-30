using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers.Static
{
    public static class CategoriasPosibles
    {
        private static List<Categoria> Categorias = new List<Categoria>
        {
            new Categoria(0, 150, 0.644, 18.76),
            new Categoria(150, 325, 0.644, 35.32),
            new Categoria(325, 400, 0.681, 60.71),
            new Categoria(400, 450, 0.738, 71.74),
            new Categoria(450, 500, 0.794, 110.38),
            new Categoria(500, 600, 0.832, 220.75),
            new Categoria(600, 700, 0.851, 433.59),
            new Categoria(700, 1400, 0.851, 545.96),
            new Categoria(1400, 9999, 0.851, 887.19)
        };

        public static Categoria GetCategoria(double consumo)
        {
            Categoria categoria = new Categoria(0, 9999, 0.851, 887.19); //Para que no devuelva null
            foreach (Categoria c in Categorias)
            {
                if (c.PerteneceA(consumo))
                {
                    categoria = c;
                }
            }
            return categoria;
        }
    }
}