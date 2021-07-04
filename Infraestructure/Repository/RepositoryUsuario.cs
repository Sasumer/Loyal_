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
    public class RepositoryUsuario : IRepositoryUsuario
    {
        public void DeleteUsuario(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<USUARIO> GetNombreUSUARIO(string nombre)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<USUARIO> GetUSUARIO()
        {
            try
            {
                IEnumerable<USUARIO> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    //LazyLoadingEnabled: esto va cargando solo lo que es requerido, 
                    //pero en este caso quiero una lista de TODO, por eso se pone FALSE.
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Esto es un "Select * from Autor".
                    lista = ctx.USUARIO.ToList<USUARIO>();
                    // lista = ctx.USUARIO.Include(x => x.USUARIO).ToList();
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

        public IEnumerable<USUARIO> GetUSUARIOByContacto(int idContacto)
        {
            throw new NotImplementedException();
        }

        public USUARIO GetUSUARIOByID(int id)
        {
            try
            {
                USUARIO oUSUARIO = null;
                using (MyContext ctx = new MyContext())
                {
                    //LazyLoadingEnabled: esto va cargando solo lo que es requerido, 
                    //pero en este caso quiero una lista de TODO, por eso se pone FALSE.
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Esto es un "Select * from Autor".
                    //lista = ctx.USUARIO.ToList<USUARIO>();
                    oUSUARIO = ctx.USUARIO.Where(p => p.ID == id + "").Include(u => u.Telefono).Include(p => p.USUARIO_ROL).Include("USUARIO_ROL.USUARIO").FirstOrDefault();

                }
                return oUSUARIO;
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

        public USUARIO Save(USUARIO USUARIO)
        {
            throw new NotImplementedException();
        }
    }
}
