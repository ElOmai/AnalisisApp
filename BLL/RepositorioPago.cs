using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entidades;


namespace BLL
{
    public class RepositorioPago
    {
        public static bool Guardar(Pago pago)
        {
            bool paso = false;

            Contexto contexto = new Contexto();
            try
            {
                if (contexto.Pago.Add(pago) != null)
                {
                    contexto.Analisis.Find(pago.AnalisisId).Balance -= pago.MontoPago;

                    contexto.SaveChanges();
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


        public static bool Modificar(Pago pago)
        {
            bool paso = false;

            Contexto contexto = new Contexto();

            try
            {
                Pago PagoAnt = RepositorioPago.Buscar(pago.PagoId);


                decimal modificado = pago.MontoPago - PagoAnt.MontoPago;

                var Analisis = contexto.Analisis.Find(pago.AnalisisId);
                Analisis.Balance += modificado;
                RepositorioAnalisis.Modificar(Analisis);

                contexto.Entry(pago).State = EntityState.Modified;
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

            Contexto contexto = new Contexto();
            try
            {
                Pago pago = contexto.Pago.Find(id);

                contexto.Analisis.Find(pago.AnalisisId).Balance += pago.MontoPago;

                contexto.Pago.Remove(pago);

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


        public static Pago Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Pago pago = new Pago();

            try
            {
                pago = contexto.Pago.Find(id);
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return pago;
        }


        public static List<Pago> GetList(Expression<Func<Pago, bool>> expression)
        {
            List<Pago> Pago = new List<Pago>();
            Contexto contexto = new Contexto();

            try
            {
                Pago = contexto.Pago.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }

            return Pago;
        }




    }
}