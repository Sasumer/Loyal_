using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryProveedor
    {
        IEnumerable<PROVEEDOR> GetPROVEEDOR();

        IEnumerable<PROVEEDOR> GetNombrePROVEEDOR(String nombre);

        IEnumerable<PROVEEDOR> GetPROVEEDORByContacto(int idContacto);


        PROVEEDOR GetPROVEEDORByID(int id);

        void DeleteLibro(int id);

        PROVEEDOR Save(PROVEEDOR proveedor, String[] selectedUsuarios);


    }
}
