using Contracts.RepositoryInterfaces;
using Contracts.ServicesInterfacaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UnitOfWorkInterface
{
    public interface IUnitOfWork
    {
        public INewsRepository News { get; }
        public ICategoryRepository Category { get; }
        public IRssSourceRepository RssSource { get; }
        public IUserRepository User{ get; }
        public ICountryRepository Country { get; }
        public ICityRepository City{ get; }
        public IRoleRepository Role{ get; }
        public IContactDetailsRepository ContactDetails{ get; }
        public IPhoneRepository Phone { get; }
        public IEmailRepository Email{ get; }



        Task SaveAsync();
        void Save();
    }
}
