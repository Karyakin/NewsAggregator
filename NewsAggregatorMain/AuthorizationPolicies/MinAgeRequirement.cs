using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregatorMain.AuthorizationPolicies
{
    public class MinAgeRequirement : IAuthorizationRequirement
    {
        public int MinAge { get; set; }

        public MinAgeRequirement(int minAge)
        {
            MinAge = minAge;
        }
    }
}
