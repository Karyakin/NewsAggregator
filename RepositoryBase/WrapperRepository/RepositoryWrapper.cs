//using NewsAggregatorMain.Data;
//using NewsAggregatorMain;
using Repositories.Migrations;
using RepositoriesInterfaces;
using RepositoriesInterfaces.WrapperInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.WrapperRepository
{
    /// <summary>
    /// Unit Of Work Врапером мы решаем проблему создания контекстов. Т.е. ко всем репозиториям мы будем обращатся через врапер
    /// и соответсвенно через одни экземпляр контекста. После работы с контекстом мы сохраняем один раз и весь контекст 
    /// сохраняется разом
    /// </summary>
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private NewsDataContext _newsDataContextWrapper;
        private INewsRepository _newsRepository;
        public RepositoryWrapper(NewsDataContext newsDataContextWrapper)
        {
            _newsDataContextWrapper = newsDataContextWrapper;
        }

        public INewsRepository News
        {
            get
            {
                if (_newsRepository == null)
                    _newsRepository = new NewsRepository(_newsDataContextWrapper);

                return _newsRepository;
            }
        }

        public void Save() => _newsDataContextWrapper.SaveChanges();
        public Task SaveAsync() => _newsDataContextWrapper.SaveChangesAsync();
    }
}
