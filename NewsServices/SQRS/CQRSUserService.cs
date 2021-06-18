using Contracts.ServicesInterfacaces;
using Entities.DataTransferObject;
using Entities.Entity.Users;
using Entities.Models;
using MediatR;
using NewsAgregato.DAL.CQRS.Queries;
using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SQRS
{
    public class CQRSUserService : IUserService
    {
        private readonly IMediator _mediator;
        public CQRSUserService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<User> GetUserByLogin(string login)
        {
            var getUserQuery = new GetUserByLoginQuery(login);
            var user = await _mediator.Send(getUserQuery);
            return user;
        }




        #region NotImplemented

        public Task<User> ArrangeNewUser(RegisterDto registerDto, PasswordSoultModel passwordSoultModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsersWithPhoneROleMail()
        {
            throw new NotImplementedException();
        }

        public PasswordSoultModel GetPasswordHashSoult(string modelPassword)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserWithDetails(string login)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExist(string login)
        {
            throw new NotImplementedException();
        }


        #endregion

       
    }
}
