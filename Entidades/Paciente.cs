using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Paciente
    {
        [Key]
        public int PacienteId { get; set; }
        public string Nombres { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public Paciente()
        {
            PacienteId = 0;
            Nombres = string.Empty;
            Direccion = string.Empty;
            Telefono = string.Empty;
        }
    }
}