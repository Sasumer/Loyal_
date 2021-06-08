using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceProducto
    {
        IEnumerable<PRODUCTO> GetProducto();
        PRODUCTO GetProductoByID(int id);

        //void DeleteLibro(int id);
        //Libro Save(Libro libro, string[] selectedCategorias);
    }
}
