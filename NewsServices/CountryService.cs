﻿using Contracts.Interfaces;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entity.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CountryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> CountryExist(string countryIn)
        {
            var exist = _unitOfWork.Country.GetAll(false);
            var rez = await exist.AnyAsync(x => x.Name.Contains(countryIn));
            return rez;


        }
    }
}
