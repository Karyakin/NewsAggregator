using Entities.Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServicesInterfacaces
{
   public interface ICityService
    {
        Task<IEnumerable<City>> FindAllCity();
        Task<City> FindCityById(Guid сityId);
        Task<City> FindCityByName(string name);
        Task<bool> CityExist(string cityName);

    }
}
