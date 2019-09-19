using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using DAL;
using System.Data.Entity;
using System.Linq.Expressions;

namespace BLL
{
    public class RepositorioAnalisis
    {


        public static bool Guardar(Analisis analisis)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.Analisis.Add(analisis) != null)

                    foreach (var item in analisis.Detalle)
                    {
                        contexto.TiposAnalisis.Find(item.TipoId);
                    }
                contexto.SaveChanges();
                paso = true;

                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        public static bool Modificar(Analisis analisis)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var Ant = RepositorioAnalisis.Buscar(analisis.AnalisisId);


                if (analisis != null)
                {
                    foreach (var item in Ant.Detalle)
                    {
                        contexto.TiposAnalisis.Find(item.TipoAnalisis);
                        if (!analisis.Detalle.ToList().Exists(v => v.DetalleId == item.DetalleId))
                        {
                            item.TipoAnalisis = null;
                            contexto.Entry(item).State = EntityState.Deleted;
                        }
                    }

                    foreach (var item in analisis.Detalle)
                    {
                        contexto.TiposAnalisis.Find(item.TipoAnalisis).TiposId += 1;
                        var estado = item.TipoId > 0 ? EntityState.Modified : EntityState.Added;
                        contexto.Entry(item).State = estado;
                    }

                    decimal modificado = analisis.Balance - Ant.Balance;
                    analisis.Balance += modificado;
                    contexto.Entry(analisis).State = EntityState.Modified;
                }



                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }






        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                var eliminar = db.Analisis.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Analisis Buscar(int id)
        {
            Analisis analisis = new Analisis();
            Contexto db = new Contexto();


            try
            {
                analisis = db.Analisis.Find(id);
                if (analisis != null)
                {
                    analisis.Detalle.Count();
                }


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return analisis;

        }

        public static List<Analisis> GetList(Expression<Func<Analisis, bool>> analisis)
        {
            List<Analisis> Lista = new List<Analisis>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Analisis.Where(analisis).ToList();

            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return Lista;
        }
    }
}