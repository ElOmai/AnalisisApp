using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Pago
    {
        [Key]
        public int PagoId { get; set; }
        public int AnalisisId { get; set; }
        public DateTime FechaRegistro { get; set; }

        public virtual List<PagosDetalle> Detalle { get; set; }
        public Pago()
        {
            PagoId = 0;
            AnalisisId = 0;
            FechaRegistro = DateTime.Now;
        }

        public void AgregarDetalle(int analisisId, int pagoId, decimal montopago)
        {
            this.Detalle.Add(new PagosDetalle(analisisId, pagoId, montopago));
        }
    }
}