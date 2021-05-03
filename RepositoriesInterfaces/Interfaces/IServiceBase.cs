using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IServiceBase<T> where T:class// не используется
    {
        public Task<IEnumerable<T>> FindAll();
        Task CreateOneAsync(T t);
        Task<T> GetById(Guid? newsId);
        Task SaveAsync();
        void Save();
    }
}
