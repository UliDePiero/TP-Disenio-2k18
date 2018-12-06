using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using TP0.Helpers;

namespace TP0.Models
{
    public class SubmitViewModel : ApplicationDbContext
    {
        [Required]
        [Display(Name = "DispositivoSeleccionado")]
        public string DispositivoSeleccionado { get; set; }
        [Required]
        [Display(Name = "IdSeleccionado")]
        public int IdSeleccionado { get; set; }
        [Required]
        public float ValorMax { get; set; }
        [Required]
        public float ValorMin { get; set; }
        [Required]
        public float horas { get; set; }
        [Required]
        public DispositivoInteligente disSelec { get; internal set; }
        [Required]
        public bool aplicarRecomendacion { get; set; }
        [Required]
        public int HorasEstandar { get; set; }
    }
}