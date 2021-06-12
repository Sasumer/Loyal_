using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryProducto
    {
        IEnumerable<PRODUCTO> GetProducto();
        PRODUCTO GetProductoByID(String id);
        //void DeleteLibro(int id);
        //Libro Save(Libro libro, string[] selectedCategorias);
    }
}
