using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryOrden : IRepositoryOrden
    {
        public IEnumerable<ENC_FACTURA> GetOrden()
        {
            List<ENC_FACTURA> ordenes = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    ordenes = ctx.ENC_FACTURA.
                               Include("USUARIO").
                               Include("TIPO_FACTURA").
                               ToList<ENC_FACTURA>();

                }
                return ordenes;

            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

        public ENC_FACTURA GetOrdenByID(int id)
        {
            ENC_FACTURA orden = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    orden = ctx.ENC_FACTURA.
                               Include("USUARIO").
                               Include("TIPO_FACTURA").
                               Include("DETALLE_FACTURA").
                               Include("DETALLE_FACTURA.PRODUCTO").
                               Where(p => p.ID == id).
                               FirstOrDefault<ENC_FACTURA>();

                }
                return orden;

            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }

        public ENC_FACTURA Save(ENC_FACTURA pOrden)
        {
            int resultado = 0;
            ENC_FACTURA orden = null;
            try
            {
                // Salvar pero con transacción porque son 2 tablas
                // 1- Orden
                // 2- OrdenDetalle 
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //using (var transaccion = ctx.Database.BeginTransaction())
                    //{
                        
                        
                       
                        //foreach (var item in pOrden.DETALLE_FACTURA)
                        //{
                        //    // Busco el producto que está en el detalle por IdLibro
                        //    PRODUCTO oProducto = ctx.PRODUCTO.Find(item.ID_PRODUCTO);

                        //    // Se indica que se alteró
                        //    ctx.Entry(oProducto).State = EntityState.Modified;
                        //    // Guardar                         
                        //    resultado = ctx.SaveChanges();
                        //}

                        // Commit 
                        ctx.ENC_FACTURA.Add(pOrden);
                        resultado = ctx.SaveChanges();
                    //    transaccion.Commit();
                    //}
                }

                // Buscar la orden que se salvó y reenviarla
                if (resultado >= 0)
                    orden = GetOrdenByID(pOrden.ID);

                using (MyContext ctx = new MyContext())
                {
                    foreach (var detalle in pOrden.DETALLE_FACTURA)
                    {
                        detalle.ID_ENC_FACTURA = pOrden.ID;
                        ctx.Entry(detalle).State = EntityState.Modified;
                        //Guardar                         
                        resultado = ctx.SaveChanges();
                    }
                }
                return orden;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        
        }
    }
}
