using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceTelefono : IServiceTelefono
    {
        public IEnumerable<Telefono> GetTelefono()
        {
            //hacemos referencia al Repositorio
            IRepositoryTelefono repository = new RepositoryTelefono();
            return repository.GetTelefono();
        }

        public Telefono GetTelefonoByID(int id)
        {
            IRepositoryTelefono repository = new RepositoryTelefono();
            return repository.GetTelefonoByID(id);
        }
    }
}
