﻿using Contracts.Interfaces;
using Entities.Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServicesInterfacaces
{
   public interface ICityRepository : IRepositoryBases<City>
    {
       // Task<bool> CityExist(string login);
    }
}
