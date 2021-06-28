using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entity.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            return await exist.AnyAsync(x => x.Name.Contains(countryIn));
        }

        public async Task<IEnumerable<Country>> FindAllCountries() =>
            await _unitOfWork.Country.GetAll(false).ToListAsync();

        public async Task<Country> FindCountryById(Guid countryId)
        {
            var country = await _unitOfWork.Country.GetById(countryId, false);
            return country;
        }

        public async Task<Country> FindCountryByName(string nameCountry) =>
           await _unitOfWork.Country.GetByCondition(x => x.Name.Equals(nameCountry), false).SingleOrDefaultAsync();
    }
}
