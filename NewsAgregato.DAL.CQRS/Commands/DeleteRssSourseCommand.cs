using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregato.DAL.CQRS.Commands
{
   public class DeleteRssSourseCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public DeleteRssSourseCommand(Guid id)
        {
            Id = id;
        }
    }
}
