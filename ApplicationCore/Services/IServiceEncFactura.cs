using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceEncFactura
    {
        IEnumerable<ENC_FACTURA> GetEncFactura();
        ENC_FACTURA GetEncFacturaByID(int id);

        //void GetOrdenCountDate(out string etiquetas, out string valores);
    }
}
