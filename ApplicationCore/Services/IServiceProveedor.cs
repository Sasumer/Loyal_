using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceProveedor
    {

        IEnumerable<PROVEEDOR> GetPROVEEDOR();
        PROVEEDOR GetPROVEEDORByID(int id);

        PROVEEDOR Save(PROVEEDOR pROVEEDOR, String[] selectedUsuarios);
        IEnumerable<PROVEEDOR> GetNombrePROVEEDOR(string nombre);
    }
}
