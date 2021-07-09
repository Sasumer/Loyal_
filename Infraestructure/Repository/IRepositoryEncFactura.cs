using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryEncFactura
    {
        IEnumerable<ENC_FACTURA> GetEncFactura();
        ENC_FACTURA GetEncFacturaByID(int id);
        //void DeleteLibro(int id);
        //Libro Save(Libro libro, string[] selectedCategorias);
    }
}
