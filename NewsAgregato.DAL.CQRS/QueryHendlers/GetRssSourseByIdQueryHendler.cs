using AutoMapper;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
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
    public class GetRssSourseByIdQueryHendler : IRequestHandler<GetRssSourseByIdQuery, RssSourceDto>
    {
        private readonly NewsDataContext _dbContext;
        private readonly IMapper _mapper;

        public GetRssSourseByIdQueryHendler( NewsDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<RssSourceDto> Handle(GetRssSourseByIdQuery request, CancellationToken cancellationToken)
        {
            var rssSourseEnt = await _dbContext.RssSource.FirstOrDefaultAsync(sourse => sourse.Id.Equals(request.Id), cancellationToken);
            var rssSourseDto = _mapper.Map<RssSourceDto>(rssSourseEnt);
            return rssSourseDto;
        }
    }
}
