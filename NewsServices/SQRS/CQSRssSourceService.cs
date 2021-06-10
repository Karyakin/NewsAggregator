using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.NewsEnt;
using Entities.Models;
using Entities.Models.AssembledModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAgregato.DAL.CQRS.Commands;
using NewsAgregato.DAL.CQRS.Queries;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Services.SQRS
{
    public class CQSRssSourceService : IRssSourceService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CQSRssSourceService( IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        public async Task<RssSourceModel> GetRssSourceById(Guid? rssSourceId)
        {
            var rssSourseQuery = new GetRssSourseByIdQuery(rssSourceId.Value);
            var rssSourseDto = await _mediator.Send(rssSourseQuery);
            var res = _mapper.Map<RssSourceModel>(rssSourseDto);
            return res;
        }

       
        public async Task<IEnumerable<RssSourceModel>> RssSourceByNameAndUrl(string name, string url)
        {
            var rssSourseQuery = new GetRssSourseByNameAndUrlQuery(name, url);
            var rssSourseDto = await _mediator.Send(rssSourseQuery);
            var res = _mapper.Map<IEnumerable<RssSourceModel>>(rssSourseDto);
            return res;
        }

      
        public async Task CreateOneRssSource(RssSourceModel rssSourceModel)
        {
            rssSourceModel.Id = Guid.NewGuid();

            var addRssSourseCommand = new AddRssSourseCommand(
                rssSourceModel.Id
                ,rssSourceModel.Name
                ,rssSourceModel.Url
                ,rssSourceModel.DateOfReceiving);

            /*var chengedCount = */await _mediator.Send(addRssSourseCommand);
        }





        public Task<IEnumerable<RssSourceModel>> GetAllRssSourceAsync(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<SourseWithNewsCategory> RssSourceByIdWithNews(Guid? rssSourceId)
        {
            throw new NotImplementedException();
        }

        public Task<RssSource> RssSourceByName(string rssSourceName)
        {
            throw new NotImplementedException();
        }

       
    }
}
