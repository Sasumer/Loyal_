using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryProveedor : IRepositoryProveedor
    {
        public void DeleteLibro(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PROVEEDOR> GetNombrePROVEEDOR(string nombre)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PROVEEDOR> GetPROVEEDOR()
        {
            try
            {
                IEnumerable<PROVEEDOR> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    //LazyLoadingEnabled: esto va cargando solo lo que es requerido, 
                    //pero en este caso quiero una lista de TODO, por eso se pone FALSE.
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Esto es un "Select * from Autor".
                    lista = ctx.PROVEEDOR.ToList<PROVEEDOR>();
                   // lista = ctx.PROVEEDOR.Include(x => x.USUARIO).ToList();
                }
                return lista;
                //para excepciones de actualizacion (ambos catch se guardarn en C:/temp)
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
                throw;
            }
        }

        public IEnumerable<PROVEEDOR> GetPROVEEDORByContacto(int idContacto)
        {
            throw new NotImplementedException();
        }

        public PROVEEDOR GetPROVEEDORByID(int id)
        {
            try
            {
                PROVEEDOR oROVEEDOR = null;
                using (MyContext ctx = new MyContext())
                {
                    //LazyLoadingEnabled: esto va cargando solo lo que es requerido, 
                    //pero en este caso quiero una lista de TODO, por eso se pone FALSE.
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Esto es un "Select * from Autor".
                    //lista = ctx.PROVEEDOR.ToList<PROVEEDOR>();
                    oROVEEDOR = ctx.PROVEEDOR.Where(p => p.ID == id+"").Include(u => u.USUARIO).Include(p => p.PRODUCTO).FirstOrDefault();

                }
                return oROVEEDOR;
                //para excepciones de actualizacion (ambos catch se guardarn en C:/temp)
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
                throw;
            }
        }

        public PROVEEDOR Save(PROVEEDOR proveedor)
        {
            throw new NotImplementedException();
        }
    }
}
