using DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RepositorioAnalisis : RepositorioBase<Analisis>
    {

        public override Analisis Buscar(int id)
        {
            Analisis analisis = new Analisis();
            Contexto db = new Contexto();
            try
            {
                analisis = db.Analisis.AsNoTracking().Include(x => x.Detalle).Where(x => x.AnalisisId == id).FirstOrDefault();
            }
            catch (Exception)
            { throw; }
            finally
            { db.Dispose(); }
            return analisis;
        }

        public override bool Modificar(Analisis analisis)
        {
            bool paso = false;
            try
            {
                var Anterior = _contexto.Analisis.Find(analisis.AnalisisId);
                foreach (var item in Anterior.Detalle)
                {
                    if (!analisis.Detalle.Exists(d => d.TipoId == item.TipoId))
                    {
                        item.TipoAnalisis = null;
                        _contexto.Entry(item).State = EntityState.Deleted;
                    }
                }

                foreach (var item in analisis.Detalle)
                {
                    var estado = item.TipoId > 0 ? EntityState.Modified : EntityState.Added;
                    _contexto.Entry(item).State = estado;
                }

                _contexto.Entry(analisis).State = EntityState.Modified;

                if (_contexto.SaveChanges() > 0)
                    paso = true;
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }


    }
}