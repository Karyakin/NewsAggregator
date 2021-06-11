using Entities.DataTransferObject;
using Entities.Models;
using MediatR;
using NewsAgregato.DAL.CQRS.QueryHendlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregato.DAL.CQRS.Queries
{
   public class GetAllRssSourseQuery : IRequest<IEnumerable<RssSourceModel>>
    {
       
      
    }
}
