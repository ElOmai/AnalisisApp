using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Analisis
    {
        [Key]
        public int AnalisisId { get; set; }
        public int PacienteId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public decimal Monto { get; set; }
        public decimal Balance { get; set; }

        public virtual List<AnalisisDetalle> Detalle { get; set; }

        public Analisis()
        {
            AnalisisId = 0;
            PacienteId = 0;
            FechaRegistro = DateTime.Now;
            Detalle = new List<AnalisisDetalle>();
        }

        public void AgregarDetalle(int analisisId, string resultado, string descripcion)
        {
            this.Detalle.Add(new AnalisisDetalle(analisisId, resultado, descripcion));
        }
    }
}