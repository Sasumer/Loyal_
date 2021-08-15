using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEncFactura : IServiceEncFactura
    {
        public IEnumerable<ENC_FACTURA> GetEncFactura()
        {
            //hacemos referencia al Repositorio
            IRepositoryEncFactura repository = new RepositoryEncFactura();
            return repository.GetEncFactura();
        }

        public ENC_FACTURA GetEncFacturaByID(int id)
        {
            IRepositoryEncFactura repository = new RepositoryEncFactura();
            return repository.GetEncFacturaByID(id);
        }

        //public void GetOrdenCountDate(out string etiquetas1, out string valores1)
        //{
        //    IRepositoryOrden repository = new RepositoryOrden();

        //    repository.GetOrdenCountDate(out string etiquetas, out string valores);
        //    etiquetas1 = etiquetas;
        //    valores1 = valores;
        //}
    }
}
