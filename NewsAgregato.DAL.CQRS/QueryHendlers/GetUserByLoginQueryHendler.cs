using AutoMapper;
using Entities.DataTransferObject;
using Entities.Entity.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAgregato.DAL.CQRS.Queries;
using Repositories.Context;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAgregato.DAL.CQRS.QueryHendlers
{
    public class GetUserByLoginQueryHendler : IRequestHandler<GetUserByLoginQuery, User>
    {

        private readonly NewsDataContext _dbContext;

        public GetUserByLoginQueryHendler(NewsDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Handle(GetUserByLoginQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(sourse => sourse.Login.Equals(request.Login), cancellationToken);
                return user;
            }
            catch (Exception ex)
            {

                Log.Error($"User not found {ex.Message}");
            }

            return new User();
        }
    }
}
