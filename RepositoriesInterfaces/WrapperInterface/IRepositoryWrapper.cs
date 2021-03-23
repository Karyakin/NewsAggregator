using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesInterfaces.WrapperInterface
{
   public interface IRepositoryWrapper
    {
        public INewsRepository News { get; }

        Task SaveAsync();
        void Save();
    }
}
