using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IServiceBase<T> where T:class// не используется
    {
        public Task<IEnumerable<T>> FindAll();
        Task<T> GetById(Guid? newsId);
        Task SaveAsync();
        Task CreateOneAsync(T t);
        void Save();
    }
}
