using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.WebAPI.Auth
{
    public class JwtAuthResult
    {
        public object AccessToken { get; set; }
        public RefreshToken RefreshToken{get;set;}
    }
}
