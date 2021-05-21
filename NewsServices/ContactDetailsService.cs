using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entities.Entity.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ContactDetailsService : IContactDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactDetailsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        public async Task DeleteContacts(Guid id)
        {
            var contact = await _unitOfWork.ContactDetails.GetById(id, false);
            _unitOfWork.ContactDetails.Remove(contact);
        }
    }
}
