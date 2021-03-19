﻿
using Microsoft.EntityFrameworkCore;
using NewsAgregator.DAL.Entities.Entity.News;
using NewsAgregator.DAL.Entities.Entity.Users;

namespace NewsAggregatorMain.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        private DbSet<ContactDetails> ContactDetails { get; set; }
        private DbSet<Phone> Phones { get; set; }
        private DbSet<EMail> EMails { get; set; }
        private DbSet<City> Сities { get; set; }
        private DbSet<Country> Countries { get; set; }
        private DbSet<User> Users { get; set; }
        private DbSet<Role> Roles { get; set; }
        private DbSet<Photo> Photos { get; set; }
        private DbSet<Author> Authors { get; set; }
        private DbSet<Source> Sources { get; set; }
        private DbSet<Category> Categories{ get; set; }
        private DbSet<Comment> Comments{ get; set; }


    }
}
