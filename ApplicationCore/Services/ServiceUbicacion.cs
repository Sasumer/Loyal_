using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUbicacion : IServiceUbicacion
    {
        public IEnumerable<UBICACION> GetUbicacion()
        {
            IRepositoryUbicacion repository = new RepositoryUbicacion();
            return repository.GetUbicacion();
        }

        public UBICACION GetUbicacionByID(int id)
        {
            IRepositoryUbicacion repository = new RepositoryUbicacion();
            return repository.GetUbicacionByID(id);
        }
    }
}
