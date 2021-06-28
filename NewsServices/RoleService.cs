using AutoMapper;
using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.DataTransferObject;
using Entities.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Serilog;
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
            if (roleName is null)
            {
                Log.Error($"roleName can not be null. method: {nameof(AddRoleToUser)}");
                throw new ArgumentNullException();
            }

            if (user is null)
            {
                Log.Error($"user can not be null. method: {nameof(AddRoleToUser)}");
                throw new ArgumentNullException();
            }
            try
            {
                var role = await GetRoleIdyByName(roleName);
                user.RoleId = role.Id;
                _unitOfWork.User.Update(user);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
            
        }

        public async Task<Role> GetRoleIdyById(Guid idRole)=>
                            await _unitOfWork.Role.GetByCondition(x => x.Id.Equals(idRole), false).SingleOrDefaultAsync();

        public async Task<Role> GetRoleIdyByName(string roleName) =>
                            await _unitOfWork.Role.GetByCondition(x => x.Name.Equals(roleName), false).SingleOrDefaultAsync();

        public async Task<IEnumerable<Role>> GetRoles()=>
                         await _unitOfWork.Role.GetAll(false).ToListAsync();
    }
}
