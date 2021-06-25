using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.WebAPI.Requests
{
        public class RegisterRequest
        {
            public string Login { get; set; }
            public string Password { get; set; }
            public string PasswordConfirmation { get; set; }
        }
}
