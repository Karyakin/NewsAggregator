﻿using Contracts.UnitOfWorkInterface;
using System;
using System.Threading.Tasks;
using Contracts.RepositoryInterfaces;
using Repositories.Context;
using Repositories.Categories;
using Repositories.RssSources;
using Repositories.NewsRep;
using Contracts.ServicesInterfacaces;
using Services;
using Repositories.CountryRepo;

namespace Repositories.UnitOfWorkRepository
{
    /// <summary>
    /// Unit Of Work Врапером мы решаем проблему создания контекстов. Т.е. ко всем репозиториям мы будем обращатся через врапер
    /// и соответсвенно через одни экземпляр контекста. После работы с контекстом мы сохраняем один раз и весь контекст 
    /// сохраняется разом
    /// </summary>
    public class RepositoryUnitOfWork : IUnitOfWork, IDisposable
    {
        private NewsDataContext _newsDataContextWrapper;
        private INewsRepository _newsRepository;
        private ICategoryRepository _categoryRepository;
        private IRssSourceRepository _rssSourceRepository;
        private IUserRepository _UserRepository;
        private ICountryRepository _countryRepository;

        public RepositoryUnitOfWork(NewsDataContext newsDataContextWrapper)
        {
            _newsDataContextWrapper = newsDataContextWrapper;
           
        }


        public ICountryRepository Country
        {
            get
            {
                if (_countryRepository == null)
                    _countryRepository = new CountryRepository(_newsDataContextWrapper);

                return _countryRepository;
            }
        }

        public IUserRepository User 
        {
            get
            {
                if (_UserRepository == null)
                    _UserRepository = new UserRepository(_newsDataContextWrapper);

                return _UserRepository;
            }
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

 

        public void Dispose()
        {
            _newsDataContextWrapper?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Save() => _newsDataContextWrapper.SaveChanges();
        public Task SaveAsync() => _newsDataContextWrapper.SaveChangesAsync();
    }
}
