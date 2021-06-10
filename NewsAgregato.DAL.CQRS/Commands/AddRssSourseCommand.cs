using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregato.DAL.CQRS.Commands
{
   public class AddRssSourseCommand : IRequest<int>
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime DateOfReceiving { get; set; } = DateTime.Now;

        public AddRssSourseCommand(Guid id, string name, string url, DateTime dateOfReceiving)
        {
            Id = id;
            Name = name;
            Url = url;
            DateOfReceiving = dateOfReceiving;
        }
    }
}
