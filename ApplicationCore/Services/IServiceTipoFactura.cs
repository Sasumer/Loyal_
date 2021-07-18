using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceTipoFactura
    {
        IEnumerable<TIPO_FACTURA> GetTipoFacturao();
        TIPO_FACTURA GetTipoFacturaByID(int id);
    }
}
