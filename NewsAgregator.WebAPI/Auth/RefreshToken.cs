using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.WebAPI.Auth
{
    public class RefreshToken
    {
        public string Login { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }//срок действия
    }
}
