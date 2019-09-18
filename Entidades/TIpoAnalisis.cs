using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class TipoAnalisis
    {
        [Key]
        public int TiposId { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaRegistro { get; set; }

        public TipoAnalisis()
        {
            TiposId = 0;
            Descripcion = string.Empty;
            Precio = 0;
            FechaRegistro = DateTime.Now;
        }
        public TipoAnalisis(int tipoid, string descripcion)
        {
            TiposId = tipoid;
            Descripcion = descripcion;
        }
    }
}