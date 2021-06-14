using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProveedor : IServiceProveedor
    {
        public IEnumerable<PROVEEDOR> GetPROVEEDOR()
        {
            IRepositoryProveedor repository = new RepositoryProveedor();
            return repository.GetPROVEEDOR();
        }

        public PROVEEDOR GetPROVEEDORByID(int id)
        {
            IRepositoryProveedor repository = new RepositoryProveedor();
            return repository.GetPROVEEDORByID(id);
        }
    }
}
