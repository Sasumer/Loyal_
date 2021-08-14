using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceTipoFactura : IServiceTipoFactura
    {
        public TIPO_FACTURA GetTipoFacturaByID(int id)
        {
            IRepositoryTipoFactura repository = new RepositoryTipoFactura();
            return repository.GetTipoFacturaByID(id);
        }

        public IEnumerable<TIPO_FACTURA> GetTipoFactura()
        {
            IRepositoryTipoFactura repository = new RepositoryTipoFactura();
            return repository.GetTipoFactura();
        }
    }
}
