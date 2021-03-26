
using Repositories.Migrations;
using Contracts;
using Contracts.WrapperInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.RepositoryInterfaces;
using Repositories.Context;
using Repositories.Categories;
using Repositories.RssSources;
using Repositories.NewsRep;

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
        private ICategoryRepository _categoryRepository;
        private IRssSourceRepository _rssSourceRepository;
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
        public ICategoryRepository Category
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_newsDataContextWrapper);

                return _categoryRepository;
            }
        }
        public IRssSourceRepository RssSource
        {
            get
            {
                if (_rssSourceRepository == null)
                    _rssSourceRepository = new RssSourceRepository(_newsDataContextWrapper);

                return _rssSourceRepository;
            }
        }



        public void Save() => _newsDataContextWrapper.SaveChanges();
        public Task SaveAsync() => _newsDataContextWrapper.SaveChangesAsync();
    }
}
