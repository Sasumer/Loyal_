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

        public PROVEEDOR Save(PROVEEDOR proveedor, String[] selectedUsuarios)
        {
            int retorno = 0;
            PROVEEDOR oPROVEEDOR = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oPROVEEDOR = GetPROVEEDORByID( int.Parse(proveedor.ID));
                IRepositoryUsuario _RepositoryUsuario = new RepositoryUsuario();

                if (oPROVEEDOR == null)
                {

                    //Insertar
                    if (selectedUsuarios != null)
                    {


                        proveedor.USUARIO = new List<USUARIO>();
                        foreach (var usuario in selectedUsuarios)
                        {
                            var usuarioToAdd = _RepositoryUsuario.GetUSUARIOByID(int.Parse(usuario));
                            ctx.USUARIO.Attach(usuarioToAdd); //sin esto, EF intentará crear una categoría
                            proveedor.USUARIO.Add(usuarioToAdd);// asociar a la categoría existente con el libro


                        }
                    }
                    ctx.PROVEEDOR.Add(proveedor);
                    //SaveChanges
                    //guarda todos los cambios realizados en el contexto de la base de datos.
                    retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas
                }
                else
                {
                    //Registradas: 1,2,3
                    //Actualizar: 1,3,4

                    //Actualizar Libro
                    ctx.PROVEEDOR.Add(proveedor);
                    ctx.Entry(proveedor).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                    //Actualizar Categorias
               



                   
                    if (selectedUsuarios != null)
                    {
                        var selectedUsuariosID = new HashSet<string>(selectedUsuarios);
                        ctx.Entry(proveedor).Collection(p => p.USUARIO).Load();
                        var newCategoriaForLibro = ctx.USUARIO
                         .Where(x => selectedUsuariosID.Contains(x.ID.ToString())).ToList();
                        proveedor.USUARIO = newCategoriaForLibro;

                        ctx.Entry(proveedor).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                }
            }

            if (retorno >= 0)
                oPROVEEDOR = GetPROVEEDORByID(int.Parse(proveedor.ID));

            return oPROVEEDOR;
        }
    }
    
}
