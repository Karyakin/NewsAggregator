using Entities.Models;
using Entity.Users;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
   public class LoginDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
