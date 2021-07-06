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
        IEnumerable<string> GetProductoNombres();
        IEnumerable<PRODUCTO> GetProductoByDescripcion(String descripcion);
        IEnumerable<PRODUCTO> GetProductoByProveedor(int idProveedor); //GetLibroByAutor
        PRODUCTO GetProductoByID(String id);

        void DeleteProducto(int id);
        PRODUCTO Save(PRODUCTO producto, string[] selectedProveedores, string[] selectedUbicaciones);
    }
}
