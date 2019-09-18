using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class AnalisisDetalle
    {
        [Key]
        public int DetalleId { get; set; }
        public int AnalisisId { get; set; }
        public string Resultado { get; set; }
        public string Analisis { get; set; }
        public int TipoId { get; set; }
        public string Descripcion { get; set; }

        public virtual TipoAnalisis TipoAnalisis { get; set; }
        public AnalisisDetalle()
        {
            DetalleId = 0;
            AnalisisId = 0;
            Analisis = string.Empty;
            Resultado = string.Empty;
            TipoId = 0;
            Descripcion = string.Empty;


        }
        public AnalisisDetalle(int analisisId, string resultado, string descripcion)
        {
            AnalisisId = analisisId;
            Resultado = resultado;
            Descripcion = descripcion;
        }
    }
}