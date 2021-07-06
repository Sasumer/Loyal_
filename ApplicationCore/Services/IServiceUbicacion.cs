using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceUbicacion
    {
        IEnumerable<UBICACION> GetUbicacion();
        UBICACION GetUbicacionByID(int id);
    }
}
