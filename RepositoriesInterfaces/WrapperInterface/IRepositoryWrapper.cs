using Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.WrapperInterface
{
    public interface IRepositoryWrapper
    {
        public INewsRepository News { get; }
        public ICategoryRepository Category { get; }
        public IRssSourceRepository RssSource { get; }

        Task SaveAsync();
        void Save();
    }
}
