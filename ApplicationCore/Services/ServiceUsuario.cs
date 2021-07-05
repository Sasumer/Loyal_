using ApplicationCore.Utils;
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
        public USUARIO GetLoginUsuario(string id, string password)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();

            // Encriptar el password para poder compararlo
            string crytpPasswd = Cryptography.EncrypthAES(password);

            return repository.GetLoginUsuario(id, crytpPasswd);
        }

        public IEnumerable<USUARIO> GetUSUARIO()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUSUARIO();
        }

        public USUARIO GetUSUARIOByID(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            USUARIO oUsuario = repository.GetUSUARIOByID(id);
            oUsuario.contrasenna = Cryptography.DecrypthAES(oUsuario.contrasenna);
            return oUsuario;
        }
        public USUARIO Save(USUARIO uSUARIO, string[] selectedRol)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.Save(uSUARIO, selectedRol);
        }

       

    }
}
