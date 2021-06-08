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
        public IEnumerable<PRODUCTO> GetProducto()
        {
            //hacemos referencia al Repositorio
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProducto();
        }

        public PRODUCTO GetProductoByID(int id)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoByID(id);
        }
    }
}
