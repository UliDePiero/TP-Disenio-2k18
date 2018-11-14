using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP0.Helpers.Static
{
    public static class CategoriasPosibles
    {
        static Categoria R1 = new Categoria(0, 150, 0.644, 18.76);
        static Categoria R2 = new Categoria(150, 325, 0.644, 35.32);
        static Categoria R3 = new Categoria(325, 400, 0.681, 60.71);
        static Categoria R4 = new Categoria(400, 450, 0.738, 71.74);
        static Categoria R5 = new Categoria(450, 500, 0.794, 110.38);
        static Categoria R6 = new Categoria(500, 600, 0.832, 220.75);
        static Categoria R7 = new Categoria(600, 700, 0.851, 433.59);
        static Categoria R8 = new Categoria(700, 1400, 0.851, 545.96);
        static Categoria R9 = new Categoria(1400, 9999, 0.851, 887.19);
        private static List<Categoria> categorias;

        public static Categoria GetCategoria(double consumo)
        {
            llenarLista();
            Categoria categoria = new Categoria();
            foreach (Categoria c in categorias)
            {
                if (c.PerteneceA(consumo))
                {
                    categoria = c;
                }
            }
            return categoria;
        }
        public static void llenarLista()
        {
            categorias.Add(R1);
            categorias.Add(R2);
            categorias.Add(R3);
            categorias.Add(R4);
            categorias.Add(R5);
            categorias.Add(R6);
            categorias.Add(R7);
            categorias.Add(R8);
            categorias.Add(R9);
        }
    }
}