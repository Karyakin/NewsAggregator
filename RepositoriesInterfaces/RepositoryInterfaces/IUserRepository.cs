using Entities.Entity.NewsEnt;
using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObject;
using Contracts.Interfaces;
using Entities.Entity.Users;

namespace Contracts.RepositoryInterfaces
{

    /// <summary>
    /// Сой вариант зависимостей и наследований
    /// </summary>
    public interface IUserRepository : IRepositoryBases<User>   //: IRepositoryBase<User>
    {
       

    }
}

