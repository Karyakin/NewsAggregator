using Contracts.ServicesInterfacaces;
using Contracts.UnitOfWorkInterface;
using Entity.Users;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Services
{
    public class EmailService : IEmailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<EMail> CheckEmailExist(string userEmail)
            => await _unitOfWork.Email.GetByCondition(x => x.UserEMail.Equals(userEmail), false).FirstOrDefaultAsync();
    }
}
