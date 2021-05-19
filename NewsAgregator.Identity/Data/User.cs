﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Identity.Data
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
    }
}
