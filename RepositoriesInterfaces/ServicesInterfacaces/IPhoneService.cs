﻿using Entities.Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServicesInterfacaces
{
   public interface IPhoneService
    {
        public  Task<Phone> CheckPhoneExist(string phone);
    }
}
