﻿using Contracts.Interfaces;
using Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServicesInterfacaces
{
   public interface ICountryService 
    {
        Task<bool> CountryExist(string login);
        Task<IEnumerable<Country>> FindAllCountries();
        Task<Country> FindCountryById(Guid countryId);
        Task<Country> FindCountryByName(string nameCountry);
    }
}
