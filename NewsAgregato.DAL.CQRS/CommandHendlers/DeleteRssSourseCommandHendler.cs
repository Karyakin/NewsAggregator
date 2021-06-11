using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAgregato.DAL.CQRS.Commands;
using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAgregato.DAL.CQRS.CommandHendlers
{
    public class DeleteRssSourseCommandHendler : IRequestHandler<DeleteRssSourseCommand, int>
    {
        private readonly NewsDataContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteRssSourseCommandHendler(NewsDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteRssSourseCommand request, CancellationToken cancellationToken)
        {
            var rssSourseEnt = await _dbContext.RssSource.FindAsync(request.Id);
            _dbContext.Remove(rssSourseEnt);
            var res = await _dbContext.SaveChangesAsync();
            return res;
        }
    }
}
