using AutoMapper;
using Entities.DataTransferObject;
using Entities.Models;
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
    public class GetAllRssSourselHendler : IRequestHandler<GetAllRssSourseQuery, IEnumerable<RssSourceModel>>
    {
        private readonly NewsDataContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllRssSourselHendler(NewsDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RssSourceModel>> Handle(GetAllRssSourseQuery request, CancellationToken cancellationToken)
        {
            var res = await _dbContext.RssSource
                .Select(sourse => _mapper.Map<RssSourceModel>(sourse))
                .ToListAsync(cancellationToken);
            return res;
        }


      
    }
}
