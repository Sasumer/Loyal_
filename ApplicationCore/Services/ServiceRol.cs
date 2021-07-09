﻿using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceRol : IServiceRol
    {
        public ROL GetROLbyID(int id)
        {
            IRepositoryRol repository = new RepositoryRol();
            return repository.GetROLbyID(id);
        }

        public IEnumerable<ROL> GetROLs()
        {
            IRepositoryRol repository = new RepositoryRol();
            return repository.GetROLs();
        }
    }
}
