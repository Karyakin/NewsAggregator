using AutoMapper;
using Entities.DataTransferObject;
using Entities.Entity.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAgregato.DAL.CQRS.Queries;
using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAgregato.DAL.CQRS.QueryHendlers
{
    public class GetAllUsersQueryHendler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly NewsDataContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHendler(NewsDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users
                .Select(sourse => _mapper.Map< UserDto>(sourse))
                .ToListAsync(cancellationToken);

            return users;
        }
    }
}
