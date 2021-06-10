using Entities.DataTransferObject;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregato.DAL.CQRS.Queries
{
    public class GetRssSourseByIdQuery : IRequest<RssSourceDto>
    {
        public Guid Id { get; set; }

        public GetRssSourseByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
