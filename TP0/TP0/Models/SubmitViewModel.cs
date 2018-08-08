using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace TP0.Models
{
    public class SubmitViewModel : ApplicationDbContext
    {
        [Required]
        [Display(Name = "DispositivoSeleccionado")]
        public string DispositivoSeleccionado { get; set; }
    }
}