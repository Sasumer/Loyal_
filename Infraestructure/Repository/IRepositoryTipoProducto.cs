using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryTipoProducto
    {
        IEnumerable<TIPO_PRODUCTO> GetTipoProducto();
        TIPO_PRODUCTO GetTipoProductoByID(int id);
    }
}
