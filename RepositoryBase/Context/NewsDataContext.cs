﻿using Microsoft.EntityFrameworkCore;
using Entities.Entity.NewsEnt;
using Entities.Entity.Users;
using Entity.Users;
using Microsoft.EntityFrameworkCore.Design;

namespace Repositories.Context
{
    public class NewsDataContext : DbContext
    {
        public NewsDataContext(DbContextOptions<NewsDataContext> options) : base(options)
        {
        }

        private DbSet<City> Сities { get; set; }
        private DbSet<ContactDetails> ContactDetails { get; set; }
        private DbSet<Phone> Phones { get; set; }
        private DbSet<EMail> EMails { get; set; }
        private DbSet<Country> Countries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        private DbSet<Photo> Photos { get; set; }
        private DbSet<Author> Authors { get; set; }
        public DbSet<RssSource> RssSource { get; set; }
        private DbSet<Category> Categories { get; set; }
        private DbSet<Comment> Comments { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<RateWorlds> RateWorlds { get; set; }
     
    }

   /* public class YourDbContextFactory : IDesignTimeDbContextFactory<NewsDataContext>
    {
        public NewsDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NewsDataContext>();
            return new NewsDataContext(optionsBuilder.Options);
        }
    }*/
}
