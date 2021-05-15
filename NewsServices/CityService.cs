using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.Entity.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CityExist(string cityName)
        {
          var city = await _unitOfWork.City?.GetByCondition(x => x.Name.Equals(cityName), false).FirstOrDefaultAsync();

            return city == null ? false : true;
        }

        public async Task<IEnumerable<City>> FindAllCity() => 
            await _unitOfWork.City.GetAll(false).ToListAsync();

        public async Task<City> FindCityById(Guid сityId)
        {
            var city = await _unitOfWork.City.GetById(сityId, false);
            return city;
        }

        public async Task<City> FindCityByName(string name) =>
            await _unitOfWork.City.GetByCondition(x => x.Name.Equals(name), false).SingleOrDefaultAsync();
    }
}
