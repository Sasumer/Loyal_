using System;
using Infraestructure.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryUsuario
    {
        IEnumerable<USUARIO> GetUSUARIO();

        IEnumerable<USUARIO> GetNombreUSUARIO(String nombre);

        IEnumerable<USUARIO> GetUSUARIOByContacto(int idContacto);


        USUARIO GetUSUARIOByID(int id);

        void DeleteUsuario(int id);

        USUARIO Save(USUARIO USUARIO, string[] selectedRol);

        USUARIO GetLoginUsuario(string id, string password);
    }
}
