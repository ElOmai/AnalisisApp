using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Analisis> Analisis { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<TipoAnalisis> TiposAnalisis { get; set; }
        public DbSet<Pago> Pago { get; set; }
        public Contexto() : base("ConStr") { }
    }
}