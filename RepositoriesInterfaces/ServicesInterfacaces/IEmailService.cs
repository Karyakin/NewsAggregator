using Entity.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ServicesInterfacaces
{
   public interface IEmailService
    {
        public Task<EMail> CheckEmailExist(string userEmail);
    }
}
