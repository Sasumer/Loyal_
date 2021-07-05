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


        public USUARIO GetLoginUsuario(string id, string password)
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
                    oUSUARIO = ctx.USUARIO.Where(p => p.ID.Equals(id) && p.contrasenna==password).FirstOrDefault<USUARIO>();




                }

                if (oUSUARIO != null)
                    oUSUARIO = GetUSUARIOByID(int.Parse(oUSUARIO.ID));

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

        public USUARIO Save(USUARIO usuario, string[] selectedRol)
        {
            int retorno = 0;
            USUARIO uSUARIO = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                uSUARIO = GetUSUARIOByID(int.Parse(usuario.ID));
                IRepositoryRol _RepositoryRol = new RepositoryRol();

                if (uSUARIO == null)
                {

                

                    if (selectedRol != null)
                    {

                        usuario.USUARIO_ROL = new List<USUARIO_ROL>();


                        foreach (var rol in selectedRol)
                        { //LOGICA PARA INSERTAR CATEGORIAS===========================================================
                            //var ubicacionToAdd = _RepositoryUbicacion.GetUbicacionByID(int.Parse(ubicaciones));
                            USUARIO_ROL usuRolToAdd = new USUARIO_ROL();
                            usuRolToAdd.ID_ROL =int.Parse(rol);
                            usuRolToAdd.cedula = usuario.ID;
                            usuRolToAdd.LOG_ACTIVO = true;

                            ctx.USUARIO_ROL.Attach(usuRolToAdd);
                            //Se hace un Attach porque sino, EF intentará esa categoria como nueva,
                            //le indicamos que ya existe, que no la cree. Y la agregamos al libro.
                            usuario.USUARIO_ROL.Add(usuRolToAdd);// asociar a la categoría existente con el libro
                                                                         //FIN =========================================================================================
                            ctx.USUARIO_ROL.Add(usuRolToAdd);
                        }
                    }
                    ctx.USUARIO.Add(usuario);


                    //SaveChanges
                    //guarda todos los cambios realizados en el contexto de la base de datos.
                    retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas
                }

            }

            if (retorno >= 0)
                uSUARIO = GetUSUARIOByID(int.Parse(usuario.ID));

            return uSUARIO;
        }
        //Fin sem 5=========================================
    }
}

