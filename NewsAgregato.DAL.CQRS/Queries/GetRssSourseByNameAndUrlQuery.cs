using Entities.DataTransferObject;
using MediatR;
using NewsAgregato.DAL.CQRS.QueryHendlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregato.DAL.CQRS.Queries
{
   public class GetRssSourseByNameAndUrlQuery : IRequest<IEnumerable<RssSourceDto>>
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public GetRssSourseByNameAndUrlQuery(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}
