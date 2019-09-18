using DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RepositorioPago : RepositorioBase<Pago>
    {
        public override bool Guardar(Pago entity)
        {
            RepositorioAnalisis repositorio = new RepositorioAnalisis();
            Contexto db = new Contexto();
            foreach (var item in entity.Detalle.ToList())
            {
                var Analisis = repositorio.Buscar(item.AnalisisId);
                Analisis.Balance -= item.MontoPago;
                db.Entry(Analisis).State = System.Data.Entity.EntityState.Modified;
            }

            bool paso = db.SaveChanges() > 0;
            repositorio.Dispose();
            if (paso)
            {
                db.Dispose();
                return base.Guardar(entity);
            }
            db.Dispose();
            return false;
        }
        public override bool Modificar(Pago entity)
        {
            bool paso = false;
            var Anterior = Buscar(entity.PagoId);
            Contexto db = new Contexto();
            try
            {
                using (Contexto contexto = new Contexto())
                {
                    bool flag = false;
                    foreach (var item in Anterior.Detalle.ToList())
                    {
                        if (!entity.Detalle.Exists(x => x.DetallePagoId == item.DetallePagoId))
                        {
                            RepositorioAnalisis repositorio = new RepositorioAnalisis();
                            var Analisis = repositorio.Buscar(item.AnalisisId);
                            Analisis.Balance += item.MontoPago;
                            contexto.Entry(item).State = EntityState.Deleted;
                            contexto.Entry(Analisis).State = EntityState.Modified;
                            flag = true;
                            repositorio.Dispose();
                        }
                    }

                    if (flag)
                        contexto.SaveChanges();
                    contexto.Dispose();
                }

                foreach (var item in entity.Detalle)
                {
                    var estado = EntityState.Unchanged;
                    if (item.DetallePagoId == 0)
                    {
                        RepositorioAnalisis repositorio = new RepositorioAnalisis();
                        var Analisis = repositorio.Buscar(item.AnalisisId);
                        Analisis.Balance -= item.MontoPago;
                        estado = EntityState.Added;
                        db.Entry(Analisis).State = EntityState.Modified;
                        repositorio.Dispose();
                    }
                    db.Entry(item).State = estado;
                }
                db.Entry(entity).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            { throw; }
            finally
            { db.Dispose(); }
            return paso;
        }
        public override Pago Buscar(int id)
        {
            Pago Pago = new Pago();
            Contexto db = new Contexto();
            try
            {
                Pago = db.Pago.Include(x => x.Detalle)
                    .Where(x => x.PagoId == id)
                    .FirstOrDefault();
            }
            catch (Exception)
            { throw; }
            finally
            { db.Dispose(); }
            return Pago;
        }
        public override bool Eliminar(int id)
        {
            Pago Pago = Buscar(id);
            Contexto db = new Contexto();
            foreach (var item in Pago.Detalle)
            {
                RepositorioAnalisis repositorio = new RepositorioAnalisis();
                var Analisis = db.Analisis.Find(item.AnalisisId);
                Analisis.Balance += item.MontoPago;
                repositorio.Modificar(Analisis);
            }
            bool paso = (db.SaveChanges() > 0);
            if (paso)
            {
                db.Dispose();
                return base.Eliminar(Pago.PagoId);
            }
            db.Dispose();
            return false;
        }
    }
}