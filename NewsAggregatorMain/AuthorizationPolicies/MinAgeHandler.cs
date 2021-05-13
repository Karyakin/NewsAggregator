using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAggregatorMain.AuthorizationPolicies
{
    public class MinAgeHandler : AuthorizationHandler<MinAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            MinAgeRequirement requirement)
        {
            if (context.User.HasClaim(cl => cl.Type == "age"))
            {
                var age = Convert.ToInt32(context.User
                    .FindFirst(cl => cl.Type == "age")
                    .Value);

                if (age >= requirement.MinAge)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
