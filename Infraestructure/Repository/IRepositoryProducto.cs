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
        IEnumerable<PRODUCTO> GetProductoByDescripcion(String descripcion);
        IEnumerable<PRODUCTO> GetProductoByProveedor(int idProveedor);
        void DeleteProducto(int id);
        PRODUCTO Save(PRODUCTO libro, string[] selectedProveedores, string[] selectedUbicaciones);
        PRODUCTO SaveXOrden(PRODUCTO libro, string[] selectedProveedores, string[] selectedUbicaciones);
    }
}
