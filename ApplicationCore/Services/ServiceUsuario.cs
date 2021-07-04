using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        public IEnumerable<USUARIO> GetUSUARIO()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUSUARIO();
        }

        public USUARIO GetUSUARIOByID(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUSUARIOByID(id);
        }
    }
}
