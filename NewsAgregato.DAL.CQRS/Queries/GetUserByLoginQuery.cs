using Entities.DataTransferObject;
using Entities.Entity.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregato.DAL.CQRS.Queries
{
   public class GetUserByLoginQuery : IRequest<User>
    {
        public string Login { get; set; }
        public GetUserByLoginQuery(string login)
        {
            Login = login;
        }
    }
}
