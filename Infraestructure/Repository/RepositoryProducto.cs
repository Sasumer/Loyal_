
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

        public PRODUCTO GetProductoByID(string id)
        {
            PRODUCTO oPRODUCTO = null;
            UBICACION uBICACION = null;
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
    }
}
