using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
   public class RegisterDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }

        PasswordSoultModel passwordSoultModel { get; set; }
       /* public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }*/
    }
}
