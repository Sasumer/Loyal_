using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProducto : IServiceProducto
    {
        public void DeleteProducto(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PRODUCTO> GetProducto()
        {
            //hacemos referencia al Repositorio
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProducto();
        }
        public IEnumerable<string> GetProductoNombres()
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProducto().Select(x => x.DESCRIPCION);
        }
        public IEnumerable<PRODUCTO> GetProductoByProveedor(int idProveedor)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoByProveedor(idProveedor);
        }


        public PRODUCTO GetProductoByID(String id)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoByID(id);
        }
        public IEnumerable<PRODUCTO> GetProductoByDescripcion(string descripcion)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoByDescripcion(descripcion);
        }

        public PRODUCTO Save(PRODUCTO producto, string[] selectedProveedores, string[] selectedUbicaciones)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.Save(producto, selectedProveedores, selectedUbicaciones);
        }

        public PRODUCTO SaveXOrden(PRODUCTO producto, string[] selectedProveedores, string[] selectedUbicaciones)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.SaveXOrden(producto, selectedProveedores, selectedUbicaciones);
        }
    }
}
