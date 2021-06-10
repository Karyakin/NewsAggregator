using AutoMapper;
using Entities.DataTransferObject;
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
    public class GetRssSourseByNameAndUrlHendler : IRequestHandler<GetRssSourseByNameAndUrlQuery, IEnumerable<RssSourceDto>>
    {
        private readonly NewsDataContext _dbContext;
        private readonly IMapper _mapper;

        public GetRssSourseByNameAndUrlHendler(NewsDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RssSourceDto>> Handle(GetRssSourseByNameAndUrlQuery request, CancellationToken cancellationToken)
        {

            return
                (await _dbContext.RssSource
                    .Where(sourse => sourse.Name.Contains(request.Name) && sourse.Url.Contains(request.Url))
                    .ToListAsync(cancellationToken)).Select(sourse => _mapper.Map<RssSourceDto>(sourse));
        }
    }
}
