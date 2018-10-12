using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP0.Helpers
{
    public abstract class Dispositivo
    {
        [Key]
        public int DispositivoID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double KWxHora { get; set; }
        public bool EsInteligente { get; set; }
        public int UsuarioID { get; set; }
        public int IDUltimoEstado { get; set; } //es el equivalente a ESTADO
        [NotMapped]
        public string Desc { get; set; }
        [NotMapped]
        public double ConsumoAcumulado { get; set; }
        [NotMapped]
        public double ConsumoPromedio { get; set; }


        [ForeignKey("UsuarioID")]
        public Usuario Usuario { get; set; }

        public abstract bool EstaEncendido();
        public abstract bool EstaApagado();
        public abstract void Encender();
        public abstract void Apagar();
        public abstract void AhorrarEnergia();
        public abstract double Consumo();
        public abstract double ConsumoEnHoras(double horas);
        public abstract double ConsumoEnPeriodo(DateTime fInicial, DateTime fFinal);
        public abstract void AgregarEstado(State est);
        public abstract DispositivoInteligente ConvertirEnInteligente(string marca);
        public abstract State GetEstado();
    }
}