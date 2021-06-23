using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceTipoProducto : IServiceTipoProducto
    {
        public IEnumerable<TIPO_PRODUCTO> GetTipoProducto()
        {
            //hacemos referencia al Repositorio
            IRepositoryTipoProducto repository = new RepositoryTipoProducto();
            return repository.GetTipoProducto();
        }

        public TIPO_PRODUCTO GetTipoProductoByID(int id)
        {
            IRepositoryTipoProducto repository = new RepositoryTipoProducto();
            return repository.GetTipoProductoByID(id);
        }
    }
}
