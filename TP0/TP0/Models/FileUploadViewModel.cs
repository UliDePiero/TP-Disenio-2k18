using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP0.Models
{
    public class FileUploadViewModel
    {
        [DataType(DataType.Upload)]
        [Display(Name = "Cargar archivo.")]
        [Required(ErrorMessage = "Por favor seleccionar el archivo a subir.")]
        public string file { get; set; }

    }
}