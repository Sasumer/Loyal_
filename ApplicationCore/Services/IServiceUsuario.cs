using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceUsuario
    {
        IEnumerable<USUARIO> GetUSUARIO();
        USUARIO GetUSUARIOByID(int id);

        USUARIO GetLoginUsuario(string id, string password);

        USUARIO Save(USUARIO uSUARIO, String[] selectedRol);

    }
}
