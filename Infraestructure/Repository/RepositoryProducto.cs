
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Models;

namespace Infraestructure.Repository
{
    public class RepositoryProducto : IRepositoryProducto
    {


        public IEnumerable<PRODUCTO> GetProducto()
        {
            try
            {
                IEnumerable<PRODUCTO> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    //LazyLoadingEnabled: esto va cargando solo lo que es requerido, 
                    //pero en este caso quiero una lista de TODO, por eso se pone FALSE.
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Esto es un "Select * from Autor".
                    //lista = ctx.PRODUCTO.ToList<PRODUCTO>();
                    lista = ctx.PRODUCTO.Include(x => x.TIPO_PRODUCTO).ToList();
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
        public IEnumerable<PRODUCTO> GetProductoByDescripcion(string descripcion)
        {
            IEnumerable<PRODUCTO> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.PRODUCTO.ToList().
                    FindAll(l => l.DESCRIPCION.ToLower().Contains(descripcion.ToLower()));
            }
            return lista;
        }

        public PRODUCTO GetProductoByID(string id)
        {
            PRODUCTO oPRODUCTO = null;
            //UBICACION uBICACION = null; 
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oPRODUCTO = ctx.PRODUCTO.
                    Where(p => p.ID == id).
                    Include(t => t.TIPO_PRODUCTO).
                    Include(p => p.PROVEEDOR).
                    Include(u => u.PRODUCTO_UBICACION).
                    Include("PRODUCTO_UBICACION.UBICACION").
                    FirstOrDefault();



                //  uBICACION = ctx.UBICACION.Where(c => c.PRODUCTO_UBICACION == oPRODUCTO.PRODUCTO_UBICACION).;

                //*** 1. Sintaxis LINQ Query *** https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/basic-linq-query-operations
                //(prof, video) Tiene una sitaxis muy similar a SQL. Desventaja: tengo que darle el formato que se espera
                //ventaja: se puede filtrar más. ordenarlo.

                //var infoAutor = from a in ctx.Autor
                //                where a.IdAutor == id
                //                select a;
                //oAutor = infoAutor.First();


                //*** 2. Sintaxis LINQ Method *** https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ef/language-reference/queries-in-linq-to-entities
                //(prof, video) Este es el que vamos a usar más. Tiene más elementos.
                //* 2.1. Método Find sin ningún otro método. VA SOLITO. formato automático

                //proyecto_quite esto**********oPRODUCTO = ctx.PRODUCTO.Find(id);

                //* 2.2 Con las demás partes de una consulta y AL FINAL se debe dar formato
                //x es cada registro.
                //oAutor = ctx.Autor.Where(x => x.IdAutor == id).First<Autor>();
                //La expresión lambda es una forma más corta de representar un método anónimo utilizando una sintaxis especial.
            }
            return oPRODUCTO;
        }
        //le falta:
        public IEnumerable<PRODUCTO> GetProductoByProveedor(int idProveedor)
        {
            IEnumerable<PRODUCTO> lista = null;
            //using (MyContext ctx = new MyContext())
            //{
            //    ctx.Configuration.LazyLoadingEnabled = false;
            //    lista = ctx.PRODUCTO.Where(l => l.PROVEEDOR == idAutor).ToList();
            //}
            return lista;
        }

