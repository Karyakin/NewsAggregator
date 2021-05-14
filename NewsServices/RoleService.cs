using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddRoleToUser(string roleName, User user)
        {
            //  var user = await _unitOfWork.User.GetByCondition(x => x.Login.Equals(userEnt.Login), false).FirstOrDefaultAsync();
            //  user.RoleId = 
            var role = await GetRoleIdyByName(roleName);
            user.RoleId = role.Id;
            await _unitOfWork.User.Update(user);
            await _unitOfWork.SaveAsync();
            // return _mapper.Map<UserDto>(user);
        }





        public async Task<Role> GetRoleIdyByName(string roleName) =>
            await _unitOfWork.Role.GetByCondition(x => x.Name.Equals(roleName), false).SingleOrDefaultAsync();

    }
}
