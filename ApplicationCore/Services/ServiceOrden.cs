using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceOrden : IServiceOrden
    {
        public IEnumerable<ENC_FACTURA> GetOrden()
        {
            IRepositoryOrden repository = new RepositoryOrden();
            return repository.GetOrden();
        }

        public ENC_FACTURA GetOrdenByID(int id)
        {
            IRepositoryOrden repository = new RepositoryOrden();
            return repository.GetOrdenByID(id);
        }

        public ENC_FACTURA Save(ENC_FACTURA orden)
        {
            IRepositoryOrden repository = new RepositoryOrden();
            return repository.Save(orden);
        }
        public void GetOrdenCountDate(out string etiquetas1, out string valores1)
        {
            IRepositoryOrden repository = new RepositoryOrden();

            repository.GetOrdenCountDate(out string etiquetas, out string valores);
            etiquetas1 = etiquetas;
            valores1 = valores;
        }

    }
}