        public PRODUCTO Save(PRODUCTO producto, string[] selectedProveedores, string[] selectedUbicaciones)
        {
            int retorno = 0;
            PRODUCTO oProducto = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProducto = GetProductoByID((String)producto.ID);
                IRepositoryProveedor _RepositoryProveedor = new RepositoryProveedor();
                IRepositoryUbicacion _RepositoryUbicacion = new RepositoryUbicacion();

                if (oProducto == null)
                {

                    //Insertar proveedores
                    if (selectedProveedores != null)
                    {

                        producto.PROVEEDOR = new List<PROVEEDOR>();
                        foreach (var proveedor in selectedProveedores)
                        { //LOGICA PARA INSERTAR CATEGORIAS===========================================================
                            var proveedorToAdd = _RepositoryProveedor.GetPROVEEDORByID(int.Parse(proveedor));
                            ctx.PROVEEDOR.Attach(proveedorToAdd);
                            //Se hace un Attach porque sino, EF intentará esa categoria como nueva,
                            //le indicamos que ya existe, que no la cree. Y la agregamos al libro.
                            producto.PROVEEDOR.Add(proveedorToAdd);// asociar a la categoría existente con el libro
                                                                   //FIN =========================================================================================

                        }
                    }
                    //Insertar ubicaciones
                    
                    if (selectedUbicaciones != null)
                    {

                        producto.PRODUCTO_UBICACION = new List<PRODUCTO_UBICACION>();
                        

                        foreach (var ubicaciones in selectedUbicaciones)
                        { //LOGICA PARA INSERTAR CATEGORIAS===========================================================
                            //var ubicacionToAdd = _RepositoryUbicacion.GetUbicacionByID(int.Parse(ubicaciones));
                            PRODUCTO_UBICACION proUbiToAdd = new PRODUCTO_UBICACION();
                            proUbiToAdd.ID_UBICACION = ubicaciones;
                            proUbiToAdd.ID_PRODUCTO = producto.ID;
                            proUbiToAdd.CANTIDAD = producto.CANTIDAD_MINIMA;

                            ctx.PRODUCTO_UBICACION.Attach(proUbiToAdd);
                            //Se hace un Attach porque sino, EF intentará esa categoria como nueva,
                            //le indicamos que ya existe, que no la cree. Y la agregamos al libro.
                            producto.PRODUCTO_UBICACION.Add(proUbiToAdd);// asociar a la categoría existente con el libro
                                                                         //FIN =========================================================================================
                            ctx.PRODUCTO_UBICACION.Add(proUbiToAdd);
                        }
                    }
                    ctx.PRODUCTO.Add(producto);
                   
                    
                    //SaveChanges
                    //guarda todos los cambios realizados en el contexto de la base de datos.
                    retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas
                }
                else
                {
                    //(mio) Ejemplo: debe sincrozinar cuales son nuevas, agregar y eliminar.
                    //Registradas: 1,2,3
                    //Actualizar: 1,3,4

                    //PRIMERO Actualizar Libro
                    ctx.PRODUCTO.Add(producto);
                    ctx.Entry(producto).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                    //SEGUNDO Actualizar Proveedores ====================================================================
                    var selectedProveedoresID = new HashSet<string>(selectedProveedores);
                    if (selectedProveedores != null)
                    {
                        //Obtengo las categorias que ya tiene:
                        ctx.Entry(producto).Collection(p => p.PROVEEDOR).Load();
                        //Aqui que sincronice, busque que hay nuevo, agregar y eliminar.
                        var newProveedorForProducto = ctx.PROVEEDOR
                         .Where(x => selectedProveedoresID.Contains(x.ID.ToString())).ToList();
                        //hace las categorias en un listado
                        producto.PROVEEDOR = newProveedorForProducto;

                        ctx.Entry(producto).State = EntityState.Modified;
                        retorno = ctx.SaveChanges(); //se guarda en el contexto
                    }
                    //SEGUNDO Actualizar Ubicaciones ====================================================================
                    //var selectedUbicacionesID = new HashSet<string>(selectedUbicaciones);
                    //if (selectedUbicaciones != null)
                    //{
                    //    //Obtengo las categorias que ya tiene:
                    //    ctx.Entry(producto).Collection(p => p.PRODUCTO_UBICACION).Load();
                    //    //Aqui que sincronice, busque que hay nuevo, agregar y eliminar.
                    //    var newUbicacionForProducto = ctx.PRODUCTO_UBICACION
                    //     .Where(x => selectedUbicacionesID.Contains(x.ID_UBICACION.ToString())).ToList(); 
                    //    //********este me esta trayendo TODAS las ubicaciones con el ID seleccionado

                    //    //hace las categorias en un listado
                    //    producto.PRODUCTO_UBICACION = newUbicacionForProducto;

                    //    ctx.Entry(producto).State = EntityState.Modified;
                    //    retorno = ctx.SaveChanges(); //se guarda en el contexto
                    //}
                }
            }

            if (retorno >= 0)
                oProducto = GetProductoByID(producto.ID);

            return oProducto;
        }
        //Fin sem 5===============================================================================

        public void DeleteProducto(int id)
        {
            throw new NotImplementedException();
        }
    }
}
