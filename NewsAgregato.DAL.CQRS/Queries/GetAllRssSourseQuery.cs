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
       /* public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime DateOfReceiving { get; set; } = DateTime.Now;

        public GetAllRssSourseQuery(Guid id, string name, string url, DateTime dateOfReceiving)
        {
            Id = id;
            Name = name;
            Url = url;
            DateOfReceiving = dateOfReceiving;
        }
*/
    }
}
