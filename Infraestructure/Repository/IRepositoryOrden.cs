using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryOrden 
    {
        ENC_FACTURA GetOrdenByID(int id);
        IEnumerable<ENC_FACTURA> GetOrden();
        ENC_FACTURA Save(ENC_FACTURA orden);

        void GetOrdenCountDate(out string etiquetas, out string valores);
    }
}
