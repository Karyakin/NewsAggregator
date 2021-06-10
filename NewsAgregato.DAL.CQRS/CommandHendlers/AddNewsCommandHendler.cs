using AutoMapper;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using MediatR;
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
    public class AddNewsCommandHendler : IRequestHandler<AddRssSourseCommand, int>
    {
        private readonly NewsDataContext _dbContext;
        private readonly IMapper _mapper;

        public AddNewsCommandHendler(NewsDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddRssSourseCommand request, CancellationToken cancellationToken)
        {
            var rssSourseDto = new RssSourceDto
            {
                Id = request.Id,
                Name = request.Name,
                Url = request.Url,
                DateOfReceiving = DateTime.Now
            };

            var rssSourseEnt = _mapper.Map<RssSource>(rssSourseDto);

            await _dbContext.RssSource.AddAsync(rssSourseEnt, cancellationToken);
            return await _dbContext.SaveChangesAsync(cancellationToken);//метод возвращает количество измененных записей

        }


    }
}
