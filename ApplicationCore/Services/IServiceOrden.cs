using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceOrden //encabezado de factura
    {
        ENC_FACTURA GetOrdenByID(int id);
        IEnumerable<ENC_FACTURA> GetOrden();
        ENC_FACTURA Save(ENC_FACTURA pOrden);
        void GetOrdenCountDate(out string etiquetas, out string valores);
    }
}
