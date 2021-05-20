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
   public class PhoneService : IPhoneService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PhoneService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Phone> CheckPhoneExist(string phone)
            => await _unitOfWork.Phone.GetByCondition(x => x.PhoneNumber.Equals(phone), false).FirstOrDefaultAsync();
    }
}
